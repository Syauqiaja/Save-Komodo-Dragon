using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreasureSkip : MonoBehaviour, IPointerClickHandler
{
    public TreasureSpinner treasureSpinner;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!treasureSpinner.isEnd) treasureSpinner.isEnd = true;
    }
}
