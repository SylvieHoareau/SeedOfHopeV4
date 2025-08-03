using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.UI.Enable();
    }

    void OnDisable()
    {
        controls.UI.Disable(); // ← IMPORTANT
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("IntroScene");
    }

    public void QuitGame()
    {
        Application.Quit();
   }

}
