using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset _actionAsset;
    [SerializeField] private UIElement self;
    public void Open()
    {
        if (!self.isActiveAndEnabled)
        {
            self.Show();
        }
        else
        {
            self.Hide();
        }
    }

    public void OnShow()
    {
        _actionAsset.actionMaps[0].Disable();
        Time.timeScale = 0f;
    }

    public void OnHide()
    {
        _actionAsset.actionMaps[0].Enable();
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Awake()
    {
        self.OnShow.AddListener(OnShow);
        self.OnHide.AddListener(OnHide);
    }
}
