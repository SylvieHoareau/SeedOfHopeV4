using UnityEngine;
using TMPro;

public class RobotFurtif : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    private bool isPlayerInRange = false;
    private Inventory playerInventory;

    public TextMeshProUGUI warningUI;
    public AudioClip voleurSound;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();

        // Affiche ou cache l'alerte
    }

    void Patrol()
    {
        
    }
}
