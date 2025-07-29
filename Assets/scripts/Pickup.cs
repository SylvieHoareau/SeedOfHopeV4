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

    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
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
