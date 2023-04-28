using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowInstructions : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject instructionsPanel;

    // Mouse enters 
    public void OnPointerEnter(PointerEventData eventData)
    {
        instructionsPanel.SetActive(true);
    }

    // Mouse exits
    public void OnPointerExit(PointerEventData eventData) 
    {
        instructionsPanel.SetActive(false);

    }

}
