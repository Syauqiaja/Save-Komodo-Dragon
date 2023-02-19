using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base config of unit
public class UnitBase : MonoBehaviour
{
    public Stats BaseStats ;
    public int currentHealth;
    public void SetStats(Stats stats) {
        BaseStats = stats;
        ResetHealth();
    }
    public void ResetHealth(){
        currentHealth = BaseStats.health;
    }
    public virtual void Damaged(int hitValue){
        currentHealth -= hitValue;
        if(currentHealth <= 0) Death();
    }
    public virtual void Death(){
        gameObject.SetActive(false);
    }
}


