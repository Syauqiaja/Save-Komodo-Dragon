using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cinemachine;

public class UnitManager : StaticInstance<UnitManager>
{
    public DataOverScene dataHolder;
    public CinemachineVirtualCamera virtualCamera;
    [HideInInspector] public Dictionary<EnemyType,List<EnemyUnitBase>> activeEnemyDict = new Dictionary<EnemyType, List<EnemyUnitBase>>();

    [Header("Diamon Spreader")]
    public int startingDiamondCount = 100;
    public float diamondSpawnInterval = 2f;
    public int diamondSpawnCount = 5;
    public float diamondSpreadRadius = 20f;
    [HideInInspector] public HeroUnitBase heroUnit;

    public void SpawnHero(){
        ScriptableHero heroSelected = dataHolder.GetSelectedHero();

        Debug.Log(heroSelected.name);
        heroUnit = Instantiate(heroSelected.Prefab, Vector3.zero, Quaternion.identity);

        //Set properties
        heroUnit.SetStats(heroSelected.BaseStats);
        heroUnit.SetSprite(heroSelected.heroSprites[0]);
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

    public void SpreadDiamond(){
        for(int i = 0; i<startingDiamondCount;i++){
            PickableBase pickableBase = ObjectPooler.Instance.GetPickable(PickableType.smallDiamond);
            pickableBase.gameObject.SetActive(true);
            pickableBase.transform.position = Random.insideUnitCircle * diamondSpreadRadius;
        }
    }
    public void SpawnPickable(PickableType type, Vector3 pos){
        PickableBase pickableBase = ObjectPooler.Instance.GetPickable(type);
        pickableBase.gameObject.SetActive(true);
        pickableBase.transform.position = pos;
    }
    public void SpawnTextDamage(int damage, Vector3 pos){
        DamageText damageText = ObjectPooler.Instance.GetParticle(ParticleType.DamageText);
        damageText.gameObject.SetActive(true);
        damageText.textMesh.text = damage.ToString();
        damageText._t.position = pos;
    }
    public async void KillAllEnemy(){
        await Task.Run(()=>{
            foreach (EnemyType enemyType in activeEnemyDict.Keys)
            { 
                while(activeEnemyDict[enemyType].Count > 0){
                    activeEnemyDict[enemyType][0].Death();
                }
            }
        });
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, diamondSpreadRadius);
    }
}
