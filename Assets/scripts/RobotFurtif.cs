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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Patrol();

        // Affiche ou cache l’alerte
        if (warningUI != null && isPlayerInRange)
        {
            warningUI.text = "⚠️ Un robot furtif vous a volé des ressources !";
        }
        else if (warningUI != null)
        {
            warningUI.text = "";
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerInventory = other.GetComponent<Inventory>();

            if (playerInventory != null)
            {
                VoleRessource();
            }

            if (voleurSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(voleurSound);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void VoleRessource()
    {
        int choix = Random.Range(0, 3);
        switch (choix)
        {
            case 0:
                if (playerInventory.GetWaterDropCount() > 0)
                {
                    playerInventory.RemoveItem("Water Drop");
                    Debug.Log("Le robot a volé une goutte d'eau !");
                }
                break;
            case 1:
                if (playerInventory.GetSeedCount() > 0)
                {
                    playerInventory.RemoveItem("Seed");
                    Debug.Log("Le robot a volé une graine !");
                }
                break;
            case 2:
                if (playerInventory.GetFertilizerCount() > 0)
                {
                    playerInventory.RemoveItem("Fertilizer");
                    Debug.Log("Le robot a volé un engrais !");
                }
                break;
        }
    }
}
