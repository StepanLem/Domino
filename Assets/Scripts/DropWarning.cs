using System.Collections;
using UnityEngine;

public class DropWarning : MonoBehaviour
{
    [SerializeField] private GameObject go;
    [SerializeField] private float interval;
    [SerializeField] private float duration;

    public void OnEnable()
    {
        StartCoroutine(Cycle());
    }

    public void OnDisable()
    {
        StopAllCoroutines();
        go.SetActive(false);
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            go.SetActive(true);
            yield return new WaitForSeconds(duration);
            go.SetActive(false);
            yield return new WaitForSeconds(interval);
        }
    }
}
