using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanaspathiEnemy : EnemyUnitBase, IHeroDamager
{
    [Header("Projectile Handler")]
    private List<ProjectileBase> projectileBases = new List<ProjectileBase>();
    public float RotationSpeed = 1;
    public float CircleRadius = 1;
    public int _hitDamage;


    [Header("Weapon Properties")]
    public WeaponStats weaponStats;
    private Vector3 positionOffset;
    
    private float _timeElapsed=0;
    private float _startingSpeed=0;

    protected override void Start()
    {
        base.Start();
        _timeElapsed = Time.time + 1/weaponStats.FiringRate;
        _startingSpeed = BaseStats.travelSpeed;
        hitDamage = _hitDamage;
        SetStats(BaseStats);
    }

    public override void Move()
    {
        Vector3 moveDirection = (heroTransform.position - _t.position);
        if(moveDirection.magnitude > 0.1f){
            _t.Translate(moveDirection.normalized * Time.deltaTime * _startingSpeed * 0.1f);
        }
        _t.localScale = new Vector3(Mathf.Sign(moveDirection.x),1,1);
    }

    private void LateUpdate()
    {
        if(_timeElapsed < Time.time){
            _timeElapsed = Time.time + 1/weaponStats.FiringRate;
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack(){
        _startingSpeed = 0f;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 8; i++)
        {
            float angle = Mathf.PI * i / 4;
            Vector3 pos = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle),0);

            ProjectileBase projectileBase = ObjectPooler.Instance.GetProjectile(SkillType.BanaspathiOrb);
            projectileBase.SetDamage(weaponStats.AttackPower);
            projectileBase.gameObject.SetActive(true);
            projectileBase.transform.position = transform.position + pos;
            projectileBase.transform.up = -pos;
            projectileBase.Launch(pos*weaponStats.speed);
        }
        yield return new WaitForSeconds(0.5f);
        _startingSpeed = BaseStats.travelSpeed;
        yield return null;
    }

    public override void Death()
    {
        base.Death();
        GameManager.Instance.ChangeState(GameState.WaveChange);
    }

}   
