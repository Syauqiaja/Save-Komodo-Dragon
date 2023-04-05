using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothBase : MonoBehaviour
{
    public ObjectUpgraded objectUpgraded;
    public void WearCloth(){
    }
}

[System.Serializable]
public enum ObjectUpgraded{
    GameManager,
    Hero,
}
