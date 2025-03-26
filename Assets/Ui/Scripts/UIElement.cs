using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class UIElement : MonoBehaviour
{
    private UIElement obstructed;
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
        if (obstructed != null)
        {
            obstructed.Show();
            obstructed = null;
        }
    }

    public void ShowOver(UIElement other)
    {
        obstructed = other;
        Show();
    }

    private void Start()
    {
        if (HideOnStart)
        {
            Hide();
        }
    }
}
