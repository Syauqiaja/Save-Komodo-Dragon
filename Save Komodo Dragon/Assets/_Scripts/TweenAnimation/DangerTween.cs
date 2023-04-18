using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DangerTween : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textShadow;
    [SerializeField] private CanvasGroup textShadowCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private CanvasGroup vignette;
    private void OnEnable() {
        OpenDanger("Banaspathi");
    }
    public void OpenDanger(string enemyname){
        textShadowCanvas.alpha = 1;
        textShadow.transform.localScale = Vector3.one;
        vignette.alpha = 0;
        text.text = enemyname+"\nIncoming";
        textShadow.text = enemyname+"\nIncoming";
        UnitManager.Instance.Shake(2f, 3f, 1f);
        vignette.LeanAlpha(1, 1).setEaseOutSine().setOnComplete(()=>{
            LeanTween.scale(textShadow.gameObject, Vector3.one * 2, 1f).setLoopCount(3).setEaseOutSine();
            LeanTween.alphaCanvas(textShadowCanvas, 0f, 1f).setLoopCount(3).setOnComplete(()=>{
                vignette.LeanAlpha(0, 1).setEaseOutSine().setOnComplete(()=>{gameObject.SetActive(false);});
            });
        });
    }
}
