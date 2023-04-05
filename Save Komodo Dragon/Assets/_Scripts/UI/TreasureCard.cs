using UnityEngine;
using TMPro;

public class TreasureCard : CardGroup
{
    [SerializeField] private TextMeshProUGUI level;
    public void SetTreasureCard(Sprite sprite, string name, string desc, int lvl){
        image.sprite = sprite;
        title.text = name;
        description.text = desc;
        level.text = "Level \n"+lvl+"/5";
    }
}
