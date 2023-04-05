using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public SkillType skillType;
    public float lifeTime = 1;
    protected int hitDamage=0;

    public void SetDamage(int value) {hitDamage = value;}

    public virtual void Launch(Vector2 direction){
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            other.GetComponent<EnemyUnitBase>().Damaged(hitDamage);
        }
    }
}