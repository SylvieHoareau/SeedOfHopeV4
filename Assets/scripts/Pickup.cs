using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum ItemType
    {
        WaterDrop,
        Seed,
        Fertilizer
    }

    public ItemType itemType = ItemType.WaterDrop;

    [Header("Audio")]
    public AudioClip pickupSound; // À assigner dans l'inspecteur
    private AudioSource audioSource;

    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;

        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.up * 0.5f, Color.red);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Quelque chose est entrée : " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("C’est le joueur !");
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                string itemName = ConvertItemTypeToName(itemType);
                inventory.AddItem(itemName);

                // Joue le son de collecte à l'endroit exacte de la collecte
                if (pickupSound != null && audioSource != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                Destroy(gameObject); // Détruire la goutte après collecte
            }
        }
    }

    private string ConvertItemTypeToName(ItemType type)
    {
        switch (type)
        {
            case ItemType.WaterDrop:
                return "Water Drop";
            case ItemType.Seed:
                return "Seed";
            case ItemType.Fertilizer:
                return "Fertilizer";
            default:
                return "Unknown";
        }
    }
}
