using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PickableBase
{
    protected override void Picked()
    {
        base.Picked();
        UnitManager.Instance.KillEnemies();
    }
}
