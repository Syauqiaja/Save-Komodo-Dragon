using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform bound;
    [SerializeField] private RectTransform analog;
    [SerializeField] private CanvasGroup canvasGroup;
    private static Vector2 posInput;
    public void OnDrag(PointerEventData eventData){
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bound, eventData.position, eventData.pressEventCamera, out posInput)){
            posInput /= bound.sizeDelta/2f;
            if(posInput.magnitude >= 1f) posInput.Normalize();
            // posInput.Normalize();
            analog.anchoredPosition = new Vector2(
                posInput.x * bound.sizeDelta.x/2f,
                posInput.y * bound.sizeDelta.y/2f
                );
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        OnDrag(eventData);
        canvasGroup.alpha = 1f;
    }
    public void OnPointerUp(PointerEventData eventData){
        posInput = Vector2.zero;
        analog.anchoredPosition = Vector2.zero;
        canvasGroup.alpha = 0.7f;
    }
    public static Vector2 GetJoystickAxis(){
        return posInput;
    }
}
