using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIElement self;
    [SerializeField] private CancelEvent cancelEvent;
    [SerializeField] private Fade fade;
    public void Open()
    {
        if (!self.isActiveAndEnabled)
        {
            self.Show();
        }
        else
        {
            self.Hide();
            Resume();
        }
    }

    public void GoToMenu()
    {
        fade.SmoothSceneTransistion("MainMenu");
    }

    public void Restart()
    {
        fade.SmoothSceneTransistion("Main");
    }

    public void Resume()
    {
        PauseController.Instance.IsOnPause = false;
    }

    private void Awake()
    {
        cancelEvent.OnCancel.AddListener(self.Hide);
        cancelEvent.OnCancel.AddListener(Resume);
    }
}
