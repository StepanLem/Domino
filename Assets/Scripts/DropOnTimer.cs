using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DropOnTimer : MonoBehaviour
{
    [SerializeField] public float Duration;
    [SerializeField] public float WarnTime;
    [SerializeField] private DominoHolder holder;
    public void StartCountDown()
    {
        StartCoroutine(Wait());
    }

    private void Reset()
    {
        holder = GetComponent<DominoHolder>();
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Duration - WarnTime);

        DropWarning warning = null;
        if (holder.ActivePiece != null && holder.ActivePiece.GetComponent<DropWarning>() != null)
        {
            warning = holder.ActivePiece.GetComponent<DropWarning>();
            warning.enabled = true;
        }

        yield return new WaitForSeconds(WarnTime);

        MatchSystem.Instance.TryUnfreezeDominos();
        holder.Drop();
        if (warning != null)
        {
            warning.enabled = false;
        }
    }
}
