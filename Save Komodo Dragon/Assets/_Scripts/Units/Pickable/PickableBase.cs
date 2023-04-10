using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBase : MonoBehaviour
{
    private Collider2D _collider;
    public PickableType pickableType;
    public AnimationCurve animationCurve;
    private Transform target;
    private void Awake() {
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerArea")){
            Picking(other.transform);
        }
    }

    public void Picking(Transform target){
        Vector3 direction = target.position - transform.position;
        Vector3 cPosition = transform.position;
        _collider.enabled = false; 
        LeanTween.value(gameObject, 0f,0.2f, Random.Range(0.5f, 0.7f)).setOnUpdate((float value)=>{
            transform.position = Vector3.LerpUnclamped(transform.position, cPosition-direction.normalized*2, value);
        }).setOnComplete(()=>{
            LeanTween.value(gameObject, 0f,0.5f, Random.Range(0.5f, 0.7f)).setOnUpdate((float value)=>{
                transform.position = Vector3.LerpUnclamped(transform.position, target.position, value);
            }).setOnComplete(Picked);
        });
        // LeanTween.value(gameObject, 0f,1f, direction.magnitude).setOnUpdate((float value)=>{
        //     transform.position = Vector3.LerpUnclamped(transform.position, target.position, value);
        // }).setEase(animationCurve).setOnComplete(Picked);
    }

    IEnumerator PickingCoroutine(Transform target){
        yield return null;
    }

    protected virtual void Picked(){
        _collider.enabled = true;
        gameObject.SetActive(false);
    }
}
public enum PickableType{
    smallDiamond,
    bigDiamond,
    blueDiamond,
    goldDiamond,
    Food,
    Magnet,
    Coin,
    Chest,
    Bomb,
}
