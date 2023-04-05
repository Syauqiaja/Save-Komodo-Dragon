using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBG : MonoBehaviour
{
    [SerializeField] private bool moveVertical = true;
    [SerializeField] private bool moveHorizontal = true;
    private Transform heroTransform;
    private Material material;

    private void Awake(){
        GameManager.OnAfterStateChanged += SetHeros;
        material = GetComponent<Renderer>().material;
    }

    void SetHeros(GameState state){
        if(state != GameState.Starting) return;
        heroTransform = UnitManager.Instance.heroUnit.transform;
    }

    private void Update() {
        Vector3 targetPos = new Vector3(
            moveHorizontal? heroTransform.position.x : 0,
            moveVertical? heroTransform.position.y : 0,
            0);
        transform.position = targetPos;
        material.mainTextureOffset = new Vector2(targetPos.x / transform.localScale.x, targetPos.y / transform.localScale.y);
    }
}
