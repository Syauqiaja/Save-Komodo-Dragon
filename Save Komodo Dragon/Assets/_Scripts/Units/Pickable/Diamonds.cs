using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : PickableBase
{
    [SerializeField] private int diamondValue;
    protected override void Picked()
    {
        GameManager.Instance.AddExp(diamondValue);
        base.Picked();
    }
}
