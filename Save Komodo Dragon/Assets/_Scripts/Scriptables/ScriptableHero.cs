using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a scriptable hero 
/// </summary>
[CreateAssetMenu(menuName = "Unit/Hero" ,fileName ="Heroes")]
public class ScriptableHero : ScriptableUnit{
    public HeroType HeroType;
    public HeroUnitBase Prefab;

    [Header("Weapon Skill")]
    public ScriptableSkill WeaponSkill;
}

[Serializable]
public enum HeroType {
    Draco = 0,
    Snorlax = 1
}
[Serializable]
public struct WeaponStats{
    public int AttackPower;
    public float FiringRate;
    public float ProjectileSpeed;
    public float launchRadius;
    public float projectileSize;
    // public ProjectileBase projectile;
}
[Serializable]
public enum WeaponType{
    PedangDraco = 0,
    ZaktiClaw = 1,
    BanaspathiOrb=2,
}
