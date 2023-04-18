using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

public class UnitManager : StaticInstance<UnitManager>
{
    public static event Action KillAllEnemies;
    public DataOverScene dataHolder;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
    private float _timer=0;
    [HideInInspector] public Dictionary<EnemyType,List<EnemyUnitBase>> activeEnemyDict = new Dictionary<EnemyType, List<EnemyUnitBase>>();

    [Header("Diamon Spreader")]
    public int startingDiamondCount = 100;
    public float diamondSpawnInterval = 2f;
    public int diamondSpawnCount = 5;
    public float diamondSpreadRadius = 20f;
    [HideInInspector] public HeroUnitBase heroUnit;

    public void SpawnHero(){
        basicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ScriptableHero heroSelected = ResourceSystem.Instance.GetHero(dataHolder.GetSelectedHero().heroType);

        Debug.Log(heroSelected.name);
        heroUnit = Instantiate(heroSelected.Prefab, Vector3.zero, Quaternion.identity);

        //Set properties
        heroUnit.SetStats(heroSelected.BaseStats);
        heroUnit.SetSprite(heroSelected.GetHeroSpriteAtLevel(1));
        virtualCamera.Follow = heroUnit.transform;

        GameManager.Instance.AddEquipment(dataHolder.SelectedSkill);
    }
    public void AddNewSkill(SkillType skillType){
        ScriptableSkill scriptableSkill = ResourceSystem.Instance.GetSkill(skillType);
        SkillBase skillBase = Instantiate(scriptableSkill.prefab, heroUnit.transform.position, Quaternion.identity);
        skillBase.SetStats(scriptableSkill.weaponStats[0]);
        skillBase.heroUnit = heroUnit;
        skillBase.transform.SetParent(heroUnit.transform);
    }
    public void TrySpawnEnemy(ScriptableEnemy scriptableEnemy, Vector3 position, int maxCount){
        if(!activeEnemyDict.ContainsKey(scriptableEnemy.enemyType))
            activeEnemyDict.Add(scriptableEnemy.enemyType, new List<EnemyUnitBase>());

        if(activeEnemyDict[scriptableEnemy.enemyType].Count >= maxCount) return;

        EnemyUnitBase enemyUnit = ObjectPooler.Instance.GetEnemy(scriptableEnemy.enemyType);
        activeEnemyDict[scriptableEnemy.enemyType].Add(enemyUnit);
        enemyUnit.gameObject.SetActive(true);
        enemyUnit.SetStats(scriptableEnemy.BaseStats);
        enemyUnit.SetProperties(scriptableEnemy.inGameSprite, scriptableEnemy.hitDamage, scriptableEnemy.pickableType);
        enemyUnit.transform.position = position;
    }
    public void TrySpawnEnemy(EnemyUnitBase enemyUnitBase, Vector3 position){
        EnemyUnitBase enemyUnit = Instantiate(enemyUnitBase, position, Quaternion.identity);
        if(!activeEnemyDict.ContainsKey(enemyUnitBase.enemyType))
            activeEnemyDict.Add(enemyUnitBase.enemyType, new List<EnemyUnitBase>());
        activeEnemyDict[enemyUnit.enemyType].Add(enemyUnit);  
    }

    public void SpreadDiamond(){
        for(int i = 0; i<startingDiamondCount;i++){
            PickableBase pickableBase = ObjectPooler.Instance.GetPickable(PickableType.smallDiamond);
            pickableBase.gameObject.SetActive(true);
            pickableBase.transform.position = UnityEngine.Random.insideUnitCircle * diamondSpreadRadius;
        }
    }
    public void SpawnPickable(PickableType type, Vector3 pos){
        PickableBase pickableBase = ObjectPooler.Instance.GetPickable(type);
        pickableBase.gameObject.SetActive(true);
        pickableBase.transform.position = pos;
    }
    public void SpawnTextDamage(int damage, Vector3 pos){
        DamageText damageText = ObjectPooler.Instance.GetDamageText();
        damageText.gameObject.SetActive(true);
        damageText.textMesh.text = damage.ToString();
        damageText._t.position = pos;
    }
    public void KillEnemies(){
        KillAllEnemies?.Invoke();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, diamondSpreadRadius);
    }

    // OOT
    public void StartShake(float intensity, float time){
        basicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _timer = time;
    }
    public void Shake(float intensity, float time){
        LeanTween.value(gameObject, intensity, 0f, time).setOnUpdate((float value)=>{
            basicMultiChannelPerlin.m_AmplitudeGain = value;
        });
    }
    public void Shake(float intensity, float time, float timemargin){
        LeanTween.value(gameObject, 0f, intensity, timemargin).setOnUpdate((float value)=>{
            basicMultiChannelPerlin.m_AmplitudeGain = value;
        });
        LeanTween.value(gameObject, intensity, 0f, timemargin).setOnUpdate((float value)=>{
            basicMultiChannelPerlin.m_AmplitudeGain = value;
        }).setDelay(time+timemargin);
    }
    public void EndShake(){
        LeanTween.value(gameObject, basicMultiChannelPerlin.m_AmplitudeGain, 0f, _timer).setOnUpdate((float value)=>{
            basicMultiChannelPerlin.m_AmplitudeGain = value;
        });
    }
}
