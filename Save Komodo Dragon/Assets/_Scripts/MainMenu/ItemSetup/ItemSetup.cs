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
    [SerializeField] GameObject itemDetailContainer;
    [SerializeField] private Image itemDetailImage;
    [SerializeField] private Image itemDetailRarity;
    [SerializeField] private TextMeshProUGUI itemDetailName;
    [SerializeField] private TextMeshProUGUI itemDetailStats;
    [SerializeField] private TextMeshProUGUI itemDetailLevel;
    [SerializeField] private TextMeshProUGUI itemRarityStats;
    [SerializeField] private TextMeshProUGUI goldNeededText;

    [Header("Item Evolution")]
    [SerializeField] private ItemEvolution itemEvolutionPanel;

    [Header("Hero Setup")]
    [SerializeField] private GameObject heroSelectContainer;
    [SerializeField] private Transform heroButtonContainer;
    [SerializeField] private GameObject heroDetailContainer;

    [Header("Hero Detail")]
    [SerializeField] private TextMeshProUGUI heroNameDetail;
    [SerializeField] private TextMeshProUGUI heroStatsDetail;
    [SerializeField] private TextMeshProUGUI heroStoryDetail;
    [SerializeField] private Image heroImageDetail;

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
                armorSlot.sprite = value.itemBundle.item.menuImage;
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
                accessorySlot.sprite = value.itemBundle.item.menuImage;
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

    private ScriptableHero heroSelected;
    private void Awake() {
        DataPresistenceManager.OnAfterLoad += InitiateUI;
    }
    private void OnDestroy(){
        DataPresistenceManager.OnAfterLoad -= InitiateUI;
    }
    private void InitiateUI() {
        if(!dataHolder.IsLoaded) return;
        AssignHeroes();

        heroSelected = dataHolder.GetSelectedHero();
        SetHeroUI(heroSelected);
        UpdateStat();

        itemBags = dataHolder.itemHeld;
        if(itemBags.Count == 0){
            Debug.Log("itembag is null");
            return;
        }
        AssignItemsToInventory();
    }

    private void UpdateStat(){
        ItemBundle armor = dataHolder.GetSelectedArmorBundle();
        ItemBundle accessory = dataHolder.GetSelectedAccessoryBundle();
        int _attack = 
            heroSelected.WeaponSkill.weaponStats[0].AttackPower + 
            (dataHolder.SelectedAccessoryIndex != -1 ? accessory.GetCurrentValue() : 0);
        int _hp = 
            heroSelected.BaseStats.health + 
             (dataHolder.SelectedArmorIndex != -1 ? armor.GetCurrentValue() : 0);
        
        attackText.text = _attack.ToString();
        hpText.text = _hp.ToString();
    }

    private void AssignItemsToInventory(){
        List<ItemButton> itemButtons = new List<ItemButton>(inventoryContainer.GetComponentsInChildren<ItemButton>(true));
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
            if(itemBags[i].item.itemFraction == ItemFraction.Armor){
                SelectedArmorButton = itemButtons[i];
            }else{
                SelectedAccessoryButton = itemButtons[i];
            }
        }
    }
    private void AssignHeroes(){
        List<ScriptableHero> heroes = ResourceSystem.Instance.Heroes;
        HeroButton[] heroButtons = heroButtonContainer.GetComponentsInChildren<HeroButton>(true);
        for (int i = 0; i < heroes.Count; i++)
        {
            heroButtons[i].gameObject.SetActive(true);

            heroButtons[i].SetHero(heroes[i]);
            if(heroes[i].heroType == dataHolder.SelectedHero){
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
        if(ItemHolded.itemBundle.item.itemFraction == ItemFraction.Armor){
            if(SelectedArmorButton != null) SelectedArmorButton.isSelected = false;
            SelectedArmorButton = ItemHolded;
            dataHolder.SetSelectedArmor(ItemHolded.indexAtBag);
        }else{
            if(SelectedAccessoryButton != null) SelectedAccessoryButton.isSelected = false;
            SelectedAccessoryButton = ItemHolded;
            dataHolder.SetSelectedAccessory(ItemHolded.indexAtBag);
        }
        ItemHolded.isSelected = true;
        itemDetailContainer.SetActive(false);
        UpdateStat();
    }

    public void OpenItemDetail(ItemButton itemButton){
        ItemHolded = itemButton;
        _levelUpGold = ItemHolded.itemBundle.level * 100 * (((int)itemButton.itemBundle.rarity) + 1);
        goldNeededText.text = _levelUpGold.ToString() + " Gold";
        itemDetailImage.sprite = itemButton.itemBundle.item.menuImage;
        itemDetailRarity.sprite = ResourceSystem.Instance.GetRaritySprite(itemButton.itemBundle.rarity);
        itemDetailName.text = itemButton.itemBundle.item.menuTitle;
        itemDetailLevel.text = "Level "+ itemButton.itemBundle.level.ToString();
        itemDetailStats.text = "+" + itemButton.itemBundle.GetCurrentValue().ToString()+
            (itemButton.itemBundle.item.itemFraction == ItemFraction.Accessories ? " Atk": " HP");
        itemDetailContainer.SetActive(true);
        mainSetupContainer.SetActive(false);
    }
    public void LevelUpItem(){
        if(dataHolder.Gold > _levelUpGold && ItemHolded.itemBundle.item.MaxLevel() > ItemHolded.itemBundle.level){
            dataHolder.Gold -= _levelUpGold;

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
    void SetHeroUI(ScriptableHero hero){
        weaponSlot.sprite = hero.WeaponSkill.menuImage;
        heroSlot.sprite = hero.menuSprite;
        weaponNameText.text = hero.WeaponSkill.menuName;
        setupTitleText.text = hero.name + " Setup";
    }
    public void SelectHero(){
        // Change selected hero UI
        if(_selectedHeroButton != null) _selectedHeroButton.isSelected = false;
        _selectedHeroButton = _heroHolded;
        _heroHolded.isSelected = true;
        SetHeroUI(_heroHolded.hero);

        // Change selected hero System
        dataHolder.SetSelectedHero(_heroHolded.hero.heroType);

        mainSetupContainer.SetActive(true);
        heroDetailContainer.SetActive(false);
    }
    public void OpenHeroDetail(HeroButton heroButton){
        _heroHolded = heroButton;
        heroNameDetail.text = heroButton.hero.name;
        heroStatsDetail.text = heroButton.hero.BaseStats.ToString();
        heroImageDetail.sprite = heroButton.hero.menuSprite;
        heroStoryDetail.text = heroButton.hero.description;
        heroSelectContainer.SetActive(false);
        heroDetailContainer.SetActive(true);
    }
    public void OpenEvolution(){
        itemEvolutionPanel.gameObject.SetActive(true);
        itemEvolutionPanel.OpenEvolution(ItemHolded.itemBundle);
    }
    public void OpenItemSetup(){
        InitiateUI();
        mainSetupContainer.SetActive(true);
    }

    
}

