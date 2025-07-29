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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
                messageUI.text = "[ I.A LOG ] Appuyer sur E pour interagir.";
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