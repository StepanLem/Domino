using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SmoothCounter : MonoBehaviour
{
    public UnityEvent OnCountingFinished;
    [SerializeField] private string label;
    [SerializeField] private TMP_Text textObject;
    [SerializeField] private float duration;
    [SerializeField] private float minDelta;
    private float displayedCount;
    private float targetCount;
    private float velocity;
    private bool isCounting;

    public void SetTarget(float value)
    {
        targetCount = value;
    }

    public void StartCounting()
    {
        displayedCount = 0;
        isCounting = true;
        enabled = true;
        velocity = 0;
    }

    private void Update()
    {
        if (!isCounting)
        {
            enabled = false;
            return;
        }
        if (displayedCount < targetCount - minDelta)
        {
            displayedCount = Mathf.SmoothDamp(displayedCount, targetCount, ref velocity, duration);
            UpdateText();
        }
        else
        {
            displayedCount = targetCount;
            UpdateText();
            OnCountingFinished?.Invoke();
            isCounting = false;
        }
    }

    private void UpdateText()
    {
        textObject.text = label + Convert.ToInt32(displayedCount).ToString();
    }
}
