using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZaktiClaw : ProjectileBase
{
    [SerializeField] private Animator _animator;
    public override void Launch(Vector2 direction)
    {
        transform.right = direction;
        _animator.SetTrigger("Attack");
    }
}
