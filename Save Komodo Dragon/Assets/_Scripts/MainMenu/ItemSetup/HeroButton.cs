using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroButton : UnitButton
{
    public TextMeshProUGUI _heroName;
    public TextMeshProUGUI _heroName2;
    public Image selectedFrame;
    public GameObject LockFrame;
    public Button buyButton;
    public TextMeshProUGUI heroPrice;

    [HideInInspector] public ScriptableHero scriptableHero;
    [HideInInspector] public HeroBundle heroBundle;

    public bool isSelected{get {
        return selectedFrame.gameObject.activeSelf;
    } set{
        selectedFrame.gameObject.SetActive(value);
    }}

    public override void MoveToDetail()
    {
        base.MoveToDetail();
        setup.OpenHeroDetail(this);
    }

    public void Unlock(){
        LockFrame.SetActive(false);
        _button.interactable = true;
    }

    public void SetHero(HeroBundle _hero){
        this.scriptableHero = ResourceSystem.Instance.GetHero(_hero.heroType);
        this.heroBundle = _hero;
        if(!_hero.isUnlocked){
            this._itemImage.sprite = scriptableHero.GetHeroSpriteAtLevel(1);
        }else{
            this._itemImage.sprite = scriptableHero.GetHeroSpriteAtLevel(_hero.level);
        }
        this._heroName.text = scriptableHero.name;
        this._heroName2.text = scriptableHero.name;
        this.heroPrice.text = "<sprite name=\"Crystal\"> "+scriptableHero.heroPrice;
    }

    public void Buy(){
        setup.BuyHero(scriptableHero);
    }
}
