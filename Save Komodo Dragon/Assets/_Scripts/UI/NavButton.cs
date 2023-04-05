using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavButton : MonoBehaviour
{
    const float LEAN_TIME = 0.1f;
    [SerializeField] bool startingState = false;
    [SerializeField] private RectTransform iconRect;
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private RectTransform bgImage;
    [SerializeField] private GameObject text;
    public bool IsSelected {get{
        return _isSelected;
    }set{
        if(value) Activate();
        else Deactivate();
        _isSelected = value;
    }}
    private bool _isSelected = true;

    private void Awake() {
        IsSelected = startingState;
    }

    void Activate(){
        if(IsSelected) return;
        LeanTween.value(gameObject, layoutElement.preferredWidth, 300, LEAN_TIME).setEaseInOutSine().setOnUpdate((float value)=>{
            layoutElement.preferredWidth = value;
        });
        LeanTween.color(bgImage, Color.white, LEAN_TIME).setEaseInOutSine();
        LeanTween.value(gameObject, iconRect.localScale.x, 1.5f, LEAN_TIME).setEaseInOutSine().setOnUpdate((float value)=>{
            iconRect.localScale = new Vector3(value,value,1f);
        }).setOnComplete(()=>{text.SetActive(true);});
    }
    void Deactivate(){
        if(!IsSelected) return;
        text.SetActive(false);
        LeanTween.value(gameObject, layoutElement.preferredWidth, 150, LEAN_TIME).setEaseInOutSine().setOnUpdate((float value)=>{
            layoutElement.preferredWidth = value;
        });
        LeanTween.color(bgImage, new Vector4(0.75f,0.75f,0.75f,1), LEAN_TIME).setEaseInOutSine();
        LeanTween.value(gameObject, iconRect.localScale.x, 1f, LEAN_TIME).setEaseInOutSine().setOnUpdate((float value)=>{
            iconRect.localScale = new Vector3(value,value,1f);
        });
    }
}
