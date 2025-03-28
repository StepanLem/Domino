using UnityEngine;

public class Progression : MonoBehaviour
{
    [SerializeField] private Transform distance;
    [SerializeField] private DropOnTimer dropOnTimer;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float initialDownSpeed;
    [SerializeField] private float initialDropCooldown;
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
        dropOnTimer.Duration = DifficultyFactor * initialDropCooldown;

        var downMovement = piece.GetComponent<DownMovement>();
        downMovement.Speed = initialDownSpeed * SoftDifficulty;

        Debug.Log(leftToRightMover.cycleDuration);
    }
}
