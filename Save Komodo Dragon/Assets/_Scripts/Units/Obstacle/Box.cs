using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // CHANCE BELOW INDEX
    private const int COIN_CHANCE = 4;
    private const int BOMB_CHANCE = 6;
    private const int FOOD_CHANCE = 8;
    private const int MAGNET_CHANCE = 10;

    private PickableType pickableType;

    private void OnEnable() {
        int fillIndex = Random.Range(1,11);
        if(fillIndex <= COIN_CHANCE){
            pickableType = PickableType.Coin;
        }else if(fillIndex <= BOMB_CHANCE){
            pickableType = PickableType.Bomb;
        }else if(fillIndex <= FOOD_CHANCE){
            pickableType = PickableType.Food;
        }else if(fillIndex <= MAGNET_CHANCE){
            pickableType = PickableType.Magnet;
        }
    }
    void OpenBox(){
        PickableBase pickable = ObjectPooler.Instance.GetPickable(pickableType);
        pickable.transform.position = transform.position;
        pickable.gameObject.SetActive(true);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Weapon")){
            OpenBox();
        }
    }
}
