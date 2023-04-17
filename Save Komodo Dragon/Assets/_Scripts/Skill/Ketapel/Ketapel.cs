using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ketapel : SkillBase
{
    [SerializeField] private Animator animator;
    [SerializeField] private ProjectileBase projectileBase;
    public override void Attack(Vector3 CenterPos, Vector3 direction)
    {
        animator.SetTrigger("Launch");
        float length = animator.GetCurrentAnimatorStateInfo(0).length;
        StartCoroutine(RapidAttack(length));
    }

    IEnumerator RapidAttack(float delay){
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < weaponStats.size; i++)
        {
            Transform target = heroUnit.enemyNear[Random.Range(0,heroUnit.enemyNear.Count)];
            ProjectileBase projectile = ObjectPooler.Instance.GetOrSetProjectile(projectileBase);
            projectile.gameObject.SetActive(true);
            projectile.SetDamage(weaponStats.AttackPower);
            projectile.transform.position = new Vector3(target.position.x, target.position.y + 8f, 0f);
            projectile.Launch(target.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
