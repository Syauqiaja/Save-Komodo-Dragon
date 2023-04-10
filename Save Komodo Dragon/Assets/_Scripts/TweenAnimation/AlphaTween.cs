using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaTween : UITween
{
    public CanvasGroup canvasGroup;
    public bool onEnable =true;

    private void OnEnable() {
        if(onEnable) Show();
    }
    public override void Show()
    {
        canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(canvasGroup, 1, leanTime).setDelay(delay).setIgnoreTimeScale(true);
    }
    public override void Hide()
    {
        LeanTween.alphaCanvas(canvasGroup, 0, leanTime).setOnComplete(()=>{
            gameObject.SetActive(false);
        }).setIgnoreTimeScale(true);
    }
}
