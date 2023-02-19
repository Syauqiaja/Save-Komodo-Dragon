using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public static event Action<SkillType,int> SkillUpgraded;

    public GameState State { get; private set; }
    public HUD CanvasHUD;

    [HideInInspector] public int Deaths;
    [HideInInspector] public int Exp = 1;
    [HideInInspector] public float ExpLevel = 0;
    public float Timer {get; private set;}
    private float maxExp;
    private Dictionary<SkillType,int> SkillHeld = new Dictionary<SkillType,int>();

    // Kick the game off with the first state
    void Start(){
        ChangeState(GameState.Starting);
        Timer = 0f;
    }
    void Update(){
        Timer += Time.deltaTime;
        CanvasHUD.timerText.text = (Timer/60).ToString("00") + ":" +(Timer%60).ToString("00");
    }
    public void AddExp(int value){
        Exp += value;
        CanvasHUD.expFillImage.fillAmount = Exp/maxExp;
        if(Exp >= maxExp) HandleLevelUp();
    }
    public void AddSkill(SkillType type){
        if(!SkillHeld.ContainsKey(type)) {
            UnitManager.Instance.AddNewSkill(type);
            SkillHeld.TryAdd(type, 0);
        }
        else UpgradeSkill(type);
        UpdateLevel();
    }
    public int GetSkillLevel(SkillType type){
        return SkillHeld[type];
    }
    public void UpgradeSkill(SkillType skill){
        SkillHeld[skill] = Mathf.Min(5, SkillHeld[skill]+1);
        SkillUpgraded?.Invoke(skill, SkillHeld[skill]);
    }
    private void UpdateLevel(){
        CanvasHUD.CloseLevelUp();
        ExpLevel++;
        CanvasHUD.levelText.text = ExpLevel.ToString();
        Exp = 0;
        maxExp = Mathf.Round(maxExp * 1.3f);
        CanvasHUD.expFillImage.fillAmount = 0;
    }
    //
    // State Handler
    //
    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.SmallPhase:
                break;
            case GameState.MassPhase:
                break;
            case GameState.ElitePhase:
                break;
            case GameState.BossPhase:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }
    public void HandleStarting(){
        UnitManager.Instance.SpawnHero(HeroType.Draco);
        UnitManager.Instance.SpreadDiamond();
        ExpLevel = 1;
        Exp = 0;
        maxExp = 10;
        SkillHeld.Add(SkillType.HeroBaseSkill, 0);
        ChangeState(GameState.SmallPhase);
    }
    public void HandleLevelUp(){
        CanvasHUD.OpenLevelUp();
    }
}

[Serializable]
public enum GameState {
    Starting = 0,
    SmallPhase = 1,
    MassPhase = 2,
    ElitePhase = 3,
    BossPhase = 4,
    LevelUp = 5,
    Win = 6,
    Lose = 7,
}