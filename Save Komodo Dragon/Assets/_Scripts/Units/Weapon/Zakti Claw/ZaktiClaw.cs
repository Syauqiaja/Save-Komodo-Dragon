using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZaktiClaw : SkillBase
{
    Vector3 projectileSize;
    private void Start() {
        projectileSize = Vector3.one * weaponStats.size;
    }
    public override void Upgrade(SkillType skillType, int upgradeTo){
        base.Upgrade(skillType, upgradeTo);
        if(skillType != this.skillType) return;
        projectileSize = Vector3.one * weaponStats.size;
    }
    public override void Attack(Vector3 CenterPos, Vector3 direction)
    {
        ProjectileBase claw = ObjectPooler.Instance.GetProjectile(SkillType.ZaktiClaw);
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
