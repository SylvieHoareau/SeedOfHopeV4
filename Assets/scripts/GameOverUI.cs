using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RetryGame()
    {
        string previousLevel = PlayerPrefs.GetString("LastPlayedLevel", "");

        if (!string.IsNullOrEmpty(previousLevel))
        {
            SceneManager.LoadScene(previousLevel);
        }
        else
        {
            // Cas de secours : recharge la scène GameOver si rien n’a été stocké
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitter le jeu");
    }
}
