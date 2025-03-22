using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{
    [SerializeField] private float min;
    [SerializeField] private float max;
    private float delta => max - min;

    void Start()
    {
        transform.localScale = transform.localScale * (min + Random.value * delta);
    }
}
