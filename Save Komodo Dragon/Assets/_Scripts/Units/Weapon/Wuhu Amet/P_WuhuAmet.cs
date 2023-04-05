using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_WuhuAmet : ProjectileBase
{
    [SerializeField] private Rigidbody2D _rigidbody;
    public override void Launch(Vector2 direction)
    {
        base.Launch(direction);
        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        StartCoroutine(DeactiveAfter());
    }
    IEnumerator DeactiveAfter(){
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.CompareTag("Enemy")){
            gameObject.SetActive(false);
            StopCoroutine(DeactiveAfter());
        }
    }
}
