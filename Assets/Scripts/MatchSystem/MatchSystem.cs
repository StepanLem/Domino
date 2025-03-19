using System.Collections;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Transform Camera;

    public Transform CurrentDominoSpawn;

    private Vector3 offsetBetweenDominoSpawnPoints;

    private Vector3 CameraOffsetFromDominoSpawn;

    public void CameraGoToNexDominoSpawn()
    {
        var nextDominoSpawn = CurrentDominoSpawn.position + offsetBetweenDominoSpawnPoints;
        var nextCameraPosition = nextDominoSpawn + CameraOffsetFromDominoSpawn;

        CameraGoSmoothlyTo(nextCameraPosition);
    }

    public float CameraMoveSpeed;
    public IEnumerator CameraGoSmoothlyTo(Vector3 TargetCameraPosition)
    {

        Vector3 startPosition = Camera.position;
        float journeyLength = Vector3.Distance(startPosition, TargetCameraPosition);
        float startTime = Time.time;
        float distanceCovered = 0f;
        float fracJourney = 0f;

        while (fracJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * CameraMoveSpeed;
            fracJourney = distanceCovered / journeyLength;

            Camera.position = Vector3.Lerp(startPosition, TargetCameraPosition, fracJourney);
            yield return null; // Wait for the next frame
        }

        Camera.position = TargetCameraPosition;
    }
}
