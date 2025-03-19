using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DominoHolder : MonoBehaviour
{
    [SerializeField] private float Cooldown;
    [SerializeField] private GameObject Prefab;
    private bool IsHolding;
    private GameObject ActivePiece;
    public UnityEvent OnDrop;

    private void Start()
    {
        CreateDomino();
    }

    public void Drop()
    {
        if (!IsHolding)
        {
            return;
        }
        IsHolding = false;
        ActivePiece.GetComponent<DominoPiece>().Activate();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Cooldown);
        CreateDomino();
    }

    private void CreateDomino()
    {
        ActivePiece = Instantiate(Prefab, transform);
        IsHolding = true;
    }
}
