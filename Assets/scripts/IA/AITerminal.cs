using UnityEngine;
using TMPro;

public class AITerminal : MonoBehaviour
{
    public Inventory playerInventory;
    [Header("Zones à revitaliser")]
    public GameObject[] zonesARevitaliser; // Liste des zones à activer
    [Header("UI")]
    public TextMeshProUGUI messageUI;

    [Header("Ressources requises")]
    public int besoinEau = 2;
    public int besoinGraines = 2;
    public int besoinFertilisant = 1;

    private bool joueurDansZone = false;

    private PlayerControls controls;

    [Header("UI Objectif")]
    public GameObject messageObjectifUI; // Glisse ici le même objet que dans Inventory

    public ObjectiveUI objectiveUI;
    // Ajouter un champ
    private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> interactionAction;
    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Enable();
        controls.Player.Interact.performed += ctx =>
        {
            if (joueurDansZone)
                ActiverIA();
        };
        controls.Player.Interact.performed += interactionAction;
    }

    void OnDisable()
    {
        controls.Player.Interact.performed -= ctx =>
        {
            if (joueurDansZone)
                ActiverIA();
        };
        controls.Player.Interact.performed -= interactionAction;
        controls.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (objectiveUI != null)
            objectiveUI.AfficherObjectif();
    }

    // Update is called once per frame
    void Update()
    {
        if (joueurDansZone && Input.GetKeyDown(KeyCode.E))
        {
            ActiverIA();
        }
    }

    void ActiverIA()
    {
        if (playerInventory == null) return;

        int eau = playerInventory.GetWaterDropCount();
        int graines = playerInventory.GetSeedCount();
        int fertil = playerInventory.GetFertilizerCount();

        Debug.Log($"[DEBUG] Inventaire : Eau={eau}, Graines={graines}, Fertilisant={fertil}");

        if (eau >= besoinEau && graines >= besoinGraines && fertil >= besoinFertilisant)
        {
            foreach (GameObject zone in zonesARevitaliser)
            {
                if (zone != null)
                    zone.SetActive(true);
            }

            if (messageUI != null)
                messageUI.text = "[ I.A LOG ] Ressources suffisantes.\nRevitalisation en cours ... trouvez la porte de sortie";

            if (objectiveUI != null)
                objectiveUI.AfficherObjectifAtteint();
                
            if (messageObjectifUI != null)
                messageObjectifUI.SetActive(false); // Cache le message objectif
        }
        else
        {
            messageUI.text = "[ I.A LOG ] Ressources insuffisantes.\nAnalyse en attente...";
        }
    }


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

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = false;
            if (messageUI != null)
                messageUI.text = "";
        }
    }
}