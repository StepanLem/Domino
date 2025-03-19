using Unity.VisualScripting;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform Camera;
    public GameObject PlatformPrefab;
    public Transform FirstPlatformTransform;
    public float platfromLength = 9.681248f;
    public int platformsAhead = 3; // Number of platforms to keep ahead

    private float nextXCameraPositionToSpawnPlatform;
    private float nextXPlatformPosition;

    private void Start()
    {
        nextXCameraPositionToSpawnPlatform = Camera.position.x;
        nextXPlatformPosition = FirstPlatformTransform.position.x + platfromLength;

        for (int i = 0; i < platformsAhead + 1; i++)
        {
            SpawnOneMorePlatform();
        }
    }

    void Update()
    {
        if (Camera.position.x > nextXCameraPositionToSpawnPlatform)
        {
            SpawnOneMorePlatform();
            nextXCameraPositionToSpawnPlatform += platfromLength;
        }
    }

    public void SpawnOneMorePlatform()
    {
        GameObject newPlat = Instantiate(PlatformPrefab);
        newPlat.transform.position = new Vector3(nextXPlatformPosition, FirstPlatformTransform.position.y, FirstPlatformTransform.position.z);
        nextXPlatformPosition += platfromLength;
    }
}
