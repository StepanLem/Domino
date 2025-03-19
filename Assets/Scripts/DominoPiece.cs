using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DominoPiece : MonoBehaviour
{
    [SerializeField] public LeftToRightMover _rb;

    private bool _hadTouchedGround;

    private void Reset()
    {
        _rb = GetComponent<LeftToRightMover>();
    }

    private void Start()
    {
        _rb.enabled = true;
    }

    public void Activate()
    {
        _rb.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hadTouchedGround && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _hadTouchedGround = true;
            DominoTouchGroundHanlder.Instance.HandleNewDominoTouchGround(this);
        }  
    }
}
