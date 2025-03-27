using UnityEngine;
using UnityEngine.Events;

public class DefeatTimer : MonoBehaviour
{
    private float exceedsAt;
    [SerializeField] private float duration;
    public UnityEvent OnTimerFire;
    public void StartCountDown()
    {
        enabled = true;
        exceedsAt = Time.time + duration;
    }

    public void ResetCountDown()
    {
        enabled = false;
    }

    private void Start()
    {
        ResetCountDown();
    }

    private void Update()
    {
        if (Time.time > exceedsAt)
        {
            OnTimerFire?.Invoke();
            ResetCountDown();
        }
    }
}
