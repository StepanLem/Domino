using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIElement self;
    [SerializeField] private CancelEvent cancelEvent;
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
        SceneManager.LoadScene("MainMenu");
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
