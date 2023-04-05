using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableActivator : MonoBehaviour
{
    // public GameObject activate;
    // public List<GameObject> deactivates = new List<GameObject>();
    private void OnEnable() {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnDisable() {
        
        for(int i = 1; i<transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
