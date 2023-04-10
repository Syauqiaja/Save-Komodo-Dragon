using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    // Mandatory Data Save
    public string playerName;
    public long gold;
    public int crystal;
    public int energy;

    public int heroSelected;
    public int armorSelected;
    public int skillSelected;
    public int accessorySelected;

    public int mapsOpened;
    public int selectedMap;
    public int[] bestSurviveTime;
    public int[] bestEnemiesKilled;

    public int[] itemHeld;
    public int[] itemHeldLevel;
    public int[] itemHeldRarity;

    public bool[] heroIsUnlocked;
    public int[] heroLevels;

    public GameData(){
        gold = 0;
        crystal = 1000;
        energy = 40;
        heroSelected = 0;
        skillSelected = 4;
        accessorySelected = -1;
        armorSelected = -1;
        playerName = "Player";
        itemHeld = new int[0];
        itemHeldLevel = new int[0];
        itemHeldRarity = new int[0];

        mapsOpened = 1;
        selectedMap = 0;
        bestEnemiesKilled = new int[1]{0};
        bestSurviveTime = new int[1]{0};

        // Added Manually as the hero count
        heroIsUnlocked = new bool[ResourceSystem.Instance.Heroes.Count];
        heroLevels = new int[ResourceSystem.Instance.Heroes.Count];
        for (int i = 1; i < heroIsUnlocked.Length; i++)
        {
            heroIsUnlocked[i] = false;
            heroLevels[i] = 0;
        }
        heroIsUnlocked[0] = true;
        heroLevels[0] = 1;
    }
}
