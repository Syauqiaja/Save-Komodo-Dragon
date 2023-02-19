using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Enemy",fileName ="Enemy")]
public class ScriptableEnemy : ScriptableUnit
{
    public EnemyType enemyType;
    public PickableType pickableType;
    public Sprite inGameSprite;
    public int hitDamage;
}

[System.Serializable]
public enum EnemyType{
    Normal,
    Shooter,
    Elite,
    Special,
    Boss
}
