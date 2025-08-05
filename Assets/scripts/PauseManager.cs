using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Référence vers le GameObject du menu de pause dans l'interface utilisateur
    [SerializeField] private GameObject pauseMenuUI;

    // Détermine si le jeu est en pause.
    public static bool isGamePaused = false;

    // Appelé à chaque frame
    void Update()
    {
        // Détecte si le bouton "Start" de la manette est pressé.
        // Le KeyCode.JoystickButton7 correspond généralement au bouton "Start"
        // sur une manette Xbox.
        if (Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Cache le menu de pause.
        pauseMenuUI.SetActive(false);
        // Remet le temps à la vitesse normale.
        Time.timeScale = 1f;
        // Met à jour l'état de la pause.
        isGamePaused = false;
    }

    public void Pause()
    {
        // Affiche le menu de pause.
        pauseMenuUI.SetActive(true);
        // Met le jeu en pause en arrêtant l'écoulement du temps.
        Time.timeScale = 0f;
        // Met à jour l'état de la pause.
        isGamePaused = true;
    }

    public void Home()
    {
        // Reprend le temps avant de changer de scène.
        Time.timeScale = 1f;
        // Charge la scène du menu principal.
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        // Reprend le temps avant de recharger la scène.
        Time.timeScale = 1f;
        // Recharge la scène actuelle.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}