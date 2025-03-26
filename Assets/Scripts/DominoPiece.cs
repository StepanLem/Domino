using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class DominoPiece : MonoBehaviour
{
    [SerializeField] public LeftToRightMover _rb;

    public Rigidbody Rigidbody;
    public Outline Outline;
    public UnityEvent OnHitGround;
    public UnityEvent OnPieceImpact;

    private bool _hadTouchedGround;
    private bool _wasTouchedByPreviousDomino;
    public bool IsNowInCollisionWithPreviousDomino;

    public int ID;

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
            MatchSystem.Instance.HandleNewDominoTouchGround(this);
            OnHitGround?.Invoke();
        }

        if (!_wasTouchedByPreviousDomino && collision.gameObject.layer == LayerMask.NameToLayer("Domino"))
        {
            collision.gameObject.TryGetComponent<DominoPiece>(out var previousDomino);
            if (previousDomino.ID + 1 == this.ID)
            {
                IsNowInCollisionWithPreviousDomino = true;
                _wasTouchedByPreviousDomino = true;
                MatchSystem.Instance.HandleDominoFirstTouchWithPrevious(previousDomino, this);
                OnPieceImpact?.Invoke();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!IsNowInCollisionWithPreviousDomino && collision.gameObject.layer == LayerMask.NameToLayer("Domino"))
        {
            collision.gameObject.TryGetComponent<DominoPiece>(out var previousDomino);
            if (previousDomino.ID + 1 == this.ID)
            {
                IsNowInCollisionWithPreviousDomino = true;
                OnPieceImpact?.Invoke();
            }
        }
    }
}
