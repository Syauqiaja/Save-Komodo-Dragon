using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamonds : PickableBase
{
    [SerializeField] private int diamondValue;
    private void OnEnable() {
        ObjectPooler.GetAllDiamonds += Picking;
    }
    private void OnDisable() {
        ObjectPooler.GetAllDiamonds -= Picking;
    }
    public void Picking(){
        base.Picking(UnitManager.Instance.heroUnit.transform);
    }
    protected override void Picked()
    {
        GameManager.Instance.AddExp(diamondValue);
        base.Picked();
    }
}
