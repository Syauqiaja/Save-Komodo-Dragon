using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GiftHandler : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;
    public List<Image> otherItems;

    public void SetGold(int value){
        string _k = value > 1000?"K":"";
        int fixedValue = value > 1000? value/1000 : value;
        goldText.text = fixedValue.ToString() + _k;
        goldText.transform.parent.gameObject.SetActive(true);
    }
    public void SetCrystal(int value){
        string _k = value > 1000?"K":"";
        int fixedValue = value > 1000? value/1000 : value;
        crystalText.text = fixedValue.ToString() + _k;
        crystalText.transform.parent.gameObject.SetActive(true);
    }
    public void SetItems(List<ScriptableItem> listItems){
        for (int i = 0; i < listItems.Count; i++)
        {
            otherItems[i].sprite = listItems[i].menuImage;
            otherItems[i].transform.parent.gameObject.SetActive(true);
        }
    }
}
