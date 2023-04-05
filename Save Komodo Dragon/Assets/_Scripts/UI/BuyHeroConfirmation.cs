using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyHeroConfirmation : MonoBehaviour
{
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroNameText;
    [SerializeField] TextMeshProUGUI detailText;
    [SerializeField] Button confirmButton;
    [SerializeField] ItemSetup itemSetup;

    private ScriptableHero hero;

    public void OpenBuyHero(ScriptableHero scriptableHero){
        heroImage.sprite = scriptableHero.menuSprite;
        heroNameText.text = scriptableHero.name;
        detailText.text = "Buy this hero by "+scriptableHero.heroPrice+"<sprite name=\"Crystal\"> ?";
        hero = scriptableHero;
    }

    public void Buy(){
        itemSetup.HeroBought(hero);
        gameObject.SetActive(false);
    }
}
