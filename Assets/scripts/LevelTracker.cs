using UnityEngine;
using UnityEngine.SceneManagement;

// Ces script permet de mémoriser le niveau en cours dans le jeu.
// Il est attaché à l'objet SceneManagement dans la scène Unity
public class LevelTracker : MonoBehaviour
{

    // Cette fonction est appelée automatiquement au début du niveau
    void Start()
    {
        // On récupère le nom du niveau actuel
        string currentScene = SceneManager.GetActiveScene().name;
        // On sauvegarde ce nom pour s'en souvenir plus tard
        PlayerPrefs.SetString("LastPlayedLevel", currentScene);
        // On affiche dans la console le nom du niveau sauvegardé
        Debug.Log("NIveau actuel sauvegardé :" + currentScene);
    }

    // Cette fonction est appelé à chaque image du jeu
    void Update()
    {

    }
}
