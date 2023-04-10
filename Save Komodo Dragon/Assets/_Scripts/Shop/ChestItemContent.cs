using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestItemContent : EvolItemHolder
{
    public TextMeshProUGUI itemNameText;
    
    public void SetItem(ItemBundle itemBundle){
        this.item = itemBundle;
        itemNameText.text = item.scriptableItem.name;
    }
}
