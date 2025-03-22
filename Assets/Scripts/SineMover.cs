using UnityEngine;

public abstract class SineMover : MonoBehaviour
{
    public float t { get; protected set; }
    [SerializeField] public float cycleDuration;
    [SerializeField] public float offset;
    public float sine => Mathf.Sin((offset + t) / cycleDuration);

    public void FixedUpdate()
    {
        t += Time.fixedDeltaTime;
        Apply(sine);
    }

    protected abstract void Apply(float sine);
}
