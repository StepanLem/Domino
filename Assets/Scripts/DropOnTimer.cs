using System.Collections;
using UnityEngine;

public class DropOnTimer : MonoBehaviour
{
    [SerializeField] public float Duration;
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
        yield return new WaitForSeconds(Duration);
        holder.Drop();
    }
}
