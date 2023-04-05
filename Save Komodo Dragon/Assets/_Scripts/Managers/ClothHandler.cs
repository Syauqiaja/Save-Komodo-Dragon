using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothHandler : StaticInstance<ClothHandler>
{
    [HideInInspector] public float expMultiplier = 1f;
    [HideInInspector] public float hpGainMultiplier = 0;
    [HideInInspector] public float speedMultiplier = 1f;
    [HideInInspector] public float maxHealthMultiplier = 1f;
    [HideInInspector] public float damageReduce = 1f;
    public void WearCloth(ClothType type, float amount){
        switch (type)
        {
            case ClothType.KingBaba:
                if(hpGainMultiplier == 0){
                    hpGainMultiplier = amount;
                    StartCoroutine(HealPerSec());
                }else{
                    hpGainMultiplier = amount;
                }
                break;
            case ClothType.Barong:
                speedMultiplier = amount;
                break;
            case ClothType.AsmatShield:
                damageReduce = amount;
                break;
            case ClothType.KotangAntakusuma:
                maxHealthMultiplier = amount;
                break;
            case ClothType.TiilanggaHat:
                expMultiplier = amount;
                break;
            default:
                break;
        }
    }

    IEnumerator HealPerSec(){
        while (true)
        {
            UnitManager.Instance.heroUnit.Heal(hpGainMultiplier);
            yield return new WaitForSeconds(5f);
        }
    }
}
