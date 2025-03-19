using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DominoPiece : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    private void Reset()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rb.isKinematic = true;
    }

    public void Activate()
    {
        _rb.isKinematic = false;
    }
}
