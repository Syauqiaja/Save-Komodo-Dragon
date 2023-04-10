using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroDetail : MonoBehaviour
{
    [Header("GUI")]
    [SerializeField] private TextMeshProUGUI heroNameDetail;
    [SerializeField] private TextMeshProUGUI heroStatsDetail;
    [SerializeField] private TextMeshProUGUI heroStoryDetail;
    [SerializeField] private Image heroImageDetail;
    [SerializeField] private Button upgradeButton;

    [Header("Upgrade Success")]
    [SerializeField] private UITween heroUpgradedPopUp;
    [SerializeField] private TextMeshProUGUI heroUpgradedName;
    [SerializeField] private Image heroUpgradedImage;

    [Header("Systems")]
    [SerializeField] private ItemSetup itemSetup;
    [SerializeField] private DataOverScene dataHolder;
    [SerializeField] private UpgradedHero upgradedHeroPanel;

    private HeroBundle heroBundle;

    public void OpenHeroDetail(HeroBundle heroBundle){
        this.heroBundle = heroBundle;
        ScriptableHero hero = ResourceSystem.Instance.GetHero(heroBundle.heroType);
        heroStoryDetail.text = hero.description;
        heroNameDetail.text = hero.name;
        heroImageDetail.sprite = hero.GetHeroSpriteAtLevel(heroBundle.level);
        upgradeButton.gameObject.SetActive(heroBundle.level < 3);
    }

    public void Upgrade(){
        upgradedHeroPanel.OpenConfirmation(heroBundle);
        upgradedHeroPanel.gameObject.SetActive(true);
        upgradedHeroPanel.GetComponent<UITween>().Show();
    }

    public void ConfirmUpgrade(){
        upgradedHeroPanel.GetComponent<UITween>().Hide();
        int index = dataHolder.HeroBundles.IndexOf(heroBundle);
        OpenHeroDetail(new HeroBundle(heroBundle.isUnlocked, heroBundle.level+1, heroBundle.heroType));
        dataHolder.HeroBundles[index] = heroBundle;
        itemSetup.InitiateUI();
        OpenPopUp();
    }
    void OpenPopUp(){
        ScriptableHero hero = ResourceSystem.Instance.GetHero(heroBundle.heroType);
        heroUpgradedImage.sprite = hero.GetHeroSpriteAtLevel(heroBundle.level);
        heroUpgradedName.text = hero.name;
        heroUpgradedPopUp.gameObject.SetActive(true);
        heroUpgradedPopUp.Show();
    }
}
