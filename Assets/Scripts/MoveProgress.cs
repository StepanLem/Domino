using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MoveProgress : MonoBehaviour
{
    [SerializeField] private float distancePerStep;
    [SerializeField] private float duration;
    private float distancePerFrame => (distancePerStep * Time.fixedDeltaTime) / duration;
    public bool IsStatic { get; private set; }

    private Vector3 MovementDirecation => Vector3.right;

    public void MoveOneStep()
    {
        IsStatic = true;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        float distance = 0;
        while (distance < distancePerStep)
        {
            var velocity = distancePerFrame * MovementDirecation;
            distance = Mathf.Min(distancePerStep, distance + velocity.magnitude);
            yield return new WaitForFixedUpdate();
        }
        IsStatic = false;
    }
}
