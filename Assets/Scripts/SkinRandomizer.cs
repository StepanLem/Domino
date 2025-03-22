using UnityEngine;
using System.Collections.Generic;

public class SkinRandomizer : MonoBehaviour
{
    [SerializeField] private List<Material> texturePool = new List<Material>();

    [SerializeField] private MeshRenderer planeFront;
    [SerializeField] private MeshRenderer planeBack;

    private void Start()
    {
        SetRandomTexture(planeFront);
        SetRandomTexture(planeBack);
    }

    private void SetRandomTexture(MeshRenderer mesh)
    {
        var i = Random.Range(0, texturePool.Count - 1);
        var materials = new List<Material>() { texturePool[i] };
        mesh.SetMaterials(materials);
    }
}
