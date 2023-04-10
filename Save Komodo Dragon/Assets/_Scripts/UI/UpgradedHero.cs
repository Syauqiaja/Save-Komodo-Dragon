using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradedHero : MonoBehaviour
{
    [SerializeField] private Image heroImage;
    [SerializeField] private UITween heroImageTween;
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private TextMeshProUGUI heroPriceText;

    public void OpenConfirmation(HeroBundle heroBundle){
        ScriptableHero hero = ResourceSystem.Instance.GetHero(heroBundle.heroType);
        heroImage.sprite = hero.GetHeroSpriteAtLevel(heroBundle.level+1);
        heroName.text = hero.name;
        heroPriceText.text = "Upgrade this hero by "+hero.heroPrice+"<sprite name=\"Crystal\">";
    }
}
