using System.Collections;
using UnityEngine;

public class DominoRainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject domino;
    [SerializeField] private float zoneSize;
    [SerializeField] private float interval;
    [SerializeField] private int createdPerStep;
    [SerializeField] private int initialCount;
    [SerializeField] private float initialSpan;

    [Header("Color")]
    public Gradient ColorGradient;
    public MaterialColorController MaterialColorController;
    public float GradientStepMultiplayer;
    public float GradientStep = 0.03f;
    private float _currentGradientPosition;

    void Start()
    {
        _currentGradientPosition = Random.value;

        for (int i = 0; i < initialCount; i++)
        {
            Spawn(
            new Vector3(RandomSymmetric(zoneSize), Random.Range(-initialSpan, 0), RandomSymmetric(zoneSize)),
            new Vector3(RandomSymmetric(180), RandomSymmetric(180), RandomSymmetric(180)),
            new Vector3(RandomSymmetric(1), RandomSymmetric(1), RandomSymmetric(1)));
        }
        StartCoroutine(Drop());
    }

    private void Spawn(Vector3 position, Vector3 rotation, Vector3 angularVelocity)
    {
        var instance = Instantiate(domino, position + transform.position, Quaternion.Euler(rotation), transform);
        var rb = instance.GetComponent<Rigidbody>();
        rb.angularVelocity = angularVelocity;
    }

    private void Update()
    {
        _currentGradientPosition = (_currentGradientPosition + Time.deltaTime*GradientStepMultiplayer*GradientStep) % 1;
        // Получаем цвет из градиента
        Color gradientColor = ColorGradient.Evaluate(_currentGradientPosition);
        MaterialColorController.SetColor(gradientColor*15);
    }

    private IEnumerator Drop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            for (int i = 0; i < createdPerStep; i++)
            {
                Spawn(
                new Vector3(RandomSymmetric(zoneSize), 0, RandomSymmetric(zoneSize)),
                new Vector3(RandomSymmetric(180), RandomSymmetric(180), RandomSymmetric(180)),
                new Vector3(RandomSymmetric(1), RandomSymmetric(1), RandomSymmetric(1)));
            }
        }
    }

    private float RandomSymmetric(float max)
    {
        return Random.Range(-max, max);
    }
}
