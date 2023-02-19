using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Skill", fileName ="New Skill")]
public class ScriptableSkill : ScriptableObject
{
    public string skillName;
    public SkillType skillType;
    public SkillBase prefab;
    public List<WeaponStats> weaponStats;

    [Header("Menu Item")]
    public Sprite menuImage;
    public string skillDesc;
}

[System.Serializable]
public enum SkillType{
    HeroBaseSkill,
    AdhistanaBow=0,
    GatotKacaArmor=1,
    KujangStab=2,
    ZaktiClaw=3,
}