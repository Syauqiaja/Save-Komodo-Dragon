using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableWave : ScriptableObject
{
    public Phase smallPhase, massPhase, elitePhase;
    public BossPhase bossPhase;
}
[System.Serializable]
public struct EnemyPair{
    public ScriptableEnemy enemyType;
    public int count;
}
[System.Serializable]
public struct Phase{
    public int duration;
    public int period;
    public List<EnemyPair> enemyPair;
}

[System.Serializable]
public struct BossPhase{
    public ScriptableBoss bossType;
    public GameObject Bound;
}
