using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemButton : UnitButton
{
    public Image selectedFrame;
    public Image frameImage;
    public int indexAtBag = -1; // -1 means not in bag
    public bool isSelected{get {
        return selectedFrame.gameObject.activeSelf;
    } set{
        selectedFrame.gameObject.SetActive(value);
    }}
    public ItemBundle itemBundle;

    public override void MoveToDetail()
    {
        base.MoveToDetail();
        setup.OpenItemDetail(this);
    }
    public void SetItem(ItemBundle itemBundle){
        this.itemBundle = itemBundle;
        _itemImage.sprite = itemBundle.item.menuImage;
        frameImage.sprite = ResourceSystem.Instance.GetRaritySprite(itemBundle.rarity);
        this.isSelected = itemBundle.isSelected;
    }
}
