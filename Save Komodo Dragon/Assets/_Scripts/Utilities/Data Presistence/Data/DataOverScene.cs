using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="GameData", fileName = "Game Data")]
public class DataOverScene : ScriptableObject
{
    public event Action OnGoldChanged;
    public event Action OnCrystalChanged;

    public bool IsLoaded{get{return _isLoaded;} set{_isLoaded = value;}}
    private bool _isLoaded = false;

    public long Gold{get{return _gold;} set{_gold = value; OnGoldChanged?.Invoke();}}
    private long _gold;

    public int Crystal{get{return _crystal;} set{_crystal = value; OnCrystalChanged?.Invoke();}}
    private int _crystal;

    public int Energy{get{return _energy;} set{_energy = value;}}
    private int _energy;

    public string PlayerName{get{return _playerName;} set{_playerName = value;}}
    private string _playerName;

    public HeroType SelectedHero{get{return _selectedHero;} set{_selectedHero = value;}}
    private HeroType _selectedHero;

    public SkillType SelectedSkill{get{return _selectedSkill;} set{_selectedSkill = value;}}
    private SkillType _selectedSkill;

    public int SelectedArmorIndex{get{return _selectedArmorIndex;} set{_selectedArmorIndex = value;}}
    private int _selectedArmorIndex = -1;

    public int SelectedAccessoryIndex{get{return _selectedAccessoryIndex;} set{_selectedAccessoryIndex = value;}}
    private int _selectedAccessoryIndex = -1;

    public int SelectedMap{get{return _selectedMap;} set{_selectedMap = value;}}
    private int _selectedMap = 0;

    public List<ItemBundle> itemHeld = new List<ItemBundle>();
    public List<MapBundle> MapUnclokedBundles = new List<MapBundle>();
    public List<HeroBundle> HeroBundles = new List<HeroBundle>();

    public void AddMap(int bestTime, int bestEnemies){
        MapUnclokedBundles.Add(new MapBundle(bestTime, bestEnemies));
    }

    public void UnlockHero(HeroType type){
        HeroBundles[((int)type)].isUnlocked = true;
        HeroBundles[((int)type)].level = 1;
    }

    public void AddItem(ItemType itemType, int level, int rarity){
        bool isSelected=false;
        if(SelectedAccessoryIndex == itemHeld.Count || SelectedArmorIndex == itemHeld.Count) isSelected = true;

        ItemBundle itemPair = new ItemBundle(ResourceSystem.Instance.GetItem(itemType), level, isSelected, (Rarity) rarity);
        itemHeld.Add(itemPair);
    }
    public void AddItem(ItemBundle item){
        itemHeld.Add(item);
    }
    public void SetSelectedArmor(int armorIndex) {
        Debug.Log("Set selected Armor Index : " + SelectedArmorIndex);
        if(SelectedArmorIndex != -1) itemHeld[SelectedArmorIndex].isSelected = false;
        SelectedArmorIndex = armorIndex;
        itemHeld[SelectedArmorIndex].isSelected = true;
    }
    public ScriptableItem GetSelectedArmor() {
        if(SelectedArmorIndex == -1) return null;
        Debug.Log("Get selected Armor Index : " + SelectedArmorIndex);
        return itemHeld[SelectedArmorIndex].item;
    }
    public ItemBundle GetSelectedArmorBundle() {
        if(SelectedArmorIndex == -1) return null;
        Debug.Log("Get selected Armor Index : " + SelectedArmorIndex);
        return itemHeld[SelectedArmorIndex];
    }
    public void SetSelectedAccessory(int accIndex) {
        if(SelectedAccessoryIndex != -1) itemHeld[SelectedAccessoryIndex].isSelected = false;
        SelectedAccessoryIndex = accIndex;
        itemHeld[SelectedAccessoryIndex].isSelected = true;
    }
    public ScriptableItem GetSelectedAccessory() {
        if(SelectedAccessoryIndex == -1) return null;
        return itemHeld[SelectedAccessoryIndex].item;
    }
    public ItemBundle GetSelectedAccessoryBundle() {
        if(SelectedAccessoryIndex == -1) return null;
        return itemHeld[SelectedAccessoryIndex];
    }
    public void SetSelectedHero(HeroType t) {
        SelectedHero = t;
        SelectedSkill = ResourceSystem.Instance.GetHero(t).WeaponSkill.skillType;}
    public ScriptableHero GetSelectedHero() => ResourceSystem.Instance.GetHero(SelectedHero);
}

public class ItemBundle{
    public ScriptableItem item;
    public int level;
    public bool isSelected;
    public Rarity rarity;
    public ItemBundle(ScriptableItem item, int level, bool isSelected,Rarity rarity){
        this.item = item;
        this.level = level;
        this.isSelected = isSelected;
        this.rarity = rarity;
    }
    public int GetCurrentValue(){
        return item.getValuesAtLevel(rarity, level);
    }
}

public class MapBundle{
    public int bestSurviveTime = 0;
    public int bestEnemiesKilled = 0;

    public MapBundle(int bestSurvive, int bestKill){
        this.bestSurviveTime = bestSurvive;
        this.bestEnemiesKilled = bestKill;
    }
}

public class HeroBundle{
    public bool isUnlocked = false;
    public int level = 0;
    public HeroBundle(bool i, int l){
        isUnlocked = i;
        level = l;
    }
}