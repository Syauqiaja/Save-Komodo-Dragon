using UnityEngine;
using System;

public interface IDataPresistence{
    void SaveData(GameData data);
    void LoadData(GameData data);
}