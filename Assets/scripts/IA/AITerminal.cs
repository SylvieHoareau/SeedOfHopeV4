using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections; // Ajout de la bibliothèque pour les coroutines

// Ce script gère le terminal d'intellignece artificielle (IA) dans le jeu
// Il permet au joueur d'activer des zones en apportant les bonnes ressources
public class AITerminal : MonoBehaviour
{
    // Inventaire du joueur (pour vérifier les ressources)
    public Inventory playerInventory;

    [Header("Zones à revitaliser")]
    // Liste des zones qui seront activées si le joueur a assez de ressources
    public GameObject[] zonesARevitaliser;

    [Header("UI")]
    // Zone d'affichage des messages à l'écran
    public TextMeshProUGUI messageUI;
    // UI du terminal
    public GameObject terminalUI;

    [Header("Ressources requises")]
    // Nombre d'unités d'eau nécessaires
    public int besoinEau;
    // Nombre de graines nécessaires
    public int besoinGraines;
    // Nombre de fertiliant nécessaire
    public int besoinFertilisant;

    // Booléen pour savoir si l'objectif est atteint
    private bool objectifAtteint = false;

    // Indique si le joueur est dans la zone d'interaction avec le terminal
    private bool joueurDansZone = false;

    // Contrôles du joueur (pour détecter les actions)
    private PlayerControls controls;

    [Header("UI Objectif")]
    // Interface pour afficher l'objectif atteint
    public ObjectiveUI objectiveUI;

    // Permet de gérer l'action d'interaction
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> interactionAction;
    void Awake()
    {
        // Initialisation des contrôles du joueur
        controls = new PlayerControls();

        // On stocke l'action dans une variable pour pouvoir s'y désabonner
        interactionAction = ctx =>
        {
            if (joueurDansZone)
                ActiverIA();
        };
    }

    void OnEnable()
    {
        // Active les contrôles du joueur
        controls.Enable();
        // Abonnement à l'action d'interaction
        controls.Player.Interact.performed += interactionAction;
    }

    void OnDisable()
    {
        // On se désabonne de l'action d'interaction
        controls.Player.Interact.performed -= interactionAction;
        // Désactive les contrôles du joueur
        controls.Disable();
    }

    // Fonction appelée au début du jeu
    void Start()
    {
        // if (objectiveUI != null)
        //     objectiveUI.AfficherObjectif();
        // On récupère la référence du script ObjectiveUI
        if (objectiveUI == null)
        {
            objectiveUI = FindObjectOfType<ObjectiveUI>();
        }

        // On affiche le message d'introduction au début du jeu
        AfficherMessage("[ I.A. LOG] Bienvenue. Votre mission : sauver la planète");
    }

    // Ajout d'une fonction centrale pour afficher les messages
    // Cela rend le code plus propre et plus facile à maintenir
    public void AfficherMessage(string message)
    {
        if (messageUI != null)
        {
            messageUI.text = message;
        }
    }

    // Fonction qui vérifie si le joueur a assez de ressources pour activer les zones 
    void ActiverIA()
    {
        // Inventaire du joueur (pour vérifier les ressources)
        if (playerInventory == null) return;

        // On récupère le nombre de ressources du joueur en temps réel
        int eau = playerInventory.GetWaterDropCount();
        int graines = playerInventory.GetSeedCount();
        int fertil = playerInventory.GetFertilizerCount();

        // On vérifie si le joueur a toutes les ressources NECESSAIRES
        if (eau >= besoinEau && graines >= besoinGraines && fertil >= besoinFertilisant)
        {
            // Le joueur a les ressources nécessaires, on lance le processus
            // On active toutes les zones à revitaliser
            foreach (GameObject zone in zonesARevitaliser)
            {
                if (zone != null)
                    zone.SetActive(true);
            }

            // On affiche un message de succès à l'écran
            AfficherMessage("[ I.A. LOG] Ressources suffisantes. \nRevitalisation en cours... trouve la porte de sortie");

            // On désactive l'UI du terminal
            // if (terminalUI != null)
            // {
            //     terminalUI.SetActive(false);
            // }

            // Démarrage de la coroutine pour afficher le message puis désactiver l'UI
            StartCoroutine(AfficherMessageEtDesactiverUI());
        }
        else
        {
            // Si le joueur n'a pas assez de ressources
            AfficherMessage("[ I.A. LOG ] Ressources insuffisantes.\nAnalyse en attente...");
        }
    }
    
     // Nouvelle fonction (coroutine) pour gérer le message et la désactivation de l'UI
    private IEnumerator AfficherMessageEtDesactiverUI()
    {
        // On affiche le message de succès à l'écran
        AfficherMessage("[ I.A. LOG] Ressources suffisantes. \nRevitalisation en cours... trouve la porte de sortie");

        // On attend 3 secondes pour que le joueur ait le temps de lire
        yield return new WaitForSeconds(3.0f);

        // On désactive l'UI du terminal
        if (terminalUI != null)
        {
            terminalUI.SetActive(false);
        }
    }

    // Appeler cette fonction à chaque fois qu'une ressource est collectée
    public void AjouterRessource()
    {
        if (playerInventory == null) return;

        // On récupère le nombre de ressources du joueur en temps réel
        int eau = playerInventory.GetWaterDropCount();
        int graines = playerInventory.GetSeedCount();
        int fertil = playerInventory.GetFertilizerCount();

        // Si l'objectif n'est pas encore atteint et que les conditions sont remplis
        if (!objectifAtteint && eau >= besoinEau && graines >= besoinGraines && fertil >= besoinFertilisant)
        {
            // Les objectifs sont atteints
            objectifAtteint = true;

            // On appelle la fonction de l'ObjectiveUI
            if (objectiveUI != null)
            {
                objectiveUI.AfficherObjectifAtteint();
            }
        }
    }

    // Fonction appelée quand le joueur sort de la zone du terminal
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // On vérifie que le joueur se trouve dans la zone du terminal IA
            joueurDansZone = true;
            // On vérifie les ressources du joueur
            int eau = playerInventory.GetWaterDropCount();
            int graines = playerInventory.GetSeedCount();
            int fertil = playerInventory.GetFertilizerCount();

            if (eau >= besoinEau && graines >= besoinGraines && fertil >= besoinFertilisant)
            {
                AfficherMessage("[ I.A LOG ] Objectif atteint. Appuyer sur la touche A pour continuer.");
            }
            else
            {
                AfficherMessage("[ I.A. LOG] Appuyer sur la touche A pour interagir.");
            }
        }
    }

    // Fonction appelée quand le joueur sort de la zone du terminal
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = false;
            AfficherMessage("");
        }
    }
}