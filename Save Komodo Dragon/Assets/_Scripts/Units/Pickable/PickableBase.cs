using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBase : MonoBehaviour
{
    public PickableType pickableType;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerArea")){
            Picking(other.transform);
        }
    }

    private void Picking(Transform target){
        LeanTween.value(gameObject, 0f,0.5f, 0.5f).setOnUpdate((float value)=>{
            transform.position = Vector3.Lerp(transform.position, target.position, value);
        }).setEaseInCubic().setOnComplete(Picked);
    }

    protected virtual void Picked(){
        gameObject.SetActive(false);
    }
}
public enum PickableType{
    smallDiamond,
    bigDiamond,
}
