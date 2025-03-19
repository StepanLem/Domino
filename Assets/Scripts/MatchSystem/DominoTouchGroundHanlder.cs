using UnityEditor.Experimental.GraphView;
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
            var forcePosition = FirstDomino.transform.position + new Vector3(0,0.5f,0);
            var direction = new Vector3(1,0,0);
            var force = 50f;
            FirstDomino.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(direction*force, forcePosition);
        }

        //MatchSystem.Instance.CameraGoToNexDominoSpawn();
    }
}
