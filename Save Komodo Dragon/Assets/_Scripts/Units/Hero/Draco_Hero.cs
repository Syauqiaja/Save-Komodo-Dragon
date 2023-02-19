using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draco_Hero : HeroUnitBase
{
    private ProjectileBase projectileBase;
    private Transform projectileTransform;
    protected override void Awake() {
        base.Awake();
    }
    private void Start() {
        projectileBase = ObjectPooler.Instance.GetProjectile(WeaponType.PedangDraco);
        projectileBase.SetDamage(Weapon.AttackPower);
        projectileTransform = projectileBase.transform;
        projectileBase.gameObject.SetActive(true);
        GameManager.SkillUpgraded += Upgrade;
    }
    public void Upgrade(SkillType type, int levelUpgrade)
    {
        if(type == SkillType.HeroBaseSkill){
            SetWeapon(ResourceSystem.Instance.GetSkill(type).weaponStats[levelUpgrade]);
            projectileTransform.localScale = Vector3.one * Weapon.projectileSize;
            projectileBase.SetDamage(Weapon.AttackPower);
        }
    }
    protected override void Attack(Vector3 CenterPoint,Vector3 direction)
    {
        projectileBase.Launch(direction);
    }
    protected override void MoveUnit()
    {
        base.MoveUnit();
        projectileTransform.position = transform.position;
    }
}
