using UnityEngine;

public class DominoTouchGroundHanlder : MonoBehaviour
{
    public static DominoTouchGroundHanlder Instance;

    public int DominosCount;

    public DominoPiece FirstDomino;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void HandleNewDominoTouchGround(DominoPiece activeDomino)
    {
        DominosCount++;

        if (DominosCount == 1)
        {
            FirstDomino = activeDomino;
        }

        if (DominosCount == 5)
        {
            //FirstDomino.AddForce();
        }
    }
}
