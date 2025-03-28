using System.Collections;
using UnityEngine;

public class HintTimer : MonoBehaviour
{
    [SerializeField] private GameObject go;
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        go.SetActive(true);
    }
}
