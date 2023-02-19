using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public List<ScriptableWave> waveData = new List<ScriptableWave>();
    public GameObject bossPlace;
    private SpawnArea spawnArea;
    private int currentWave = 1;
    private GameObject bossBound;

    private void Awake() {
        spawnArea = GetComponent<SpawnArea>();
    }
    private void Start(){
        GameManager.OnAfterStateChanged += ChangeWave;
        bossPlace = Instantiate(bossPlace);
        bossPlace.SetActive(false);
    }
    private void ChangeWave(GameState state){
        switch(state){
            case GameState.SmallPhase :
                StartCoroutine(SmallPhase());
                break;
            case GameState.MassPhase :
                StartCoroutine(MassPhase());
                break;
            case GameState.ElitePhase:
                StartCoroutine(ElitePhase());
                break;
            case GameState.BossPhase:
                StartCoroutine(BossPhase());
                break;
        }
    }
    private IEnumerator SmallPhase(){
        Phase phase = waveData[currentWave-1].smallPhase;
        float timeNow = Time.time;
        while(timeNow + phase.duration > Time.time){
            foreach (EnemyPair enemy in phase.enemyPair)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    UnitManager.Instance.SpawnEnemy(enemy.enemyType, spawnArea.GetRandomArea());
                }
                yield return null;
            }
            yield return new WaitForSeconds(phase.period);
        }
        GameManager.Instance.ChangeState(GameState.MassPhase);
    }
    private IEnumerator MassPhase(){
        Phase phase = waveData[currentWave-1].massPhase;
        float timeNow = Time.time;
        while(timeNow + phase.duration > Time.time){
            foreach (EnemyPair enemy in phase.enemyPair)
            {
                for (int i = 0; i < enemy.count; i++)
                {
                    UnitManager.Instance.SpawnEnemy(enemy.enemyType, spawnArea.GetRandomArea());
                }
                yield return null;
            }
            yield return new WaitForSeconds(phase.period);
        }
        GameManager.Instance.ChangeState(GameState.ElitePhase);
    }
    private IEnumerator ElitePhase(){
        Phase phase = waveData[currentWave-1].elitePhase;
        float timeNow = Time.time;
        UnitManager.Instance.SpawnEnemy(phase.enemyPair[0].enemyType, spawnArea.GetRandomArea());
        while(timeNow + phase.duration > Time.time){
            foreach (EnemyPair enemy in phase.enemyPair)
            {
                if(enemy.enemyType.enemyType == EnemyType.Elite) continue;
                for (int i = 0; i < enemy.count; i++)
                {
                    UnitManager.Instance.SpawnEnemy(enemy.enemyType, spawnArea.GetRandomArea());
                }
                yield return null;
            }
            yield return new WaitForSeconds(phase.period);
        }
        GameManager.Instance.ChangeState(GameState.BossPhase);
    }
    private IEnumerator BossPhase(){
        yield return new WaitForSeconds(2.5f);
        UnitManager.Instance.KillAllEnemy();
        BossPhase phase = waveData[currentWave-1].bossPhase;
        bossBound = Instantiate(phase.Bound, spawnArea.GetCenter(), Quaternion.identity);
        bossPlace.transform.position = spawnArea.GetCenter();
        bossPlace.SetActive(true);
        yield return new WaitForSeconds(2f);
        EnemyUnitBase go = Instantiate(phase.bossType.prefab, bossPlace.transform.position, Quaternion.identity);
        go.SetStats(phase.bossType.BaseStats);
        go.SetProperties(phase.bossType.inGameSprite, phase.bossType.hitDamage, phase.bossType.pickableType);
        bossPlace.SetActive(false);
    }
}
