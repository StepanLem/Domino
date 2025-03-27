using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public UnityEvent OnFadeInCompleted;
    public UnityEvent OnFadeOutCompleted;
    [SerializeField] private Image Tint;
    [SerializeField] private float fadeRate;
    [SerializeField] private bool fadeOutOnStart;
    private bool actionStarted;

    public void StartFadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    public void StartFadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    private void Reset()
    {
        Tint = GetComponent<Image>();
    }

    private void Start()
    {
        if (fadeOutOnStart)
        {
            StartFadeOut();
        }
    }

    private IEnumerator FadeOut()
    {
        while (Tint.color.a > 0)
        {
            Tint.color = new Color(Tint.color.r, Tint.color.g, Tint.color.b, Mathf.Max(Tint.color.a - Time.unscaledDeltaTime * fadeRate, 0));
            yield return new WaitForEndOfFrame();
        }
        OnFadeOutCompleted?.Invoke();
    }

    private IEnumerator FadeIn()
    {
        while (Tint.color.a < 1)
        {
            Tint.color = new Color(Tint.color.r, Tint.color.g, Tint.color.b, Mathf.Min(Tint.color.a + Time.unscaledDeltaTime * fadeRate, 1));
            yield return new WaitForEndOfFrame();
        }
        OnFadeInCompleted?.Invoke();
    }

    public void SmoothSceneTransistion(string name)
    {
        if (actionStarted)
        {
            return;
        }
        OnFadeInCompleted.AddListener(() => SceneManager.LoadScene(name));
        StartFadeIn();
        actionStarted = true;
    }
}
