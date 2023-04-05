using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Cloth", fileName ="New Cloth")]
public class ScriptableCloth : ScriptableEquipment
{
    public ClothType clothType;
    public List<float> upgradeValues;
}

[System.Serializable]
public enum ClothType{
    KingBaba,
    Barong,
    TiilanggaHat,
    KotangAntakusuma,
    AsmatShield,
}