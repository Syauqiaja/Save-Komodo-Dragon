using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType skillType;
    [HideInInspector] public HeroUnitBase heroUnit;
    public WeaponStats weaponStats {get; private set;}
    public virtual void SetStats(WeaponStats _stats) => weaponStats = _stats;
    private float _timeElapsed = 0;

    virtual public void Awake() {
        GameManager.SkillUpgraded += Upgrade;
    }
    virtual public void OnDestroy() {
        GameManager.SkillUpgraded -= Upgrade;
    }

    protected virtual void Update() {
        if(_timeElapsed < Time.time && weaponStats.FiringRate > 0){
            _timeElapsed = Time.time + 1f/weaponStats.FiringRate;
            Attack(transform.position, heroUnit.facingDirection);
        }
    }

    public virtual void Attack(Vector3 CenterPos, Vector3 direction){}

    public virtual void Upgrade(SkillType skillType, int upgradeTo){
        if(skillType == this.skillType)
        SetStats(ResourceSystem.Instance.GetSkill(this.skillType).weaponStats[upgradeTo]);
    }
}
