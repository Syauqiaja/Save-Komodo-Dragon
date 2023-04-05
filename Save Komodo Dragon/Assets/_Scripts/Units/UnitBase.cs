using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base config of any unit (Hero & Enemy)
public class UnitBase : MonoBehaviour
{
    public Stats BaseStats ;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float maxHealth;
    public void SetStats(Stats stats) {
        BaseStats = stats;
        maxHealth = BaseStats.health;
        currentHealth = maxHealth;
    }
    public virtual void Damaged(float hitValue){
        currentHealth -= hitValue;
        if(currentHealth <= 0) Death();
    }
    public virtual void Death(){
        gameObject.SetActive(false);
    }
}


