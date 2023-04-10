using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSetup : MonoBehaviour
{
    [SerializeField] private DataOverScene dataHolder;
    [Header("Main Setup")]
    [SerializeField] GameObject mainSetupContainer;
    [SerializeField] private TextMeshProUGUI setupTitleText;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Image heroSlot;
    [SerializeField] private Image weaponSlot;
    [SerializeField] private Image accessorySlot;
    [SerializeField] private Image accessorySlotRarity;
    [SerializeField] private Image armorSlot;
    [SerializeField] private Image armorSlotRarity;
    [SerializeField] private Transform inventoryContainer;

    [Header("Item Detail")]
    [SerializeField] ItemDetail itemDetailContainer;

    [Header("Hero Setup")]
    [SerializeField] private GameObject heroSelectContainer;
    [SerializeField] private Transform heroButtonContainer;

    [Header("Hero Detail")]
    [SerializeField] private HeroDetail heroDetailContainer;

    [Header("Hero Dialog")]
    [SerializeField] private GameObject heroBuyFailed;
    [SerializeField] private BuyHeroConfirmation heroBuyConfirmation;

    private List<ItemBundle> itemBags = null;
    private HeroButton _selectedHeroButton;
    private ItemButton SelectedArmorButton{
        get{
            return _selectedArmorButton;
        }
        set{
            _selectedArmorButton = value;
            if(value != null){
                armorSlot.enabled = true;
                armorSlot.sprite = value.itemBundle.scriptableItem.menuImage;
                armorSlotRarity.sprite = ResourceSystem.Instance.GetRaritySprite(value.itemBundle.rarity);
            }else{
                armorSlot.enabled = false;
                armorSlotRarity.sprite = ResourceSystem.Instance.GetRaritySprite(Rarity.Normal);
            }
        }
    }
    private ItemButton _selectedArmorButton;
    private ItemButton SelectedAccessoryButton{
        get{
            return _selectedAccessoryButton;
        }
        set{
            _selectedAccessoryButton = value;
            if(value != null){
                accessorySlot.enabled = true;
                accessorySlot.sprite = value.itemBundle.scriptableItem.menuImage;
                accessorySlotRarity.sprite = ResourceSystem.Instance.GetRaritySprite(value.itemBundle.rarity);
            }else{
                accessorySlot.enabled = false;
                accessorySlotRarity.sprite = ResourceSystem.Instance.GetRaritySprite(Rarity.Normal);
            }
        }
    }
    private ItemButton _selectedAccessoryButton;
    private ItemButton ItemHolded;
    private HeroButton _heroHolded;
    private int _levelUpGold;

    private HeroBundle heroSelected;
    private void Awake() {
        DataPresistenceManager.OnAfterLoad += InitiateUI;
    }
    private void OnDestroy(){
        DataPresistenceManager.OnAfterLoad -= InitiateUI;
    }
    public void InitiateUI() {
        if(!dataHolder.IsLoaded) return;
        AssignHeroes();

        heroSelected = dataHolder.GetSelectedHero();
        SetHeroUI(heroSelected);

        itemBags = dataHolder.itemHeld;
        if(itemBags.Count == 0){
            Debug.Log("itembag is null");
            return;
        }
        AssignItemsToInventory();
        UpdateStat();
    }

    private void UpdateStat(){
        ScriptableHero hero = ResourceSystem.Instance.GetHero(heroSelected.heroType);
        ItemBundle armor = dataHolder.GetSelectedArmorBundle();
        ItemBundle accessory = dataHolder.GetSelectedAccessoryBundle();
        int _attack = 
            hero.WeaponSkill.weaponStats[0].AttackPower + 
            (dataHolder.SelectedAccessoryIndex != -1 ? accessory.GetCurrentValue() : 0);
        int _hp = 
            hero.BaseStats.health + 
             (dataHolder.SelectedArmorIndex != -1 ? armor.GetCurrentValue() : 0);
        
        attackText.text = _attack.ToString();
        hpText.text = _hp.ToString();
    }

    private void AssignItemsToInventory(){
        List<ItemButton> itemButtons = new List<ItemButton>(inventoryContainer.GetComponentsInChildren<ItemButton>(true));
        foreach (ItemButton item in itemButtons)
        {
            if(item.gameObject.activeSelf) item.gameObject.SetActive(false);
            else break;
        }

        for (int i = 0; i < itemBags.Count; i++)
        {
            if(i >= itemButtons.Count){
                ItemButton newButton = Instantiate(itemButtons[itemButtons.Count-1], inventoryContainer);
                itemButtons.Add(newButton);
            }
            itemButtons[i].gameObject.SetActive(true);
            itemButtons[i].SetItem(itemBags[i]);
            itemButtons[i].indexAtBag = i;

            if(itemBags[i].isSelected)
            if(itemBags[i].scriptableItem.itemFraction == ItemFraction.Armor){
                SelectedArmorButton = itemButtons[i];
            }else{
                SelectedAccessoryButton = itemButtons[i];
            }
        }
    }
    public void AssignHeroes(){
        HeroButton[] heroButtons = heroButtonContainer.GetComponentsInChildren<HeroButton>(true);
        for (int i = 0; i < dataHolder.HeroBundles.Count; i++)
        {
            heroButtons[i].gameObject.SetActive(true);
            heroButtons[i].SetHero(dataHolder.HeroBundles[i]);

            if(dataHolder.HeroBundles[i].heroType == dataHolder.SelectedHero){
                heroButtons[i].isSelected = true;
                _selectedHeroButton = heroButtons[i];
            }
            if(dataHolder.HeroBundles[i].isUnlocked) heroButtons[i].Unlock();
        }
    }
    public void BuyHero(ScriptableHero hero){
        if(dataHolder.Crystal > hero.heroPrice){
            heroBuyConfirmation.gameObject.SetActive(true);
            heroBuyConfirmation.OpenBuyHero(hero);
        }else{
            heroBuyFailed.SetActive(true);
        }
    }

    public void HeroBought(ScriptableHero hero){
        dataHolder.UnlockHero(hero.heroType);
        dataHolder.Crystal -= hero.heroPrice;
        AssignHeroes();
    }

    public void SelectItem(){
        mainSetupContainer.SetActive(true);
        if(ItemHolded.itemBundle.scriptableItem.itemFraction == ItemFraction.Armor){
            if(SelectedArmorButton != null) SelectedArmorButton.isSelected = false;
            SelectedArmorButton = ItemHolded;
            dataHolder.SetSelectedArmor(ItemHolded.indexAtBag);
        }else{
            if(SelectedAccessoryButton != null) SelectedAccessoryButton.isSelected = false;
            SelectedAccessoryButton = ItemHolded;
            dataHolder.SetSelectedAccessory(ItemHolded.indexAtBag);
        }
        ItemHolded.isSelected = true;
        itemDetailContainer.gameObject.SetActive(false);
        UpdateStat();
    }

    public void OpenItemDetail(ItemButton itemButton){
        ItemHolded = itemButton;
        itemDetailContainer.gameObject.SetActive(true);
        itemDetailContainer.OpenItemDetail(itemButton.itemBundle, itemButton.indexAtBag);
        mainSetupContainer.SetActive(false);
    }
    public void LevelUpItem(int levelUpGold){
        if(dataHolder.Gold > levelUpGold && ItemHolded.itemBundle.scriptableItem.MaxLevel() > ItemHolded.itemBundle.level){
            dataHolder.Gold -= levelUpGold;

            int level = ItemHolded.itemBundle.level+1;
            ItemHolded.itemBundle.level = level;
            itemBags[ItemHolded.indexAtBag].level = level;
            dataHolder.itemHeld[ItemHolded.indexAtBag].level = level;
            OpenItemDetail(ItemHolded);
            UpdateStat();
        }
    }
    public void OpenHeroSelect(){
        mainSetupContainer.SetActive(false);
        heroSelectContainer.SetActive(true);
    }
    void SetHeroUI(HeroBundle hero){
        ScriptableHero _hero = ResourceSystem.Instance.GetHero(hero.heroType);
        weaponSlot.sprite = _hero.WeaponSkill.menuImage;
        heroSlot.sprite = _hero.GetHeroSpriteAtLevel(hero.level);
        weaponNameText.text = _hero.WeaponSkill.menuName;
        setupTitleText.text = _hero.name + " Setup";
    }
    public void SelectHero(){
        // Change selected hero UI
        if(_selectedHeroButton != null) _selectedHeroButton.isSelected = false;
        _selectedHeroButton = _heroHolded;
        _heroHolded.isSelected = true;
        SetHeroUI(_heroHolded.heroBundle);

        // Change selected hero System
        dataHolder.SetSelectedHero(_heroHolded.scriptableHero.heroType);
        heroDetailContainer.gameObject.SetActive(false);
        mainSetupContainer.SetActive(true);
    }
    public void OpenHeroDetail(HeroButton heroButton){
        _heroHolded = heroButton;
        heroSelectContainer.SetActive(false);
        heroDetailContainer.OpenHeroDetail(heroButton.heroBundle);
        heroDetailContainer.gameObject.SetActive(true);
    }
    public void OpenItemSetup(){
        mainSetupContainer.SetActive(true);
        InitiateUI();
    }

    
}

