using UnityEngine;
using TMPro;

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

    [Header("Ressources requises")]
    // Nombre d'unités d'eau nécessaires
    public int besoinEau = 2;
    // Nombre de graines nécessaires
    public int besoinGraines = 2;
    // Nombre de fertiliant nécessaire
    public int besoinFertilisant = 1;

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
        // Quand le joueur appuie sur la touche d'interaction, on vérifie s'il est dans la zone
        controls.Player.Interact.performed += ctx =>
        {
            if (joueurDansZone)
                ActiverIA();
        };
        // Abonnement à l'action d'interaction
        controls.Player.Interact.performed += interactionAction;
    }

    void OnDisable()
    {
        // On se désabonne de l'action d'interaction
        controls.Player.Interact.performed -= ctx =>
        {
            if (joueurDansZone)
                ActiverIA();
        };
        // Désabonnement
        controls.Player.Interact.performed -= interactionAction;
        // Désactive les contrôles du joueur
        controls.Disable();
    }

    // Fonction appelée au début du jeu
    void Start()
    {
        // if (objectiveUI != null)
        //     objectiveUI.AfficherObjectif();
    }

    // Update is called once per frame
    void Update()
    {
        if (joueurDansZone && Input.GetKeyDown(KeyCode.E))
        {
            ActiverIA();
        }
    }

    // Fonction qui vérifie si le joueur a assez de ressources pour activer les zones 
    void ActiverIA()
    {
        if (playerInventory == null) return;

        // On récupère le nombre de ressources du joueur
        int eau = playerInventory.GetWaterDropCount();
        int graines = playerInventory.GetSeedCount();
        int fertil = playerInventory.GetFertilizerCount();

        Debug.Log($"[DEBUG] Inventaire : Eau={eau}, Graines={graines}, Fertilisant={fertil}");

        // Si le joueur a assez de chaque ressource...
        if (eau >= besoinEau && graines >= besoinGraines && fertil >= besoinFertilisant)
        {
            // On active toutes les zones à revitaliser
            foreach (GameObject zone in zonesARevitaliser)
            {
                if (zone != null)
                    zone.SetActive(true);
            }

            // On affiche un message de succès à l'écran
            if (messageUI != null)
                messageUI.text = "[ I.A LOG ] Ressources suffisantes.\nRevitalisation en cours ... trouvez la porte de sortie";

            // On indique que l'objectif est atteint
            if (objectiveUI != null)
                objectiveUI.AfficherObjectifAtteint();

        }
        else
        {
            // Si le joueur n'a pas assez de ressources, on affiche un message d'erreur
            messageUI.text = "[ I.A LOG ] Ressources insuffisantes.\nAnalyse en attente...";
        }
    }

    // Fonction appelée quand le joueur sort de la zone du terminal
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = true;
            if (messageUI != null)
                // Le joueur appuie sur la touche A de la console pour interagir avec l'IA
                messageUI.text = "[ I.A LOG ] Appuyer sur la touche A pour interagir.";
        }
    }

    // Fonction appelée quand le joueur sort de la zone du terminal
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = false;
            if (messageUI != null)
                // On efface le message
                messageUI.text = "";
        }
    }
}