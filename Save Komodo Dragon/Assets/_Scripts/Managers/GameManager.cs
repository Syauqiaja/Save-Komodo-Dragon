using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;
    public static event Action<SkillType,int> SkillUpgraded;

    public GameState State { get; private set; }
    public HUD CanvasHUD;
    public DataOverScene dataHolder;

    [HideInInspector] public int Deaths;
    [HideInInspector] public int Exp = 1;
    [HideInInspector] public float ExpLevel = 0;
    [HideInInspector] public int goldEarned=0;
    [HideInInspector] public int crystalEarned = 0;
    [HideInInspector] public Dictionary<EnemyType, int> totalEnemies;
    [HideInInspector] public Dictionary<SkillType,int> SkillHeld = new Dictionary<SkillType,int>();
    [HideInInspector] public Dictionary<ClothType,int> ClothHeld = new Dictionary<ClothType,int>();
    [HideInInspector] public List<ScriptableItem> itemObtained = new List<ScriptableItem>();
    [HideInInspector] public List<ScriptableEquipment> preparedEquipment = new List<ScriptableEquipment>();
    public float Timer {get; private set;}
    private bool isWin = false;
    private float maxExp;
    private int enemyKilled=0;

    // Kick the game off with the first state
    protected override void Awake()
    {
        base.Awake();
    }
    void Start(){
        Timer = 0f;
        ChangeState(GameState.Starting);
        PrepareEquipment();
    }
    void Update(){
        Timer += Time.deltaTime;
        CanvasHUD.timerText.text = (Timer/60).ToString("00") + ":" +(Timer%60).ToString("00");
    }

//============= Leveling Part BEGIN ===================

    public void AddExp(int value){
        Exp += Mathf.FloorToInt(value * ClothHandler.Instance.expMultiplier);
        CanvasHUD.expFillImage.fillAmount = Exp/maxExp;
        if(Exp>= maxExp){
            Exp = 0;
            maxExp = Mathf.Round(maxExp * 1.5f);
            HandleLevelUp();
        }
    }
    private void UpdateLevel(){
        CanvasHUD.expFillImage.fillAmount = 0;
        CanvasHUD.CloseLevelUp();
    }
    public void HandleLevelUp(){
        ExpLevel++;
        CanvasHUD.levelText.text = ExpLevel.ToString();
        CanvasHUD.OpenLevelUp();
    }

//============= Leveling Part END ===================
//============= Equipment Part BEGIN ===================

    public void AddEquipment(SkillType type){
        if(!SkillHeld.ContainsKey(type)) {
            UnitManager.Instance.AddNewSkill(type);
            SkillHeld.Add(type, 0);
            CanvasHUD.UpdateInventory(type, 1);
        }
        else UpgradeEquipment(type);
        PrepareEquipment();
        UpdateLevel();
    }
    public void AddEquipment(ClothType type){
        if(!ClothHeld.ContainsKey(type)) {
            ClothHeld.Add(type, 0);
            CanvasHUD.UpdateInventory(type, 1);
        }
        else UpgradeEquipment(type);
        PrepareEquipment();
        UpdateLevel();
    }
    public int GetEquipmentLevel(ScriptableEquipment equipment){
        int level;
        if(equipment.equipmentType == EquipmentType.Skill){
            if(!SkillHeld.TryGetValue(((ScriptableSkill) equipment).skillType,out level))
                level = -1;
        }else{
            if(!ClothHeld.TryGetValue(((ScriptableCloth) equipment).clothType,out level))
                level = -1;
        }
        return level;
    }
    public void UpgradeEquipment(SkillType type){
        SkillHeld[type] = Mathf.Min(4, SkillHeld[type]+1);
        SkillUpgraded?.Invoke(type, SkillHeld[type]);
        CanvasHUD.UpdateInventory(type, SkillHeld[type]+1);
    }
    public void UpgradeEquipment(ClothType type){
        ClothHeld[type] = Mathf.Min(5, ClothHeld[type]+1);
        CanvasHUD.UpdateInventory(type, ClothHeld[type]+1);
    }
    public List<ScriptableEquipment> GetEquipmentHeldList(){
        List<ScriptableEquipment> equipmentList = new List<ScriptableEquipment>();
        foreach (SkillType item in SkillHeld.Keys)
        {
            equipmentList.Add(ResourceSystem.Instance.GetSkill(item));
        }
        foreach (ClothType item in ClothHeld.Keys)
        {
            equipmentList.Add(ResourceSystem.Instance.GetCloth(item));
        }

        return equipmentList;
    }
    private async void PrepareEquipment(){
        // List<ScriptableEquipment> _prepared = new List<ScriptableEquipment>();
        preparedEquipment = await ResourceSystem.Instance.GetUpgradableEquipment();
        // preparedEquipment[0] = _prepared[0];
        // preparedEquipment[1] = _prepared[1];
        // preparedEquipment[2] = _prepared[2];
    }
    public ScriptableEquipment GetPreparedEquipment(int index){
        return preparedEquipment[index];
    }

//============= Equipment Part END ===================
//============= State Handler BEGIN ===================
    
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
                HandleOnBattleEnd();
                break;
            case GameState.BattleOver:
                SceneLoader.Instance.LoadScene(isWin? LoadingContentType.WinMainMenu : LoadingContentType.LoseMainMenu);
                break;
            case GameState.WaveChange:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }
    public void HandleOnBattleEnd(){
        foreach (ScriptableItem item in itemObtained)
        {
            // dataHolder.AddItem(item.itemType,1,);
        }
        dataHolder.Gold += goldEarned;
        dataHolder.Crystal += crystalEarned;

        if(Timer > dataHolder.MapUnclokedBundles[dataHolder.SelectedMap].bestSurviveTime) {
            dataHolder.MapUnclokedBundles[dataHolder.SelectedMap].bestSurviveTime = (int) Timer;
        }
    }
    public void HandleStarting(){
        UnitManager.Instance.SpawnHero();
        UnitManager.Instance.SpreadDiamond();
        ExpLevel = 1;
        Exp = 0;
        maxExp = 10;
    }
    //============= State Handler END ===================
    //============= Pickable Handler BEGIN ===================
    public void OpenChest(){
        Time.timeScale = 0f;
        CanvasHUD.OpenTreasure();
    }
    public void GoldPicked(int value){
        goldEarned += value;
        CanvasHUD.goldText.text = goldEarned.ToString();
    }
    public void EnemyKilled(){
        enemyKilled++;
        CanvasHUD.enemyKilledText.text = enemyKilled.ToString();
        if(enemyKilled % 10 == 0){
            if(itemObtained.Count < 6)
            itemObtained.Add(ResourceSystem.Instance.GetRandomItem());
        }
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
    BattleOver = 8,
    WaveChange = 9,
}