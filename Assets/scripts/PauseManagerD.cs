using UnityEngine;

public class PauseInput : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (pauseMenu != null)
            {
                if (pauseMenu.IsPaused)
                    pauseMenu.Resume();
                else
                    pauseMenu.Pause();
            }
        }
    }
}
