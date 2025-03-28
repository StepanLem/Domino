using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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


    public List<DominoPiece> Dominoes; //упорядоченный список домино слева направо.

    [NonSerialized] public int lastConnectedDominoId;

    public static int CountOfDominoBetweenFrozenAndActiveDomino = 5;

    public event Action DominoAllowedSpawnInArm;

    public UnityEvent<int> CounterIncrement;

    [SerializeField] private float minLinearVelocity;
    [SerializeField] private float minAngularVelocity;

    public void HandleNewDominoTouchGround(DominoPiece activeDomino)
    {
        Dominoes.Add(activeDomino);
        activeDomino.ID = Dominoes.Count - 1;

        if (Dominoes.Count == 1)
        {
            lastConnectedDominoId = 0;
            activeDomino.IsInChain = true;
        }
        if (Dominoes.Count == CountOfDominoBetweenFrozenAndActiveDomino)
        {
            var forcePosition = Dominoes[0].transform.position + new Vector3(0, 0.5f, 0);
            var direction = new Vector3(1, 0, 0);
            var force = 50f;
            Dominoes[0].Rigidbody.AddForceAtPosition(direction * force, forcePosition);
        }

    }

    public List<DominoPiece> FrozenDominoes;

    public void TryUnfreezeDominos()
    {
        if (Dominoes.Count >= CountOfDominoBetweenFrozenAndActiveDomino)
        {
            if (FrozenDominoes.Count != 2)
                return;

            var domino1 = FrozenDominoes[0];
            var domino2 = FrozenDominoes[1];
            UnfreezeDominoes(domino1, domino2);

            //отключаем шейдер, делающий обводку для обоих
            domino1.Outline.enabled = false;
            domino2.Outline.enabled = false;

            //Аудио
            //TODO: звук отмораживания.

            FrozenDominoes.Remove(domino1);
            FrozenDominoes.Remove(domino2);
        }
    }

    public Dictionary<int, int> frozenDominoContainsTimesInFrozenPairsByID = new();
    public List<(DominoPiece, DominoPiece)> NextPairToFrezze = new();


    public UnityEvent OnFreeze;
    
    public void HandleDominoTouch(DominoPiece Domino1, DominoPiece Domino2)
    {
        while (true)
        {
            if (FrozenDominoes.Count > 2)
            {
                UnfreezDomino(FrozenDominoes[0]);
                FrozenDominoes[0].Outline.enabled = false;
                FrozenDominoes.RemoveAt(0);
            }
            else break;

        }
        if (Domino1.IsInChain && !Domino2.IsInChain)
        {
            FreezeDominoes(Domino1, Domino2);

            Domino1.Outline.enabled = true;
            Domino2.Outline.enabled = true;

            OnFreeze?.Invoke();

            Domino2.IsInChain = true;

            FrozenDominoes.Add(Domino1);
            FrozenDominoes.Add(Domino2);

            //Увеличиваем счётчик коснувшихся домино.
            lastConnectedDominoId = Domino2.ID;
            Debug.Log("Очень ненадёжный счётчик, что если коснулись домино откуда-то позади? Или домино стали сталкиваться в обратном направлении?");
            CounterIncrement?.Invoke(lastConnectedDominoId);

            DominoAllowedSpawnInArm?.Invoke();
        }
    }

    private void UnfreezDomino(DominoPiece Domino)
    {
        Domino.Rigidbody.isKinematic = false;
        Domino.Rigidbody.linearVelocity = Domino.LinearVelocityBeforeFrozen;
        Domino.Rigidbody.angularVelocity = Domino.AngularVelocityBeforeFrozen;
    }

    public void FreezeDominoes(DominoPiece Domino1, DominoPiece Domino2)
    {
        //запоминаем скорость перед заморозкой домино.
        Domino1.LinearVelocityBeforeFrozen = Domino1.Rigidbody.linearVelocity;
        Domino1.AngularVelocityBeforeFrozen = Domino1.Rigidbody.angularVelocity;
        Domino2.LinearVelocityBeforeFrozen = Domino2.Rigidbody.linearVelocity;
        Domino2.AngularVelocityBeforeFrozen = Domino2.Rigidbody.angularVelocity;

        //замораживаем
        Domino1.Rigidbody.isKinematic = true;
        Domino2.Rigidbody.isKinematic = true;
    }

    public void UnfreezeDominoes(DominoPiece Domino1, DominoPiece Domino2)
    {
        Domino1.Rigidbody.isKinematic = false;
        Domino2.Rigidbody.isKinematic = false;

        DominoPiece right = null;
        if (Domino1.transform.position.x > Domino2.transform.position.x)
        {
            right = Domino1;
        }
        else
        {
            right = Domino2;
        }

        if (right.AngularVelocityBeforeFrozen.magnitude < minAngularVelocity)
        {
            var forcePosition = Dominoes[0].transform.position + new Vector3(0, 0.5f, 0);
            var direction = new Vector3(1, 0, 0);
            var force = 15f;
            right.Rigidbody.AddForceAtPosition(direction * force, forcePosition);
        }

        Debug.Log("a: " + right.AngularVelocityBeforeFrozen.magnitude + "l: " + right.LinearVelocityBeforeFrozen.magnitude);

        Domino1.Rigidbody.linearVelocity = Domino1.LinearVelocityBeforeFrozen;
        Domino1.Rigidbody.angularVelocity = Domino1.AngularVelocityBeforeFrozen;
        Domino2.Rigidbody.linearVelocity = Domino2.LinearVelocityBeforeFrozen;
        Domino2.Rigidbody.angularVelocity = Domino2.AngularVelocityBeforeFrozen;
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
