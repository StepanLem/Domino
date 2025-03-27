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

    void Start()
    {
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
