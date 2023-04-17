using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LemparSandal : ProjectileBase
{
    [SerializeField] private Rigidbody2D _rigidbody;
    public override void Launch(Vector2 direction)
    {
        _rigidbody.velocity = direction;
        Invoke("Disable", lifeTime);
    }

    void Disable(){
        gameObject.SetActive(false);
    }
}
