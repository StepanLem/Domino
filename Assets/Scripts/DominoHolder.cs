using System.Collections;
using UnityEngine;

public class DominoHolder : MonoBehaviour
{
    [SerializeField] private float Cooldown;
    [SerializeField] private GameObject Prefab;
    private bool IsHolding;
    private GameObject ActivePiece;

    public void Drop()
    {
        IsHolding = false;
        ActivePiece.GetComponent<DominoPiece>().Activate();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Cooldown);
        ActivePiece = Instantiate(Prefab, transform);
        IsHolding=true;
    }
}
