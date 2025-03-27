using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MCC", menuName = "Material Color Controller")]
public class MaterialColorController : ScriptableObject
{
    [SerializeField] private Material material;
    [SerializeField] private string colorVariable;
    public void SetColor(Color color)
    {
        material.SetColor(colorVariable, color);
    }
}
