using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolItemHolder : MonoBehaviour
{
    [SerializeField] protected Image frameImage;
    [SerializeField] protected Image contentImage;
    [HideInInspector] public ItemBundle item {
        get{
            return _item;
        }
        set{
            _item = value;
            if(value != null){
                contentImage.enabled = true;
                contentImage.sprite = value.item.menuImage;
                frameImage.sprite = ResourceSystem.Instance.GetRaritySprite(value.rarity);
            }else{
                contentImage.enabled = false;
                frameImage.sprite = ResourceSystem.Instance.GetRaritySprite(Rarity.Normal);
            }
        }
    }
    private ItemBundle _item = null;

    public void Deselect(){
        item = null;
    }
}
