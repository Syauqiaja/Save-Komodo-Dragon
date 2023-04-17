using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorRadius : MonoBehaviour
{
    HeroUnitBase heroUnit;
    private void Awake() {
        heroUnit = transform.parent.GetComponent<HeroUnitBase>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            foreach (Transform item in heroUnit.enemyNear)
            {
                if(!item.gameObject.activeInHierarchy) {
                    continue;
                }
                if(item == other.transform){
                    return;
                }
            }
            heroUnit.enemyNear.Add(other.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            if(heroUnit.enemyNear.Contains(other.transform)) heroUnit.enemyNear.Remove(other.transform);
        }
    }
}
