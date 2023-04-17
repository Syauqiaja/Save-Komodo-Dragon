using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wipe : UITween
{
    public LayoutGroup layoutToDisable;
    public WipeDirection direction;
    public bool onEnable = true;

    private RectTransform rectTransform;
    private Vector3 leftScreenBound, rightScreenBound, topScreenBound, bottomScreenBound;
    private Vector3 startPos;
    public enum WipeDirection{
        Top,
        Bottom,
        Left,
        Right,
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.position;
        RectTransform parentTrans = transform.parent.GetComponent<RectTransform>();
        leftScreenBound = new Vector3(startPos.x-parentTrans.rect.width, startPos.y, 0);
        rightScreenBound = new Vector3(startPos.x+parentTrans.rect.width, startPos.y, 0);
        topScreenBound = new Vector3(startPos.x, startPos.y+parentTrans.rect.height, 0);
        bottomScreenBound = new Vector3(startPos.x, startPos.y-parentTrans.rect.height, 0);
    }

    private void OnEnable() {
        if(onEnable) Show();
    }

    override public void Show(){
        LeanTween.cancel(gameObject);
        rectTransform.position = GetHidingPosition();
        Vector3 hidingPos = GetHidingPosition();
        LeanTween.moveY(gameObject, startPos.y, leanTime).setDelay(delay).setEase(easeType).setIgnoreTimeScale(true);
    }
    override public void Show(Action action){
        LeanTween.cancel(gameObject);
        rectTransform.position = GetHidingPosition();
        Vector3 hidingPos = GetHidingPosition();
        LeanTween.moveY(gameObject, startPos.y, leanTime).setDelay(delay).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(action);
    }
    override public void Hide(){
        LeanTween.cancel(gameObject);
        rectTransform.position = startPos;
        Vector3 hidingPos = GetHidingPosition();
        LeanTween.value(gameObject, 1,0, leanTime).setDelay(delay).setEase(easeType).setOnUpdate((float value)=>{
            rectTransform.position = Vector3.LerpUnclamped(hidingPos, startPos, value);
        }).setIgnoreTimeScale(true);
    }
    public override void Hide(Action action)
    {
        LeanTween.cancel(gameObject);
        rectTransform.position = startPos;
        Vector3 hidingPos = GetHidingPosition();
        LeanTween.value(gameObject, 1,0, leanTime).setDelay(delay).setEase(easeType).setOnUpdate((float value)=>{
            rectTransform.position = Vector3.LerpUnclamped(hidingPos, startPos, value);
        }).setIgnoreTimeScale(true).setOnComplete(action.Invoke);
    }
    Vector3 GetHidingPosition(){
        switch(direction){
            case WipeDirection.Top:
                return new Vector3(startPos.x, startPos.y+Screen.height, 0);
            case WipeDirection.Bottom:
                return new Vector3(startPos.x, startPos.y-Screen.height, 0);
            case WipeDirection.Left:
                return new Vector3(startPos.x-Screen.width, startPos.y, 0);
            case WipeDirection.Right:
                return new Vector3(startPos.x+Screen.width, startPos.y, 0);
            default:
                break;
        }
        return Vector3.zero;
    }
}
