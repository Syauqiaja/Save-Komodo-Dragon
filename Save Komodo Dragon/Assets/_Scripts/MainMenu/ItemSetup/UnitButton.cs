using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnitButton : MonoBehaviour
{
    public Image _itemImage;
    public Button _button;
    public ItemSetup setup;
    protected virtual void Awake() {
        _button.onClick.AddListener(MoveToDetail);
    }
    public virtual void MoveToDetail(){
    }
}
