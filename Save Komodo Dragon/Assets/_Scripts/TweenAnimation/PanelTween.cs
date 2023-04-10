using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelTween : UITween
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private List<UITween> childTween;
    public event Action afterShow;

    public override void Show()
    {
        foreach (UITween item in childTween)
        {
            item.Show(delay + leanTime);
        }
        LeanTween.alphaCanvas(canvasGroup, 1, leanTime).setIgnoreTimeScale(true);
        StartCoroutine(waitShoeEnd());
    }
    IEnumerator waitShoeEnd(){
        yield return new WaitForSecondsRealtime(childTween[childTween.Count-1].delay+childTween[childTween.Count-1].leanTime);
        afterShow?.Invoke();
    }
    public override void Hide()
    {
        foreach (UITween item in childTween)
        {
            item.Hide(delay+leanTime);
        }
        LeanTween.alphaCanvas(canvasGroup, 0, leanTime)
            .setDelay(childTween[childTween.Count-1].delay+childTween[childTween.Count-1].leanTime)
            .setOnComplete(()=>{
                gameObject.SetActive(false);
            }).setIgnoreTimeScale(true);
    }
    public void QuickHide(){
        LeanTween.alphaCanvas(canvasGroup, 0, leanTime).setOnComplete(()=>{
            gameObject.SetActive(false);
        }).setIgnoreTimeScale(true);
    }
    public void QuickHide(Action action){
        LeanTween.alphaCanvas(canvasGroup, 0, leanTime).setOnComplete(()=>{
            action.Invoke();
            gameObject.SetActive(false);
        }).setIgnoreTimeScale(true);
    }
}
