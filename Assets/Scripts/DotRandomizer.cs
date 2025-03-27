using System.Collections.Generic;
using UnityEngine;

public class DotRandomizer : MonoBehaviour
{
    [SerializeField] private List<Transform> Spots;
    [SerializeField] private List<GameObject> Dots;

    private void Start()
    {
        var random = new System.Random();
        foreach (var t in Spots)
        {
            var i = random.Next(0, Dots.Count - 1);
            var instance = Instantiate(Dots[i], t);
        }
    }
}
