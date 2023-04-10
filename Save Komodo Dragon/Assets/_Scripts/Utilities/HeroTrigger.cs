using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTrigger : MonoBehaviour
{
    HeroUnitBase heroUnit;
    // EnemyUnitBase enemyTouched;
    List<Collider2D> enemyColls = new List<Collider2D>();
    private void Awake() {
        heroUnit = transform.parent.GetComponent<HeroUnitBase>();
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            int damage;
            other.GetComponent<IHeroDamager>().GetDamage(out damage);
            heroUnit.Damaged(damage);
            if(PlayerPrefs.GetInt("vibration") == 1) Handheld.Vibrate();
            heroUnit.UpdateHealthBar();
        }
    }
}
