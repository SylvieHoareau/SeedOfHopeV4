using UnityEngine;
using UnityEngine.EventSystems;

public class MenuStarter : MonoBehaviour
{
    public GameObject firstSelected;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}
