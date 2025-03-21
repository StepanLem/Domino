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


    public List<DominoPiece> Dominoes; //упорядоченный список домино слева направо.

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
        else if (Dominoes.Count > CountOfDominoBetweenFrozenAndActiveDomino)
        {
            //МБ перенести это на "При бросске?"

            if (FirstFrozenDomino == null || SecondFrozenDomino == null)
            {
                Debug.LogError("Нельзя давать игроку ставить новые фигуры, пока старые не коснулись");
                return;
            }

            UnfreezeDominoes();

            //TODO: отключаем шейдер, делающий обводку для обоих

            //Аудио
            //TODO: звук отмораживания.
        }
    }

    private DominoPiece FirstFrozenDomino;
    private DominoPiece SecondFrozenDomino;

    //запомненная скорость двух замороженных домино.
    private Vector3 _FirstFrozenDominoLinearVelocity;
    private Vector3 _FirstFrozenDominoAngularVelocity;
    private Vector3 _SecondFrozenDominoLinearVelocity;
    private Vector3 _SecondFrozenDominoAngularVelocity;

    public void HandleDominoFirstTouchWithPrevious(DominoPiece previousDomino, DominoPiece domino)
    {
        if (previousDomino.ID != lastConnectedDominoId)
        {
            Debug.Log("Вот этой фигни быть не должно. Неправильно будет работать при следующем взаимодействии. " +
                "Рассмотрите как это произошло?");
            //скорее всего это произошло если игрок соеденил домино в середине игры сам. А значит мог уронить.
            //+ тут внутри переменные сохраняют состояние. может быть ошибка.
            return;
        }
            
        FreezeDominoes(previousDomino, domino);

        //Увеличиваем счётчик коснувшихся домино.
        lastConnectedDominoId++;

        //Визуально выделяем две доминошки
        //TODO: включить шейдер, делающий обводку для обоих

        //Аудио
        //TODO: звук соприкосновения доминошек. И звук замораживания(как в зельде)
    }




    public void FreezeDominoes(DominoPiece previousDomino, DominoPiece domino)
    {
        FirstFrozenDomino = previousDomino;
        SecondFrozenDomino = domino;

        //запоминаем скорость перед заморозкой домино.
        _FirstFrozenDominoLinearVelocity = previousDomino.Rigidbody.linearVelocity;
        _FirstFrozenDominoAngularVelocity = previousDomino.Rigidbody.angularVelocity;
        _SecondFrozenDominoLinearVelocity = previousDomino.Rigidbody.linearVelocity;
        _SecondFrozenDominoAngularVelocity = previousDomino.Rigidbody.angularVelocity;

        //замораживаем
        previousDomino.Rigidbody.isKinematic = true;
        domino.Rigidbody.isKinematic = true;
    }

    public void UnfreezeDominoes()
    {
        FirstFrozenDomino.Rigidbody.isKinematic = false;
        SecondFrozenDomino.Rigidbody.isKinematic = false;

        FirstFrozenDomino.Rigidbody.linearVelocity = _FirstFrozenDominoLinearVelocity;
        FirstFrozenDomino.Rigidbody.angularVelocity = _FirstFrozenDominoAngularVelocity;
        SecondFrozenDomino.Rigidbody.linearVelocity = _SecondFrozenDominoLinearVelocity;
        SecondFrozenDomino.Rigidbody.angularVelocity = _SecondFrozenDominoAngularVelocity;

        FirstFrozenDomino = null;
        SecondFrozenDomino = null;
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
