using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEquipment : ScriptableObject
{
    [Header("Equipment Part")]
    public string menuName;
    public Sprite menuImage;
    public List<string> menuDesc;
    public EquipmentType equipmentType;
}

[System.Serializable]
public enum EquipmentType{
    Skill = 0,
    Cloth = 1,
}
