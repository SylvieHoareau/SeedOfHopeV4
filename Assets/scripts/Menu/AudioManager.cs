using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------- Audio Source ---------")]
    [SerializeField] AudioSource musicSource;

    [Header("--------- Audio Clip ---------")]
    public AudioClip background;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

        private void Start()
        {
            musicSource.clip = background;
            musicSource.loop = true; // 🔁 ici on active la boucle
            musicSource.Play();
        }

}
