using UnityEngine;

public class DownMovement : MonoBehaviour
{
    [field: SerializeField] public float Speed {  get; set; }
    private void FixedUpdate()
    {
        transform.position += Vector3.down * Time.fixedDeltaTime * Speed;
    }
}
