using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chest : PickableBase
{
    protected override void Picked(){
        GameManager.Instance.OpenChest();
        base.Picked();
    }
}
