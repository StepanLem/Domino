using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Fade fade;
    [SerializeField] private float fadeRate;
    [SerializeField] private int counter = 0;
    [SerializeField] private List<GameObject> dialogue;
    [SerializeField] private TMP_Text maxScore;
    public void Play()
    {
        Time.timeScale = 1.0f;
        fade.SmoothSceneTransistion("Main");
    }

    public void Exit()
    {
        if (counter >= dialogue.Count)
        {
            Application.Quit();
            return;
        }
        dialogue[counter].SetActive(true);
        if (counter > 0)
        {
            dialogue[counter - 1].SetActive(false);
        }
        counter += 1;
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        maxScore.text = "Максимальный счёт: " + PlayerPrefs.GetInt("MaxScore", 0);
    }
}
