using UnityEngine;
using TMPro;
using System.Collections;

// Ce script affiche le texte d'introduction lettre par lettre
public class IntroTypingEffect : MonoBehaviour
{
    // Zone où le texte d'intro sera affiché à l'écran
    public TextMeshProUGUI introText;

    // Liste des phrases à afficher au début du jeu
    [TextArea(4, 10)]
    public string[] lignesIntro;

    // Temps d'attente entre chaque pĥrase (en secondes)
    public float delayBetweenLines = 1f;
    // Vitesse d'apparition des lettres (plus petit = plus rapide)
    public float typingSpeed = 0.05f;

    void Start()
    {
        // Si la zone de texte existe et qu'il y a des phrases à afficher,
        // on commence à afficher les phrases une par une
        if (introText != null && lignesIntro.Length > 0)
            StartCoroutine(AfficherLignesUneParUne());
    }

    // Cette fonction affiche chaque phrase, une après l'autre
    IEnumerator AfficherLignesUneParUne()
    {
        // On vide la zone de texte avant de commencer
        introText.text = "";

        // Pour chaque phrase dans la liste ...
        foreach (string ligne in lignesIntro)
        {   
            // On affiche la phrase lettre par lettre
            yield return StartCoroutine(AfficherLettreParLettre(ligne));
            // On attend un peu avant d'afficher la suivante
            yield return new WaitForSeconds(delayBetweenLines);
            // ON passe à la ligne suivante dans la zone de texte
            introText.text += "\n";
        }
    }

    // Cette fonction affiche une phrase lettre par lettre.
    IEnumerator AfficherLettreParLettre(string ligne)
    {
        // POur chaque lettre de la phrase ...
        foreach (char lettre in ligne)
        {
            // On ajoute la lettre à la zone de texte
            introText.text += lettre;
            // On attend un petit moment avant d'ajouter la suivante
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
