using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Skill", fileName ="New Skill")]
public class ScriptableSkill : ScriptableEquipment
{
    [Header("Skill Part")]
    public SkillType skillType;

    public SkillBase prefab;
    public List<WeaponStats> weaponStats;
}

[System.Serializable]
public enum SkillType{
    WuhuAmet=0,
    GatotKacaArmor=1,
    KujangStab=2,
    ZaktiClaw=3,
    DracoSword=4,
    KerisBali=5,
    BanaspathiOrb = 6,
    LemparSandal = 7,
    PetasanBanting=8,
    Kalawali=9,
    KapakBatu=10,
    Ketapel=11,
    Pletokan=12,
    PoisonousSpear=13,
}