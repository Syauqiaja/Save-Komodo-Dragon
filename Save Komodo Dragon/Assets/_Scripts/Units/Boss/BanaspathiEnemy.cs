using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanaspathiEnemy : EnemyUnitBase
{
    [Header("Projectile Handler")]
    private List<ProjectileBase> projectileBases = new List<ProjectileBase>();
    public float RotationSpeed = 1;
    public float CircleRadius = 1;


    [Header("Weapon Properties")]
    public WeaponStats weaponStats;
    private Vector3 positionOffset;
    private float[] angles = {0f,2f,4f};
    private float _timeElapsed=0;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start() {
        base.Start();
        for (int i = 0; i < 3; i++)
        {
            AddProjectile();
        }
    }
    private void LateUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            positionOffset.Set(
                Mathf.Cos(angles[i]) * CircleRadius,
                Mathf.Sin(angles[i]) * CircleRadius, 0f);
            projectileBases[i].transform.position = _t.position + positionOffset;
            angles[i] += Time.deltaTime * RotationSpeed;
        }

        if(_timeElapsed < Time.time){
            _timeElapsed = Time.time + 1/weaponStats.FiringRate;
            projectileBases[0].Launch((heroTransform.position - projectileBases[0].transform.position)*weaponStats.ProjectileSpeed);
            projectileBases.RemoveAt(0);
            AddProjectile();
        }
    }
    void AddProjectile(){
        ProjectileBase projectileBase = ObjectPooler.Instance.GetProjectile(WeaponType.BanaspathiOrb);
        projectileBase.gameObject.SetActive(true);
        projectileBase.SetDamage(weaponStats.AttackPower);
        projectileBases.Add(projectileBase);
    }
}   
