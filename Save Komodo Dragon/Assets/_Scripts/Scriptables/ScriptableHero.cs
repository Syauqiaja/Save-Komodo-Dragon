using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a scriptable hero 
/// </summary>
[CreateAssetMenu(menuName = "Unit/Hero" ,fileName ="Heroes")]
public class ScriptableHero : ScriptableUnit{
    public HeroType heroType;
    public HeroUnitBase Prefab;
    public int heroPrice;
    public List<Sprite> heroSprites;

    [Header("Weapon Skill")]
    public ScriptableSkill WeaponSkill;
}

[Serializable]
public enum HeroType {
    Draco = 0,
    Pratidhi = 1,
    Adhistana = 2,
    Zakti = 3,
    Jarada = 4,
}
[Serializable]
public struct WeaponStats{
    public int AttackPower;
    public float FiringRate;
    public float speed;
    public float launchRadius;
    public float duration;
    public float size;
}
[Serializable]
public enum WeaponType{
    PedangDraco = 0,
    ZaktiClaw = 1,
    BanaspathiOrb=2,
}
