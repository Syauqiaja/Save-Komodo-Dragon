using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectil : ProjectileBase, IHeroDamager
{
    private Rigidbody2D _rigid;

    private void Awake() {
        _rigid = GetComponent<Rigidbody2D>();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
    }
    public override void Launch(Vector2 direction)
    {
        base.Launch(direction);
        transform.up = -direction;
        _rigid.velocity = direction;
        Invoke("Disable", lifeTime);
    }
    void Disable(){
        gameObject.SetActive(false);
    }

    public void GetDamage(out int damage)
    {
        damage = hitDamage;
    }
}
