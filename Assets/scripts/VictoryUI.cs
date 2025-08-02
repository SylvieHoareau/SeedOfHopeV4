using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{
    void Start()
    {
        // Nettoie la sauvegarde du dernier niveau atteint
        PlayerPrefs.DeleteKey("LastPlayedLevel");
        Debug.Log("Progression réinitialisée.");
    }
    public void Rejouer()
    {
        SceneManager.LoadScene(0); // ouvre la scène "Menu"
    }

    public void Quitter()
    {
        Application.Quit();
        Debug.Log("Le joueur quitte le jeu.");
    }
}
