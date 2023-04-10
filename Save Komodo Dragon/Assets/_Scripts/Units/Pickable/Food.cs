using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : PickableBase
{
    protected override void Picked()
    {
        UnitManager.Instance.heroUnit.Heal(500);
        base.Picked();
    }
}
