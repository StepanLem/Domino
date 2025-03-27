using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DominoPiece : MonoBehaviour
{
    [SerializeField] public LeftToRightMover _rb;
    [SerializeField] public MaterialColorController _mcc;

    public Rigidbody Rigidbody;
    public Outline Outline;
    public UnityEvent OnHitGround;
    public UnityEvent OnPieceImpact;

    private bool _hadTouchedGround;
    private bool _wasTouchedByPreviousDomino;
    [NonSerialized] public bool IsInChain;
    [NonSerialized] public bool IsNowInCollisionWithPreviousDomino;
    [NonSerialized] public List<int> OffsetsToCurrentlyTouchedDominoes = new();

    public bool IsFrozen;
    public Vector3 LinearVelocityBeforeFrozen;
    public Vector3 AngularVelocityBeforeFrozen;

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
            _mcc.SetColor(collision.gameObject.GetComponent<Renderer>().material.color * 25);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Domino"))
        {
            collision.gameObject.TryGetComponent<DominoPiece>(out var otherDomino);
            int offsetToID = otherDomino.ID - this.ID;
            OffsetsToCurrentlyTouchedDominoes.Add(offsetToID);
            OnPieceImpact?.Invoke();

            MatchSystem.Instance.HandleDominoTouch(otherDomino, this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Domino"))
        {
            collision.gameObject.TryGetComponent<DominoPiece>(out var otherDomino);

            int offsetToID = otherDomino.ID - this.ID;
            if (OffsetsToCurrentlyTouchedDominoes.Contains(offsetToID))
                OffsetsToCurrentlyTouchedDominoes.Remove(offsetToID);
        }

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
