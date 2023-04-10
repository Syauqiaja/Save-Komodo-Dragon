using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : PickableBase
{
    protected override void Picked()
    {
        ObjectPooler.Instance.CollectAllDiamonds();
        base.Picked();
    }
}
