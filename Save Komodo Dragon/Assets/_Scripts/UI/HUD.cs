using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Timer")] public TextMeshProUGUI timerText;

    [Header("Leveling")]
    public Image expFillImage;
    public TextMeshProUGUI levelText;

    [Header("Level Up")]
    public CanvasGroup levelUpPanel;
    public List<SkillUpButton> skillUpButtons = new List<SkillUpButton>();
    
    [Header("Aware Text")]
    public CanvasGroup bossText;
    public CanvasGroup massText;

    private void Start() {
        GameManager.OnAfterStateChanged += BeforeActionHandler;
    }
    void BeforeActionHandler(GameState state){
        if(state == GameState.BossPhase){
            OpenDangerText(bossText);
        }
    }
    public void OpenLevelUp(){
        foreach (SkillUpButton item in skillUpButtons)
        {
            item.SetUpButton();
        }
        levelUpPanel.alpha = 0;
        levelUpPanel.gameObject.SetActive(true);
        LeanTween.alphaCanvas(levelUpPanel, 1, 0.2f).setOnStart(()=>{
            levelUpPanel.alpha = 0;
            Time.timeScale = 0;
        }).setIgnoreTimeScale(true);
    }
    public void CloseLevelUp(){
        LeanTween.alphaCanvas(levelUpPanel, 0, 0.2f).setOnStart(()=>{levelUpPanel.alpha = 0;})
            .setIgnoreTimeScale(true).setOnComplete(()=>{
                levelUpPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            });
    }
    void OpenDangerText(CanvasGroup canvasGroup){
        canvasGroup.gameObject.SetActive(true);
        LeanTween.alphaCanvas(canvasGroup, 1f, 0.5f);
        LeanTween.alphaCanvas(canvasGroup, 0, 0.5f).setOnComplete(()=>{
            canvasGroup.gameObject.SetActive(false);
        }).setDelay(2f);
    }
}
