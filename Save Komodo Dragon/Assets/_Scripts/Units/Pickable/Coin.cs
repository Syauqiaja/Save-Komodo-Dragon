using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickableBase
{
    protected override void Picked()
    {
        base.Picked();
        GameManager.Instance.GoldPicked(100);
    }
}
