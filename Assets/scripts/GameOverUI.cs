using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void RetryGame()
    {
        string previousLevel = PlayerPrefs.GetString("LastPlayedLevel", "");

        Debug.Log("RetryGame() appelé. Dernier niveau : " + previousLevel);

        if (!string.IsNullOrEmpty(previousLevel))
        {
            SceneManager.LoadScene(previousLevel);
        }
        else
        {
            Debug.LogWarning("Aucune scène sauvegardée, rechargement de la scène actuelle.");
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
