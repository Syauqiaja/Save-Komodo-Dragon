using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolItemButton : EvolItemHolder
{
    [SerializeField] ItemEvolution itemEvolution;
    
    public void SetItem(ItemBundle itemBundle){
        item = itemBundle;
        if(itemEvolution != null){
            GetComponent<Button>().onClick.AddListener(()=>{itemEvolution.AddToSlot(this);});
        }
    }
}
