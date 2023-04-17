using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemparSandal : SkillBase
{
    public ProjectileBase projectileBase;

    public override void Attack(Vector3 CenterPos, Vector3 direction)
    {
        ProjectileBase projectile = ObjectPooler.Instance.GetOrSetProjectile(projectileBase);
        projectile.transform.position = CenterPos;
        projectile.gameObject.SetActive(true);
        projectile.lifeTime = weaponStats.duration;
        projectile.SetDamage(weaponStats.AttackPower);
        projectile.transform.localScale = Vector3.one * weaponStats.size;
        projectile.Launch(direction*weaponStats.speed);
    }
    public override void Upgrade(SkillType skillType, int upgradeTo)
    {
        base.Upgrade(skillType, upgradeTo);
    }
}
