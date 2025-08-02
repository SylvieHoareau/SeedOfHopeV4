using UnityEngine;
using TMPro;

public class EvasionHelpUI : MonoBehaviour
{
    public GameObject helpUI; // Texte à activer (à lier avec l'inspecteur)
    public float tempsAvantAffichage = 3f; // temps bloqué avant affichage
    public float tempsBloque = 0f;

    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        if (helpUI != null)
            helpUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null) return;

        // Vérifie si le joueur est quasi immobile
        if (rb.linearVelocity.magnitude < 0.01f)
        {
            tempsBloque += Time.deltaTime;

            if (tempsBloque >= tempsAvantAffichage && helpUI.activeSelf)
            {
                helpUI.SetActive(true);
            }
        }
        else
        {
            tempsBloque = 0f;

            if (helpUI != null && helpUI.activeSelf)
            {
                helpUI.SetActive(false);
            }
        }
    }
}
