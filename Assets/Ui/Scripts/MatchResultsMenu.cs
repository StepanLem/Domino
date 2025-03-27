using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MatchResultsMenu : MonoBehaviour
{
    public UnityEvent OnNewMaxScore;
    [SerializeField] private SmoothCounter distance;
    [SerializeField] private SmoothCounter chain;
    [SerializeField] private SmoothCounter score;
    [SerializeField] private Transform distanceMeasure;
    [SerializeField] private Fade fade;
    [SerializeField] private ChainCounter counter;
    private bool actionStarted = false;
    public void GoToMenu()
    {
        fade.SmoothSceneTransistion("MainMenu");
    }

    public void Restart()
    {
        fade.SmoothSceneTransistion("Main");
    }

    public void LoadValues()
    {
        var distanceValue = distanceMeasure.position.x;
        var chainValue = counter.Counter;
        var scoreValue = distanceValue * chainValue;
        distance.SetTarget(distanceValue);
        chain.SetTarget(chainValue);
        score.SetTarget(scoreValue);
        var maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        if (scoreValue > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", Convert.ToInt32(scoreValue));
            OnNewMaxScore?.Invoke();
        }
        PlayerPrefs.Save();
    }

    public void StartCountdownWithDelay(float delay)
    {
        StartCoroutine(Delay(delay));
    }

    private IEnumerator Delay(float amount)
    {
        yield return new WaitForSeconds(amount);
        distance.StartCounting();
    }
}
