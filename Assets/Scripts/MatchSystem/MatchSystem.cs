using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSystem : MonoBehaviour
{
    public static MatchSystem Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public List<DominoPiece> Dominoes; //������������� ������ ������ ����� �������.

    public int lastConnectedDominoId;

    public static int CountOfDominoBetweenFrozenAndActiveDomino = 5;

    public void HandleNewDominoTouchGround(DominoPiece activeDomino)
    {
        Dominoes.Add(activeDomino);
        activeDomino.ID = Dominoes.Count - 1;
        
        if(Dominoes.Count == 1)
        {
            lastConnectedDominoId = 0;
        }
        if (Dominoes.Count == CountOfDominoBetweenFrozenAndActiveDomino)
        {
            var forcePosition = Dominoes[0].transform.position + new Vector3(0, 0.5f, 0);
            var direction = new Vector3(1, 0, 0);
            var force = 50f;
            Dominoes[0].Rigidbody.AddForceAtPosition(direction * force, forcePosition);
        }
        
    }

    public void TryUnfreezeDominos()
    {
        if (Dominoes.Count > CountOfDominoBetweenFrozenAndActiveDomino)
        {
            if (FirstFrozenDomino == null || SecondFrozenDomino == null)
            {
                Debug.LogError("������ ������ ������ ������� ����� ������, ���� ������ �� ���������");
                return;
            }

            UnfreezeDominoes(FirstFrozenDomino, SecondFrozenDomino);

            //��������� ������, �������� ������� ��� �����
            FirstFrozenDomino.Outline.enabled = false;
            SecondFrozenDomino.Outline.enabled = false;

            //�����
            //TODO: ���� �������������.


            FirstFrozenDomino = null;
            SecondFrozenDomino = null;
        }
    }

    private DominoPiece FirstFrozenDomino;
    private DominoPiece SecondFrozenDomino;

    //����������� �������� ���� ������������ ������.
    private Vector3 _FirstFrozenDominoLinearVelocity;
    private Vector3 _FirstFrozenDominoAngularVelocity;
    private Vector3 _SecondFrozenDominoLinearVelocity;
    private Vector3 _SecondFrozenDominoAngularVelocity;

    public void HandleDominoFirstTouchWithPrevious(DominoPiece previousDomino, DominoPiece domino)
    {
        if (previousDomino.ID != lastConnectedDominoId)
        {
            Debug.Log("��� ���� ����� ���� �� ������. ����������� ����� �������� ��� ��������� ��������������. " +
                "����������� ��� ��� ���������?");
            //������ ����� ��� ��������� ���� ����� �������� ������ � �������� ���� ���. � ������ ��� �������.
            //+ ��� ������ ���������� ��������� ���������. ����� ���� ������.
            return;
        }

        FirstFrozenDomino = previousDomino;
        SecondFrozenDomino = domino;

        FreezeDominoes(FirstFrozenDomino, SecondFrozenDomino);

        //����������� ������� ����������� ������.
        lastConnectedDominoId++;

        //��������� �������� ��� ���������
        FirstFrozenDomino.Outline.enabled = true;
        SecondFrozenDomino.Outline.enabled = true;

        //�����
        //TODO: ���� ��������������� ���������. � ���� �������������(��� � ������)
    }




    public void FreezeDominoes(DominoPiece previousDomino, DominoPiece domino)
    {
        //���������� �������� ����� ���������� ������.
        _FirstFrozenDominoLinearVelocity = previousDomino.Rigidbody.linearVelocity;
        _FirstFrozenDominoAngularVelocity = previousDomino.Rigidbody.angularVelocity;
        _SecondFrozenDominoLinearVelocity = previousDomino.Rigidbody.linearVelocity;
        _SecondFrozenDominoAngularVelocity = previousDomino.Rigidbody.angularVelocity;

        //������������
        previousDomino.Rigidbody.isKinematic = true;
        domino.Rigidbody.isKinematic = true;
    }

    public void UnfreezeDominoes(DominoPiece previousDomino, DominoPiece domino)
    {
        previousDomino.Rigidbody.isKinematic = false;
        domino.Rigidbody.isKinematic = false;

        previousDomino.Rigidbody.linearVelocity = _FirstFrozenDominoLinearVelocity;
        previousDomino.Rigidbody.angularVelocity = _FirstFrozenDominoAngularVelocity;
        domino.Rigidbody.linearVelocity = _SecondFrozenDominoLinearVelocity;
        domino.Rigidbody.angularVelocity = _SecondFrozenDominoAngularVelocity;
    }





    #region CameraLogic(didnt used)
    public Transform Camera;

    public Transform CurrentDominoSpawn;

    public Vector3 OffsetBetweenDominoSpawnPoints;

    public Vector3 CameraOffsetFromDominoSpawn;

    public void CameraGoToNexDominoSpawn()
    {
        var nextDominoSpawnPosition = CurrentDominoSpawn.position + OffsetBetweenDominoSpawnPoints;
        CurrentDominoSpawn.position = nextDominoSpawnPosition;
        var nextCameraPosition = nextDominoSpawnPosition + CameraOffsetFromDominoSpawn;

        StartCoroutine(CameraGoSmoothlyTo(nextCameraPosition));
    }

    public float CameraMoveSpeed;
    public IEnumerator CameraGoSmoothlyTo(Vector3 TargetCameraPosition)
    {

        Vector3 startPosition = Camera.position;
        float journeyLength = Vector3.Distance(startPosition, TargetCameraPosition);
        float startTime = Time.time;
        float distanceCovered = 0f;
        float fracJourney = 0f;

        while (fracJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * CameraMoveSpeed;
            fracJourney = distanceCovered / journeyLength;

            Camera.position = Vector3.Lerp(startPosition, TargetCameraPosition, fracJourney);
            yield return null; // Wait for the next frame
        }

        Camera.position = TargetCameraPosition;
    }
    #endregion
}
