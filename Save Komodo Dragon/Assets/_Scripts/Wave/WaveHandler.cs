using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public List<ScriptableWave> waveData = new List<ScriptableWave>();
    public GameObject bossPlace;
    private SpawnArea spawnArea;
    private int currentWave = 0;
    private GameObject bossBound = null;

    protected void Awake() {
        spawnArea = GetComponent<SpawnArea>();
        GameManager.OnAfterStateChanged += ChangeWave;
        GameManager.OnBeforeStateChanged += DoBeforeState;
    }
    private void Start(){
        bossPlace = Instantiate(bossPlace);
        bossPlace.SetActive(false);
    }
    private void OnDestroy() {
        GameManager.OnAfterStateChanged -= ChangeWave;
        GameManager.OnBeforeStateChanged -= DoBeforeState;
    }
    private void DoBeforeState(GameState state){
        switch(state){
            case GameState.BossPhase:
                BossPhase phase = waveData[currentWave].bossPhase;
                UnitManager.Instance.currentBoss = phase.scriptableBoss;
            break;
        }
    }
    private void ChangeWave(GameState state){
        switch(state){
            case GameState.Starting :
                currentWave = 0;
                StartCoroutine(StartWave());
                break;
            case GameState.BossPhase:
                StartCoroutine(BossPhase());
                break;
            case GameState.WaveChange:
                currentWave++;
                if(currentWave < waveData.Count){
                    bossBound.SetActive(false);
                    StartCoroutine(StartWave());
                }else{
                    GameManager.Instance.ChangeState(GameState.Win);
                }
                break;
        }
    }
    private IEnumerator StartWave(){
        int currentPhase = 0;
        do{
            Phase phase = waveData[currentWave].phases[currentPhase];
            GameManager.Instance.ChangeState(phase.state);

            yield return new WaitForSeconds(phase.delay);
            float targetTime = Time.time + phase.duration;
            while(targetTime > Time.time){
                foreach (EnemyPair enemy in phase.enemyPair)
                {
                    UnitManager.Instance.TrySpawnEnemy(enemy.enemyData, spawnArea.GetRandomArea(), enemy.countMax);
                }
                yield return new WaitForSeconds(phase.rate);
            }
            currentPhase++;
        }while(currentPhase < waveData[currentWave].phases.Count);

        GameManager.Instance.ChangeState(GameState.BossPhase);
    }
    private IEnumerator BossPhase(){
        yield return new WaitForSeconds(5f); //wait for boss danger text
        BossPhase phase = waveData[currentWave].bossPhase;

        UnitManager.Instance.KillEnemies();
        if(bossBound == null) bossBound = Instantiate(phase.Bound);
        bossBound.transform.position = spawnArea.GetCenter();
        bossBound.gameObject.SetActive(true);
        bossPlace.transform.position = spawnArea.GetCenter();
        bossPlace.SetActive(true);

        yield return new WaitForSeconds(2f);

        UnitManager.Instance.TrySpawnEnemy(phase.scriptableBoss, bossPlace.transform.position);
        bossPlace.SetActive(false);
    }

}
