using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanelTween : MonoBehaviour
{
    [SerializeField] private CanvasGroup vignette;
    [SerializeField] private Image enemyImage;
    [SerializeField] private GameObject blood1,blood2,blood3;

    private void OnEnable() {
        OpenLose();
    }
    void OpenLose(){
        blood1.transform.localScale = Vector3.zero;
        blood2.transform.localScale = Vector3.zero;
        blood3.transform.localScale = Vector3.zero;
        enemyImage.transform.localScale = Vector3.zero;
        blood1.LeanScale(Vector3.one, 0.7f).setEaseOutExpo().setOnComplete(()=>{
            LeanTween.scale(enemyImage.gameObject, Vector2.one, 0.5f).setEaseOutExpo();
        });
        blood2.LeanScale(Vector3.one, 0.7f).setEaseOutExpo().setDelay(0.1f);
        blood3.LeanScale(Vector3.one, 0.7f).setEaseOutExpo().setDelay(0.2f);
    }
}
