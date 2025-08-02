using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    private string objectifDeBase = "Objectif : Collectez 2 Eau, 2 Graines, 1 Engrais";
    private string objectifTermine = "Objectif atteint – Parlez à l’IA (E)";

    public void AfficherObjectif()
    {
        if (objectiveText != null)
            objectiveText.text = objectifDeBase;
    }

    public void AfficherObjectifAtteint()
    {
        if (objectiveText != null)
            objectiveText.text = objectifTermine;
    }
}
