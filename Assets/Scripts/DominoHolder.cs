using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DominoHolder : MonoBehaviour
{
    [SerializeField] private float Cooldown;
    [SerializeField] private float StartAfter;
    [SerializeField] private GameObject Prefab;
    private bool IsHolding;
    private GameObject ActivePiece;
    [SerializeField] private Transform WorldOrigin;
    public UnityEvent OnDrop;
    public UnityEvent<float> OnDropX;
    public UnityEvent OnWaitingStarted;
    public UnityEvent OnNewDomino;
    public UnityEvent<GameObject> OnPieceCreated;
    public int QueuedCounter = 0;
    private bool IsWaiting = true;

    private void Start()
    {
        StartCoroutine(Wait(StartAfter));
        MatchSystem.Instance.DominoAllowedSpawnInArm += QueueDomino;
    }

    private void OnDestroy()
    {
        MatchSystem.Instance.DominoAllowedSpawnInArm -= QueueDomino;
    }

    public void Drop()
    {
        if (!IsHolding)
        {
            return;
        }
        IsHolding = false;
        ActivePiece.GetComponent<DominoPiece>().Activate();
        ActivePiece.transform.parent = WorldOrigin;
        OnDrop?.Invoke();
        OnDropX?.Invoke(ActivePiece.transform.position.x);
        StartCoroutine(Wait(Cooldown));
    }

    private IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (QueuedCounter > 0)
        {
            CreateDomino();
            QueuedCounter--;
        }
        else
        {
            IsWaiting = true;
            OnWaitingStarted?.Invoke();
        }
    }

    private void CreateDomino()
    {
        ActivePiece = Instantiate(Prefab, transform);
        IsHolding = true;
        IsWaiting = false;
        OnNewDomino?.Invoke();
        OnPieceCreated?.Invoke(ActivePiece);
    }

    [ContextMenu("Queue")]
    public void QueueDomino()
    {
        if (IsWaiting)
        {
            CreateDomino();
            return;
        }
        QueuedCounter++;
    }
}
