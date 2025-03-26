using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform Camera;
    public GameObject PlatformPrefab;
    public Platform FirstPlatform;

    private Vector3 _rightTopOfLastPlatform;
    public float CameraOffsetToSpawnPlatform;

    public int platformsAhead = 3; // Number of platforms to keep ahead

    public Gradient ColorGradient;

    private void Start()
    {
        _rightTopOfLastPlatform = FirstPlatform.RightTopPoint.position;

        for (int i = 0; i < platformsAhead + 1; i++)
        {
            SpawnOneMoreRandomPlatform();
        }
    }

    void Update()
    {
        if (Camera.position.x > _rightTopOfLastPlatform.x + CameraOffsetToSpawnPlatform)
        {
            SpawnOneMoreRandomPlatform();
        }
    }

    public float MaxLength;
    public float MinLength;

    public float MaxYGlobal;
    public float MinYGlobal;
    public float MaxYFromPrev;
    public float MinYFromPrev;

    public void SpawnOneMoreRandomPlatform()
    {
        GameObject platrformGameObject = Instantiate(PlatformPrefab);
        platrformGameObject.TryGetComponent<Platform>(out var platrform);

        var prevY = _rightTopOfLastPlatform.y;
        float minY = System.Math.Max(MinYGlobal, prevY + MinYFromPrev);
        float maxY = System.Math.Min(MaxYGlobal, prevY + MaxYFromPrev);
        float resultY = Random.Range(minY, maxY);
        var nextLeftTopPosition = new Vector3(_rightTopOfLastPlatform.x, _rightTopOfLastPlatform.y + resultY, _rightTopOfLastPlatform.z);

        var length = Random.Range(MaxLength, MinLength);
        var height = 10000f;

        var positionOnGradient = Random.value;
        // Получаем цвет из градиента
        Color gradientColor = ColorGradient.Evaluate(positionOnGradient);

        platrform.SetParams(nextLeftTopPosition, length, height, gradientColor);

        _rightTopOfLastPlatform = platrform.RightTopPoint.position;
    }
}
