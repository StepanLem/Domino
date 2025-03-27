using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MoveProgress : MonoBehaviour
{
    [SerializeField] private float distancePerStep;
    [SerializeField] private float duration;
    private float distancePerFrame => (distancePerStep * Time.fixedDeltaTime) / duration;
    public bool IsStatic { get; private set; }

    private Vector3 MovementDirecation => Vector3.right;

    public void MoveOneStep(float deltaX)
    {
        IsStatic = false;
        StartCoroutine(Move(deltaX));
    }

    private IEnumerator Move(float x)
    {
        float distance = 0;
        var targetDistance = x + distancePerStep - transform.position.x;
        Debug.Log($"d: {x} td: {targetDistance}");
        while (distance < targetDistance)
        {
            var velocity = distancePerFrame * MovementDirecation;
            distance = Mathf.Min(targetDistance, distance + velocity.magnitude);
            transform.position += velocity;
            yield return new WaitForFixedUpdate();
        }
        IsStatic = true;
    }
}
