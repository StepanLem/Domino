using UnityEngine;

public class Progression : MonoBehaviour
{
    [SerializeField] private Transform distance;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minFactor;
    [SerializeField] private float maxFactor;

    private float Difficulty => distance.position.x;
    private float SoftDifficulty => Mathf.Clamp01(distance.position.x / maxDistance);
    private float DifficultyFactor => minFactor + (1 - SoftDifficulty) * (maxFactor - minFactor);

    public void UpdateDifficulty(GameObject piece)
    {
        var leftToRightMover = piece.GetComponent<LeftToRightMover>();
        leftToRightMover.cycleDuration = DifficultyFactor * initialSpeed;
        Debug.Log(leftToRightMover.cycleDuration);
    }
}
