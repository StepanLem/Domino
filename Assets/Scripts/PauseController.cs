using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public UnityEvent OnPaused;
    public UnityEvent OnResumed;

    [SerializeField] private InputActionAsset _actionAsset;
    private bool _isOnPause;
    public bool IsOnPause
    {
        get
        {
            return _isOnPause;
        }
        set
        {
            if (_isOnPause == value)
            {
                return;
            }
            if (value)
            {
                _actionAsset.actionMaps[0].Disable();
                Time.timeScale = 0f;
                OnPaused?.Invoke();
            }
            else
            {
                _actionAsset.actionMaps[0].Enable();
                Time.timeScale = 1f;
                OnResumed?.Invoke();
            }    
            _isOnPause = value;
        }
    }
}
