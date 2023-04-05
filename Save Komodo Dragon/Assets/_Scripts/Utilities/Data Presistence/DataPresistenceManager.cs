using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

public class DataPresistenceManager : StaticInstance<DataPresistenceManager>
{
    [SerializeField] private DataOverScene dataHolder;
    [SerializeField] private string fileName = "";

    public static event Action OnAfterLoad;
    private GameData gameData = null;
    private List<IDataPresistence> dataPresistences;
    private FileDataHandler dataHandler;
    public bool IsLoaded = false;

    protected override void Awake() {
        base.Awake();
        this.dataPresistences = FindAllDataPresistences();
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    private void Start() {
        LoadData();
    }
    public void SaveData(){
        gameData.heroSelected = ((int)dataHolder.SelectedHero);
        gameData.skillSelected = (int)dataHolder.SelectedSkill;
        gameData.gold = this.dataHolder.Gold;
        gameData.crystal = this.dataHolder.Crystal;
        gameData.playerName = this.dataHolder.PlayerName;
        gameData.energy = this.dataHolder.Energy;
        gameData.armorSelected = this.dataHolder.SelectedArmorIndex;
        gameData.accessorySelected = this.dataHolder.SelectedAccessoryIndex;
        gameData.selectedMap = this.dataHolder.SelectedMap;
        gameData.mapsOpened = this.dataHolder.MapUnclokedBundles.Count;

        // Saving hero levels
        int[] heroLevels = new int[dataHolder.HeroBundles.Count];
        bool[] heroIsUnlocked = new bool[dataHolder.HeroBundles.Count];
        for (int i = 0; i < dataHolder.HeroBundles.Count; i++)
        {
            heroLevels[i] = dataHolder.HeroBundles[i].level;
            heroIsUnlocked[i] = dataHolder.HeroBundles[i].isUnlocked;
        }
        gameData.heroIsUnlocked = heroIsUnlocked;
        gameData.heroLevels = heroLevels;

        // Saving maps data
        int[] bestTime = new int[dataHolder.MapUnclokedBundles.Count];
        int[] bestKilled = new int[dataHolder.MapUnclokedBundles.Count];
        for (int i = 0; i < dataHolder.MapUnclokedBundles.Count; i++)
        {
            bestTime[i] = dataHolder.MapUnclokedBundles[i].bestSurviveTime;
            bestKilled[i] = dataHolder.MapUnclokedBundles[i].bestEnemiesKilled;
        }
        gameData.bestSurviveTime = bestTime;
        gameData.bestEnemiesKilled = bestKilled;

        // Saving items data
        int[] _itemHeld = new int[dataHolder.itemHeld.Count];
        int[] _itemLevel = new int[dataHolder.itemHeld.Count];
        int[] _itemRarity = new int[dataHolder.itemHeld.Count];
        for (int i = 0; i < dataHolder.itemHeld.Count; i++)
        {
            _itemHeld[i] = ((int)dataHolder.itemHeld[i].item.itemType);
            _itemLevel[i] = dataHolder.itemHeld[i].level;
            _itemRarity[i] = ((int)dataHolder.itemHeld[i].rarity);
        }
        gameData.itemHeld = _itemHeld;
        gameData.itemHeldLevel = _itemLevel;
        gameData.itemHeldRarity = _itemRarity;

        // Save to file
        dataHolder.IsLoaded = false;
        this.IsLoaded = false;
        dataHandler.Save(gameData);
    }
    public void LoadData(){
        gameData = dataHandler.Load();
        if(gameData == null){
            gameData = new GameData();
        }

        Debug.Log(this.name +" is loading");
        dataHolder.SelectedHero = (HeroType) gameData.heroSelected;
        dataHolder.SelectedSkill = (SkillType) gameData.skillSelected;
        dataHolder.Gold = gameData.gold;
        dataHolder.Crystal = gameData.crystal;
        dataHolder.PlayerName = gameData.playerName;
        dataHolder.Energy = gameData.energy;
        dataHolder.SelectedAccessoryIndex = gameData.accessorySelected;
        dataHolder.SelectedArmorIndex = gameData.armorSelected;
        dataHolder.SelectedMap = gameData.selectedMap;

        dataHolder.HeroBundles = new List<HeroBundle>();
        for (int i = 0; i < gameData.heroIsUnlocked.Length; i++)
        {
            dataHolder.HeroBundles.Add(new HeroBundle(gameData.heroIsUnlocked[i], gameData.heroLevels[i]));
        }
        
        dataHolder.MapUnclokedBundles = new List<MapBundle>();
        for (int i = 0; i < gameData.mapsOpened; i++)
        {
            dataHolder.AddMap(gameData.bestSurviveTime[i], gameData.bestEnemiesKilled[i]);
        }

        // Load Item held
        dataHolder.itemHeld = new List<ItemBundle>();
        if(gameData.itemHeld.Length != gameData.itemHeldLevel.Length)
            Debug.LogError("Item data not sync");
        int itemcount = gameData.itemHeld.Length;
        for(int i = 0; i<itemcount; i++){
            dataHolder.AddItem((ItemType) gameData.itemHeld[i], gameData.itemHeldLevel[i], gameData.itemHeldRarity[i]);
        }
        dataHolder.IsLoaded = true;
        OnAfterLoad?.Invoke();
        this.IsLoaded = true;
    }

    public List<IDataPresistence> FindAllDataPresistences(){
        IEnumerable<IDataPresistence> dataPresistences = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPresistence>();
        return new List<IDataPresistence>(dataPresistences);
    }

    protected override void OnApplicationQuit() {
        SaveData();
        base.OnApplicationQuit();
    }
    // private void OnApplicationFocus(bool focusStatus) {
    //     if(!focusStatus) SaveData();
    // }
    // private void OnApplicationPause(bool pauseStatus) {
    //     if(pauseStatus) SaveData();
    // }
}