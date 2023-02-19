using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ZaktiClaw : SkillBase
{
    Vector3 projectileSize;
    private void Start() {
        GameManager.SkillUpgraded += Upgrade;
        projectileSize = Vector3.one * weaponStats.projectileSize;
    }
    public void Upgrade(SkillType skillType, int upgradeTo){
        if(skillType == this.skillType)
        SetStats(ResourceSystem.Instance.GetSkill(SkillType.ZaktiClaw).weaponStats[upgradeTo]);
        projectileSize = Vector3.one * weaponStats.projectileSize;
    }
    protected override void Attack(Vector3 CenterPos, Vector3 direction)
    {
        ProjectileBase claw = ObjectPooler.Instance.GetProjectile(WeaponType.ZaktiClaw);
        claw.transform.right = direction;
        claw.transform.localScale = projectileSize;

        // int enemyCount = heroUnit.enemyFront.Count;
        if(heroUnit.enemyFront.Count > 0) 
            claw.transform.position = heroUnit.enemyFront[Random.Range(0,heroUnit.enemyFront.Count)].position;
        else claw.transform.position = (Vector2) (CenterPos + direction);

        claw.SetDamage(weaponStats.AttackPower);
        claw.gameObject.SetActive(true);
        claw.Launch(direction);
    }
}
