using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mandau : SkillBase
{
    private ProjectileBase projectileBase;
    private Transform projectileTransform;
    private void Start() {
        projectileBase = ObjectPooler.Instance.GetProjectile(SkillType.DracoSword);
        projectileBase.SetDamage(weaponStats.AttackPower);
        projectileTransform = projectileBase.transform;
        projectileBase.gameObject.SetActive(true);
    }
    override public void Upgrade(SkillType type, int levelUpgrade)
    {
        base.Upgrade(type, levelUpgrade);
        if(type != this.skillType) return;
        projectileTransform.localScale = Vector3.one * weaponStats.size;
        projectileBase.SetDamage(weaponStats.AttackPower);
    }
    protected override void Update()
    {
        base.Update();
        projectileTransform.position = transform.position;
    }
    public override void Attack(Vector3 CenterPoint,Vector3 direction)
    {
        base.Attack(CenterPoint, direction);
        projectileBase.Launch(direction);
    }
}
