using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class CancelEventAggregator : MonoBehaviour
{
    public static CancelEventAggregator Instance;
    private bool invoking = false;
    private List<(Action, bool)> cached = new List<(Action, bool)>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Cancel()
    {
        invoking = true;
        fire?.Invoke();
        invoking = false;
        if (cached.Count > 0)
        {
            foreach (var action in cached)
            {
                if (action.Item2)
                {
                    fire += action.Item1;
                }
                else
                {
                    fire -= action.Item1;
                }
            }
            cached.Clear();
        }
    }

    public event Action OnCancel 
    { 
        add
        {
            if (!invoking)
            {
                fire += value;
            }
            else
            {
                cached.Add((value, true));
            }
        }
        remove
        {
            if (!invoking)
            {
                fire -= value;
            }
            else
            {
                cached.Add((value, false));
            }
        }
    }
    private event Action fire;
}
