using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableWave : ScriptableObject
{
    public List<Phase> phases;
    public BossPhase bossPhase;

    public Phase GetPhase(int phaseIndex){
        if(phaseIndex < phases.Count) return phases[phaseIndex];
        else return new Phase();
    }
}
[System.Serializable]
public struct EnemyPair{
    public ScriptableEnemy enemyData;
    public int countMax;
    public bool spawnOnce;
}
[System.Serializable]
public struct Phase{
    public float delay;
    public int duration;
    public float rate;
    public GameState state;
    public List<EnemyPair> enemyPair;
}

[System.Serializable]
public struct BossPhase{
    public ScriptableBoss bossType;
    public GameObject Bound;
}
