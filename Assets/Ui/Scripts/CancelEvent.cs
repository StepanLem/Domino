using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class CancelEvent : MonoBehaviour
{
    public UnityEvent OnCancel;
    private bool assigned = false;

    public void React()
    {
        OnCancel?.Invoke();
    }

    private void Start()
    {
        if (!assigned)
        {
            CancelEventAggregator.Instance.OnCancel += React;
        }
    }

    private void OnEnable()
    {
        if (CancelEventAggregator.Instance != null)
        {
            CancelEventAggregator.Instance.OnCancel += React;
            assigned = true;
        }
    }

    private void OnDisable()
    {
        if (CancelEventAggregator.Instance != null)
        {
            CancelEventAggregator.Instance.OnCancel -= React;
            assigned = false;
        }
    }
}