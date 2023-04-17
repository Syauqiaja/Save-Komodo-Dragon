using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_PoisonousSpear : ProjectileBase
{
    [SerializeField] private Animator animator;
    public override void Launch(Vector2 direction)
    {
        transform.right = direction;
        animator.SetTrigger("Attack");
    }
}
