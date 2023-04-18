using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanelTween : MonoBehaviour
{
    [SerializeField] private GameObject glow, hero;
    [SerializeField] private GameObject[] objects;

    private void OnEnable() {
        OpenWin();
    }

    private void OpenWin()
    {
        glow.transform.localScale = Vector3.zero;
        hero.transform.localScale = Vector3.zero;
        glow.LeanScale(Vector3.one, 0.5f).setEaseOutSine();
        hero.LeanScale(Vector3.one, 0.7f).setEaseOutBack();
        float delay = 0.1f;
        foreach (GameObject item in objects)
        {
            float startPosY = item.transform.position.y;
            item.transform.Translate(0,Random.Range(100,150),0);
            item.SetActive(false);
            LeanTween.moveY(item,startPosY, 1f).setEaseOutBounce().setDelay(delay).setOnStart(()=>{
                item.SetActive(true);
            });
            delay += 0.2f;
        }
    }
}
