using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LeftToRightMover : SineMover
{
    [SerializeField] private float Amplitude;
    [SerializeField] private Rigidbody rb;
    private float cached;

    private void Reset()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        offset = (UnityEngine.Random.value - 0.5f) * 4 * Mathf.PI * cycleDuration;
        transform.position += Vector3.left * sine * Amplitude;
        cached = sine;
    }

    protected override void Apply(float sine)
    {
        var delta = sine - cached;
        cached = sine;
        transform.position += Vector3.left * delta * Amplitude;
    }

    private void OnEnable()
    {
        rb.useGravity = false;
    }

    private void OnDisable()
    {
        rb.useGravity = true;
        rb.linearVelocity = Vector3.zero;
    }
}
