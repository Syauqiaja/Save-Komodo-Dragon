using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Accessories/Item" ,fileName ="New Item")]
public class ScriptableItem : ScriptableObject
{
    public string menuTitle;
    public ItemFraction itemFraction;
    public Sprite menuImage;
    public HeroType heroSignature;
    public ItemType itemType;

    [SerializeField] private List<int> values;

    public int getValuesAtLevel(Rarity rarity, int level){
        // Max value is 10 * (level 1 value)
        // where max level = 30

        return values[((int)rarity)] + (values[((int)rarity)] / 10 * 3) * (level+1);
    }
    public int MaxLevel(){
        return 30;
    }
}

[System.Serializable]
public enum Rarity{
    Normal = 0,
    Rare = 1,
    SuperRare = 2,
    SuperSuperRare = 3,
}

[System.Serializable]
public enum ItemType{
    KainPoleng=0,
    RoteTribe=1,
    CapingBasunanda=2,
    TesselSkirt=3,
    KingBaba=4,
    KingBabaArmor=5,
    KotangAntakusuma=6,
    AsmatShield=7,
    Barong=8,
    TiilanggaHat=9,
}
public enum ItemFraction{
    Accessories,
    Armor,
}