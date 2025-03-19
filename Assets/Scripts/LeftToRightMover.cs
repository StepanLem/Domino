using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class LeftToRightMover : SineMover
{
    [SerializeField] private float Amplitude;
    private Rigidbody rb;
    private float cached;

    protected override void Apply(float sine)
    {
        var delta = sine - cached;
        cached = sine;
        rb.linearVelocity = Vector3.left * delta;
    }
}
