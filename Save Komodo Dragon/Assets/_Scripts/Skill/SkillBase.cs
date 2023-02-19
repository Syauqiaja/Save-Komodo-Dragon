using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    public SkillType skillType;
    public HeroUnitBase heroUnit;
    public WeaponStats weaponStats {get; private set;}
    public void SetStats(WeaponStats _stats) => weaponStats = _stats;
    private float _timeElapsed = 0;

    private void Update() {
        if(_timeElapsed < Time.time){
            _timeElapsed = Time.time + 1f/weaponStats.FiringRate;
            Attack(transform.position, heroUnit.facingDirection);
        }
    }

    protected virtual void Attack(Vector3 CenterPos, Vector3 direction){
    }
}
