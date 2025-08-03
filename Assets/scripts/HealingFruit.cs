using UnityEngine;

public class HealingFruit : MonoBehaviour
{
    public int healingAmount = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healingAmount);
                Destroy(gameObject);
                Debug.Log("🍓 Soin reçu : +" + healingAmount + " PV");
            }
        }
    }
}
