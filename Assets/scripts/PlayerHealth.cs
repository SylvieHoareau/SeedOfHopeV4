using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public bool isInvincible = false;
    public SpriteRenderer graphics;
    public int maxHealth = 100;
    public int currentHealth;
    public float InvincibilityTimeAfterHit = 3f; 
    public HealthBar healthBar;

    public GameObject gameOverUI;
    public float InvincibilityFlashDelay = 0.2f;

    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        PlayerPrefs.SetString("LastPlayedLevel", SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth = Math.Max(0, currentHealth - damage);
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibilityDelay());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Le joueur est mort !");

        // ✅ Sauvegarde du nom de la scène actuelle dans PlayerPrefs
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastPlayedLevel", currentSceneName);

        if (gameOverUI != null)
        {
            Debug.Log("UI trouvée ! Activation...");
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogWarning("gameOverUI n'est pas assigné !");
        }

        // ✅ Chargement de la scène GameOver après l'affichage de l'UI
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDelay()
    {
        yield return new WaitForSeconds(InvincibilityTimeAfterHit);
        isInvincible = false;
    }
}
