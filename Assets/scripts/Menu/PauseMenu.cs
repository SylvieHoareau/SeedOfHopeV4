using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public bool IsPaused;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void Update()
    {
        // Détecte l'appui sur le bouton Start
        // La détection des boutons de manettes peut varier
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (pauseMenu.activeSelf)
            {
                // Si le menu pause est déjà actif, on le désactive et on 
                Resume();
            }
            else
            {
                // Sinon, on met le jeu en pause et on active le menu
                Pause();
            }
        }
    }

}