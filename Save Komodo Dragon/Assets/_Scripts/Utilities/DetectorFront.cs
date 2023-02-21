using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorFront : MonoBehaviour
{
    HeroUnitBase heroUnit;
    Transform _t;
    private void Awake() {
        heroUnit = transform.parent.GetComponent<HeroUnitBase>();
        _t = transform;
    }
    private void Update() {
        _t.right = heroUnit.facingDirection;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            foreach (Transform item in heroUnit.enemyFront)
            {
                if(!item.gameObject.activeInHierarchy) {
                    continue;
                }
                if(item == other.transform){
                    return;
                }
            }
            heroUnit.enemyFront.Add(other.transform);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            if(heroUnit.enemyFront.Contains(other.transform)) heroUnit.enemyFront.Remove(other.transform);
        }
    }
}
