using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image Tint;
    [SerializeField] private float fadeRate;
    [SerializeField] private int counter = 0;
    [SerializeField] private List<GameObject> dialogue;
    public void Play()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main");
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
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while (Tint.color.a > 0)
        {
            Tint.color = new Color(Tint.color.r, Tint.color.g, Tint.color.b, Mathf.Max(Tint.color.a - Time.deltaTime * fadeRate, 0));
            yield return new WaitForEndOfFrame();
        }
    }
}
