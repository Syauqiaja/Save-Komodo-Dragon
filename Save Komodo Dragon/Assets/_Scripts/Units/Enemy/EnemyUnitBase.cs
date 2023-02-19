using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitBase : UnitBase
{
    public int hitDamage {get; private set;}
    public EnemyType enemyType;
    public PickableType pickableType {get; private set;}
    protected Transform _t;
    protected Rigidbody2D _r;
    private Vector3 moveDirection;
    protected Transform heroTransform;
    protected virtual void Awake() {
        _t = transform;
        _r = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start() {
        heroTransform = UnitManager.Instance.heroUnit.transform;
    }
    void FixedUpdate(){
        moveDirection = (heroTransform.position - _t.position).normalized;
        _r.velocity = BaseStats.travelSpeed * moveDirection;
        _t.localScale = new Vector3(Mathf.Sign(moveDirection.x),1,1);
    }
    public void SetProperties(Sprite sprite, int hitDamage, PickableType pickableType){
        GetComponent<SpriteRenderer>().sprite = sprite;
        this.hitDamage = hitDamage;
        this.pickableType = pickableType;
    }
    public override void Death()
    {
        UnitManager.Instance.SpawnPickable(pickableType, transform.position);
        UnitManager.Instance.activeEnemy.Remove(this);
        if(UnitManager.Instance.heroUnit.enemyFront.Contains(transform)) UnitManager.Instance.heroUnit.enemyFront.Remove(transform);
        base.Death();
    }
    public override void Damaged(int hitValue)
    {
        UnitManager.Instance.SpawnTextDamage(hitValue, _t.position);
        base.Damaged(hitValue);
    }
}
