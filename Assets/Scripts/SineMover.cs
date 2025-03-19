using UnityEngine;

public abstract class SineMover : MonoBehaviour
{
    private float t;
    [SerializeField] private float cycleDuration;
    [SerializeField] private float offset;
    private float sine => Mathf.Sin(offset + t / cycleDuration);

    public void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        Apply(sine);
    }

    protected abstract void Apply(float sine);
}
