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

    [HideInInspector] public ScriptableHero hero;

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

    public void SetHero(ScriptableHero hero){
        this.hero = hero;
        this._itemImage.sprite = hero.menuSprite;
        this._heroName.text = hero.name;
        this._heroName2.text = hero.name;
        this.heroPrice.text = "<sprite name=\"Crystal\"> "+hero.heroPrice;
    }

    public void Buy(){
        setup.BuyHero(hero);
    }
}
