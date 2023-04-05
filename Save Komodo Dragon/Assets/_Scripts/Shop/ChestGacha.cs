using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGacha : MonoBehaviour
{
    [SerializeField] private Transform itemContentContainer;
    [SerializeField] private DataOverScene dataHolder;
    private List<ChestItemContent> itemContents;
    private void Awake() {
        itemContents = new List<ChestItemContent>(itemContentContainer.GetComponentsInChildren<ChestItemContent>(true));
    }

    public void AddRandomItem(int count,Rarity rarity){
        for (int i = 0; i < count; i++)
        {
            ItemBundle itemBundle = (new ItemBundle(ResourceSystem.Instance.GetRandomItem(), 1,false, rarity));
            itemContents[i].SetItem(itemBundle);
            itemContents[i].gameObject.SetActive(true);
            dataHolder.AddItem(itemBundle);
        }
    }
    public void AddRandomItem(int count){
        for (int i = 0; i < count; i++)
        {
            ItemBundle itemBundle = new ItemBundle(ResourceSystem.Instance.GetRandomItem(), 1,false, Rarity.Normal);
            itemContents[i].SetItem(itemBundle);
            itemContents[i].gameObject.SetActive(true);
            dataHolder.AddItem(itemBundle);
        }
    }
    public void CloseChest(){
        foreach (ChestItemContent item in itemContents)
        {
            item.gameObject.SetActive(false);
        }
    }
}
