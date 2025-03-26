using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class UIElement : MonoBehaviour
{
    public UnityEvent OnHide;
    public UnityEvent OnShow;
    public bool HideOnStart = true;
    public void Show()
    {
        gameObject.SetActive(true);
        OnShow?.Invoke();
    }

    public void Hide() 
    {
        gameObject.SetActive(false);
        OnHide?.Invoke();
    }

    private void Start()
    {
        if (HideOnStart)
        {
            Hide();
        }
    }
}
