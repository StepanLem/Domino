using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChainCounter : MonoBehaviour
{
    private int _counter;
    public UnityEvent<int> CounterChanged;
    public int Counter
    {
        get
        {
            return _counter;
        }
        set
        {
            _counter = value;
            CounterChanged?.Invoke(value);
        }
    }

    public void Increment()
    {
        Counter++;
    }

    public void SetValue(int value)
    {
        Counter = value;
    }
}