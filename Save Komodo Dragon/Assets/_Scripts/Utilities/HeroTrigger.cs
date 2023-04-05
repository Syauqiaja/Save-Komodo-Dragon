using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTrigger : MonoBehaviour
{
    HeroUnitBase heroUnit;
    EnemyUnitBase enemyTouched;
    List<Collider2D> enemyColls = new List<Collider2D>();
    private void Awake() {
        heroUnit = transform.parent.GetComponent<HeroUnitBase>();
    }
    // private void Update() {
    //     if(enemyColls.Count > 0 && Time.timeScale > 0){
    //         heroUnit.Damaged(enemyTouched.hitDamage);
    //         heroUnit.UpdateHealthBar();
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            enemyColls.Add(other);
            enemyTouched = other.GetComponent<EnemyUnitBase>();
            heroUnit.Damaged(enemyTouched.hitDamage);
            heroUnit.UpdateHealthBar();
        }else if(other.CompareTag("BossBound")){
            heroUnit.Death();
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            enemyColls.Remove(other);
            if(enemyColls.Count == 0) enemyTouched = null;
        }
    }
}
