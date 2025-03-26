using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform Camera;
    public GameObject PlatformPrefab;
    public Platform FirstPlatform;

    private Vector3 _rightTopOfLastPlatform;
    public float AllowedCameraOffsetToLastPlatform;

    public int SpawnOnAwake = 3;



    private void Awake()
    {
        _rightTopOfLastPlatform = FirstPlatform.RightTopPoint.position;

        _currentGradientPosition = Random.value;
        //_currentGradientPosition = 0;

        for (int i = 0; i < SpawnOnAwake + 1; i++)
        {
            SpawnOneMoreRandomPlatform();
        }
    }

    void Update()
    {
        if (Camera.position.x > _rightTopOfLastPlatform.x + AllowedCameraOffsetToLastPlatform)
        {
            SpawnOneMoreRandomPlatform();
        }
    }

    [Header("Color")]
    public Gradient ColorGradient;
    public float GradientStep = 0.03f;
    private float _currentGradientPosition;

    [Header("Length")]
    public float MinLength;
    public float MaxLength;
    

    [Header("Top Position")]
    public float MinYGlobal;
    public float MaxYGlobal;

    public float MinYFromPrev;
    public float MaxYFromPrev;


    
    public void SpawnOneMoreRandomPlatform()
    {
        GameObject platrformGameObject = Instantiate(PlatformPrefab);
        platrformGameObject.TryGetComponent<Platform>(out var platrform);

        var prevY = _rightTopOfLastPlatform.y;
        float minY = System.Math.Max(MinYGlobal, prevY + MinYFromPrev);
        float maxY = System.Math.Min(MaxYGlobal, prevY + MaxYFromPrev);
        float resultY = Random.Range(minY, maxY);
        var nextLeftTopPosition = new Vector3(_rightTopOfLastPlatform.x, resultY, _rightTopOfLastPlatform.z);

        var length = Random.Range(MaxLength, MinLength);
        var height = 10000f;

        _currentGradientPosition = (_currentGradientPosition + GradientStep) % 1;
        // Получаем цвет из градиента
        Color gradientColor = ColorGradient.Evaluate(_currentGradientPosition);

        platrform.SetParams(nextLeftTopPosition, length, height, gradientColor);

        _rightTopOfLastPlatform = platrform.RightTopPoint.position;
    }
}
