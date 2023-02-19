using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTrigger : MonoBehaviour
{
    HeroUnitBase heroUnit;
    EnemyUnitBase enemyTouched;
    private void Awake() {
        heroUnit = transform.parent.GetComponent<HeroUnitBase>();
    }
    private void Update() {
        if(enemyTouched != null){
            heroUnit.Damaged(enemyTouched.hitDamage);
            heroUnit.UpdateHealthBar();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            enemyTouched = other.GetComponent<EnemyUnitBase>();
        }else if(other.CompareTag("BossBound")){
            heroUnit.Death();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            if(enemyTouched == other.GetComponent<EnemyUnitBase>())
                enemyTouched = null;
        }
    }
}
