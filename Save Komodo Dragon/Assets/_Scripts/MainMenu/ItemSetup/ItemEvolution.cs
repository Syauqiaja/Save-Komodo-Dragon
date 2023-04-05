using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvolution : MonoBehaviour
{
    public DataOverScene dataHolder;
    [SerializeField] private Transform itemButtonContainer;

    [Header("Item Joins")]
    [SerializeField] private EvolItemHolder resultItem;
    [SerializeField] private EvolItemHolder holdedItem1;
    [SerializeField] private EvolItemHolder holdedItem2;

    private List<EvolItemButton> itemButtons;
    private void Awake() {
        ClearSlot();
        itemButtons = new List<EvolItemButton>(itemButtonContainer.GetComponentsInChildren<EvolItemButton>(true));
    }
    private void ResetContainer(){
        foreach(EvolItemButton itemButton in itemButtons){
            if(!itemButton.gameObject.activeSelf) return;
            itemButton.gameObject.SetActive(false);
        }
    }
    private void ClearSlot(){
        holdedItem1.item = null;
        holdedItem2.item = null;
        resultItem.item = null;
    }
    public void OpenEvolution(ItemBundle itemBundle){
        int i = 0; //Item buttons iterator
        ResetContainer();
        foreach (ItemBundle item in dataHolder.itemHeld)
        {
            if(item.item.itemType == itemBundle.item.itemType){
                itemButtons[i].gameObject.SetActive(true);
                itemButtons[i].SetItem(item);
                i++;
            }
        }
    }

    public void AddToSlot(EvolItemButton itemAdded){
        // return when item holded
        if(itemAdded.item == holdedItem1.item || itemAdded.item == holdedItem2.item) return;

        if(holdedItem1.item == null || holdedItem1.item.rarity != itemAdded.item.rarity){
            holdedItem1.item = itemAdded.item;
        }else{
            holdedItem2.item = itemAdded.item;
        }

        // Check if both items have same rarity
        if(holdedItem1.item != null && holdedItem2.item != null)
        if(holdedItem1.item.rarity == holdedItem2.item.rarity){
            resultItem.item = new ItemBundle(itemAdded.item.item, 1, false, (Rarity)(((int)itemAdded.item.rarity) + 1));
        }else{
            resultItem.item = null;
        }
    }

    public void Evolve(){
        if(resultItem.item == null) return;
        dataHolder.itemHeld.Remove(holdedItem1.item);
        dataHolder.itemHeld.Remove(holdedItem2.item);
        dataHolder.itemHeld.Add(resultItem.item);
        OpenEvolution(resultItem.item);
        ClearSlot();
    }
}
