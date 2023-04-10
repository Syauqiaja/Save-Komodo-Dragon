using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDetail : MonoBehaviour
{  
    [Header("GUI")]
    [SerializeField] private Image itemDetailImage;
    [SerializeField] private Image itemDetailRarity;
    [SerializeField] private TextMeshProUGUI itemDetailName;
    [SerializeField] private TextMeshProUGUI itemDetailStats;
    [SerializeField] private TextMeshProUGUI itemDetailLevel;
    [SerializeField] private TextMeshProUGUI itemRarityStats;
    [SerializeField] private TextMeshProUGUI goldNeededText;

    [Header("Item System")]
    [SerializeField] private ItemSetup itemSetup;
    [SerializeField] private ItemEvolution itemEvolution;
    [SerializeField] private DataOverScene dataHolder;

    private ItemBundle itemHolded;
    private int _levelUpGold;
    private int index;

    public void OpenItemDetail(ItemBundle itemBundle, int index){
        itemHolded = itemBundle;
        this.index = index;
        _levelUpGold = itemBundle.level * 100 * (((int)itemBundle.rarity) + 1);
        goldNeededText.text = _levelUpGold.ToString() + " Gold";
        itemDetailImage.sprite = itemBundle.scriptableItem.menuImage;
        itemDetailRarity.sprite = ResourceSystem.Instance.GetRaritySprite(itemBundle.rarity);
        itemDetailName.text = itemBundle.scriptableItem.menuTitle;
        itemDetailLevel.text = "Level "+ itemBundle.level.ToString();
        itemDetailStats.text = "+" + itemBundle.GetCurrentValue().ToString()+
            (itemBundle.scriptableItem.itemFraction == ItemFraction.Accessories ? " Atk": " HP");
    }

    public void SelectItem(){
        if(itemHolded.scriptableItem.itemFraction == ItemFraction.Accessories){
            dataHolder.SetSelectedAccessory(index);
        }else if(itemHolded.scriptableItem.itemFraction == ItemFraction.Armor){
            dataHolder.SetSelectedArmor(index);
        }
        itemSetup.OpenItemSetup();
        gameObject.SetActive(false);
    }
    public void OpenEvolution(){
        itemEvolution.gameObject.SetActive(true);
        itemEvolution.OpenEvolution(itemHolded, index);
    }
    public void LevelUp(){
        itemSetup.LevelUpItem(_levelUpGold);
    }
}
