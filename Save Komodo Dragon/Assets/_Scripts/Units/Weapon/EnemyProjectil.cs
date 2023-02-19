using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectil : ProjectileBase
{
    private Rigidbody2D _rigid;

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            UnitManager.Instance.heroUnit.Damaged(hitDamage);
        }else if(other.CompareTag("BossBound")) gameObject.SetActive(false);
    }
    public override void Launch(Vector2 direction)
    {
        base.Launch(direction);
        _rigid.velocity = direction;
    }
}
