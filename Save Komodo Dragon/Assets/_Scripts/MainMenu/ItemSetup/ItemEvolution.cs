using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEvolution : MonoBehaviour
{
    public DataOverScene dataHolder;
    [SerializeField] private Transform itemButtonContainer;
    [SerializeField] private ItemDetail itemDetail;
    [SerializeField] private Button evolveButton;
    [SerializeField] private UITween topUITween, botUITween;

    [Header("Item Joins")]
    [SerializeField] private EvolItemHolder resultItem;
    [SerializeField] private EvolItemHolder holdedItem1;
    [SerializeField] private EvolItemHolder holdedItem2;

    private List<EvolItemButton> itemButtons;
    private ItemBundle _itemBundle;
    private int index;
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
        evolveButton.interactable = false;
        holdedItem1.item = null;
        holdedItem2.item = null;
        resultItem.item = _itemBundle;
    }
    public void OpenEvolution(ItemBundle itemBundle, int index){
        int i = 0; //Item buttons iterator
        this.index = index;
        _itemBundle = itemBundle;
        resultItem.item = _itemBundle;
        ResetContainer();
        foreach (ItemBundle item in dataHolder.itemHeld)
        {
            if(item.scriptableItem.itemType == itemBundle.scriptableItem.itemType && item != itemBundle && item.rarity == itemBundle.rarity){
                itemButtons[i].gameObject.SetActive(true);
                itemButtons[i].SetItem(item);
                i++;
            }
        }
    }

    public void BackToDetail(){
        itemDetail.gameObject.SetActive(true);
        itemDetail.OpenItemDetail(_itemBundle,index);
        topUITween.Hide();
        botUITween.Hide();
        Invoke("CloseThis", botUITween.delay + botUITween.leanTime);
    }

    void CloseThis(){
        ClearSlot();
        gameObject.SetActive(false);
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
            resultItem.item = new ItemBundle(itemAdded.item.scriptableItem, 1, false, (Rarity)(((int)itemAdded.item.rarity) + 1));
            evolveButton.interactable = true;
        }else{
            resultItem.item = _itemBundle;
            evolveButton.interactable = false;
        }
    }

    public void Evolve(){
        if(resultItem.item == null) return;
        dataHolder.itemHeld.Remove(holdedItem1.item);
        dataHolder.itemHeld.Remove(holdedItem2.item);
        dataHolder.itemHeld.Remove(_itemBundle);
        dataHolder.itemHeld.Add(resultItem.item);
        itemDetail.gameObject.SetActive(true);
        itemDetail.OpenItemDetail(resultItem.item, dataHolder.itemHeld.Count-1);
        ClearSlot();

        topUITween.Hide();
        botUITween.Hide();
        Invoke("CloseThis", botUITween.delay + botUITween.leanTime);
    }
    public void Deselect(EvolItemHolder deselectedItem){
        deselectedItem.item = null;
        evolveButton.interactable=false;
        resultItem.item = _itemBundle;
    }
}
