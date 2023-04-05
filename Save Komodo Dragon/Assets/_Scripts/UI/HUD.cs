using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    private DataOverScene dataHolder;

    [Header("Leveling")]
    public Image expFillImage;
    public TextMeshProUGUI levelText;

    [Header("Level Up")]
    public CanvasGroup levelUpPanel;
    public List<SkillUpButton> skillUpButtons = new List<SkillUpButton>();
    public List<EquipmentContainer> equipmentContainers = new List<EquipmentContainer>();
    
    [Header("Aware Text")]
    public CanvasGroup bossText;
    public CanvasGroup massText;

    [Header("Pause Handler")] 
    public CanvasGroup pausePanel;
    public Transform SkillGroup;
    public Transform ClothGroup;
    private PauseCard[] skillCardGroups = new PauseCard[5];
    private PauseCard[] clothCardGroups = new PauseCard[5];

    [Header("Other")] 
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI enemyKilledText;
    public CanvasGroup HomeConfirmation;
    public TreasureSpinner treasureSpinner;
    private GameObject _openedBy;

    [Header("On Win")]
    public CanvasGroup WinPanel;
    public GiftHandler onWinGift;

    [Header("On Lose")]
    public CanvasGroup LosePanel;
    public TextMeshProUGUI currentTimerText;
    public TextMeshProUGUI bestTimeText;

    private short _pauseState = 0; // 0:Not paused, 1:between, 2:Paused

    private void Awake() {
        
            skillCardGroups = SkillGroup.GetComponentsInChildren<PauseCard>();
            clothCardGroups = ClothGroup.GetComponentsInChildren<PauseCard>();
        
    }
    private void Start() {
        GameManager.OnBeforeStateChanged += BeforeActionHandler;
        dataHolder = GameManager.Instance.dataHolder;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    private void OnDestroy() {
        GameManager.OnBeforeStateChanged -= BeforeActionHandler;
    }
    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    void BeforeActionHandler(GameState state){
        if(state == GameState.BossPhase){
            OpenDangerText(bossText);
        }else if(state == GameState.Win){
            OnWin();
        }else if(state == GameState.Lose){
            OnLose();
        }
    }
    public void OnLose(){
        int bestTime = dataHolder.MapUnclokedBundles[dataHolder.SelectedMap].bestSurviveTime;
        int timer = (int) GameManager.Instance.Timer;
        if(timer > bestTime) {
            bestTime = timer;
        }
        bestTimeText.text = "Best Time : "+bestTime/60+"m "+bestTime%60+"s";
        currentTimerText.text = timer/60+"m "+timer%60+"s";
        LosePanel.gameObject.SetActive(true);
    }
    public void OnWin(){
        onWinGift.SetGold(GameManager.Instance.goldEarned);
        onWinGift.SetItems(GameManager.Instance.itemObtained);
        WinPanel.gameObject.SetActive(true);
    }
    public void OpenLevelUp(){
        Time.timeScale = 0;
        levelUpPanel.alpha = 0;
        for (int i = 0; i < 3; i++)
        {
            skillUpButtons[i].SetUpButton(GameManager.Instance.GetPreparedEquipment(i));
        }
        levelUpPanel.alpha = 0;
        levelUpPanel.gameObject.SetActive(true);
        LeanTween.alphaCanvas(levelUpPanel, 1, 0.2f).setIgnoreTimeScale(true);
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
    public void UpdateInventory(SkillType type, int currentLevel){
        ScriptableSkill skill = ResourceSystem.Instance.GetSkill(type);
        foreach (PauseCard cardGroup in skillCardGroups)
        {
            if(cardGroup.title.text == skill.menuName){
                cardGroup.SetPauseCard(currentLevel);
                return;
            }else if(cardGroup.title.text == "-"){
                cardGroup.SetPauseCard(skill.menuImage, skill.menuName, currentLevel);
                return;
            }
        }
    }
    public void UpdateInventory(ClothType type, int currentLevel){
        ScriptableCloth cloth = ResourceSystem.Instance.GetCloth(type);
        foreach (PauseCard cardGroup in clothCardGroups)
        {
            if(cardGroup.title.text == cloth.menuName){
                cardGroup.SetPauseCard(currentLevel);
                return;
            }else if(cardGroup.title.text == "-"){
                cardGroup.SetPauseCard(cloth.menuImage, cloth.menuName, currentLevel);
                return;
            }
        }
    }
// =============== Accessed By Button ==============
    public void Pause(){
        Debug.Log("Pausing");
        switch(_pauseState){
            case 0:
                DoPause();    
                break;
            case 2:
                DoUnpause();
                break;
            default:
                break;
        }
    }
    private void DoPause(){
        _pauseState = 1;
        Time.timeScale = 0f;
        pausePanel.alpha = 0;
        pausePanel.gameObject.SetActive(true);
        LeanTween.alphaCanvas(pausePanel, 1, 0.2f).setIgnoreTimeScale(true).setIgnoreTimeScale(true).setOnComplete(()=>{
            _pauseState = 2;
        });
    }
    private void DoUnpause(){
        _pauseState = 1;
        pausePanel.alpha = 1;
        LeanTween.alphaCanvas(pausePanel, 0, 0.2f).setIgnoreTimeScale(true).setIgnoreTimeScale(true).setOnComplete(()=>{
            _pauseState = 0;
            Time.timeScale = 1f;
            pausePanel.gameObject.SetActive(false);
        });
    }
    public void OpenHomeConfirmation(GameObject openBy){
        _openedBy = openBy;
        _openedBy.SetActive(false);
        HomeConfirmation.gameObject.SetActive(true);
    }
    public void CloseHomeConfirmation(){
        _openedBy.SetActive(true);
        HomeConfirmation.gameObject.SetActive(false);
    }
    public void BackToHome(){
        Time.timeScale = 1f;
        GameManager.Instance.ChangeState(GameState.BattleOver);
    }
}

[System.Serializable]
public struct EquipmentContainer{
    public Image image;
    public TextMeshProUGUI level;
    public EquipmentType equipmentType;
}
