using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPooler : StaticInstance<ObjectPooler>
{
    public static event Action GetAllDiamonds;

    [Header("Object To Pool")]
    public List<ObjectToPool> objectToPools = new List<ObjectToPool>();

    private List<ProjectileBase> projectileBases = new List<ProjectileBase>();
    private List<EnemyUnitBase> enemyUnitBases = new List<EnemyUnitBase>();
    private List<PickableBase> pickableBases = new List<PickableBase>();
    private List<ParticleBase> particleBases = new List<ParticleBase>();
    private List<DamageText> damageTexts = new List<DamageText>();
    protected override void Awake()
    {
        base.Awake();
        InitiateObject();
    }
    private void InitiateObject(){
        foreach (ObjectToPool pooledObject in objectToPools)
        {
            for (int i = 0; i < pooledObject.count; i++)
            {
                GameObject go = Instantiate(pooledObject.prefab);
                go.SetActive(false);
                if(pooledObject.poolType == PoolType.Projectile) projectileBases.Add(go.GetComponent<ProjectileBase>());
                else if (pooledObject.poolType == PoolType.Enemy) enemyUnitBases.Add(go.GetComponent<EnemyUnitBase>());
                else if(pooledObject.poolType == PoolType.Pickable) pickableBases.Add(go.GetComponent<PickableBase>());
                else if(pooledObject.poolType == PoolType.Particle) particleBases.Add(go.GetComponent<ParticleBase>());
                else if(pooledObject.poolType == PoolType.damageText) damageTexts.Add(go.GetComponent<DamageText>());
            }
        }
    }
    public ProjectileBase GetProjectile(SkillType type){
        foreach (ProjectileBase projectile in projectileBases)
        {
            if(projectile.skillType == type && !projectile.gameObject.activeInHierarchy){
                return projectile;
            }
        }
        foreach (ObjectToPool item in objectToPools)
        {
            if(item.poolType == PoolType.Projectile && item.prefab.GetComponent<ProjectileBase>().skillType == type){
                GameObject go = Instantiate(item.prefab);
                go.SetActive(false);
                ProjectileBase pb = go.GetComponent<ProjectileBase>();
                projectileBases.Add(pb);
                return pb;
            }
        }
        return null;
    }
    public ProjectileBase GetOrSetProjectile(ProjectileBase projectileBase){
        foreach (ProjectileBase projectile in projectileBases)
        {
            if(projectile.skillType == projectileBase.skillType && !projectile.gameObject.activeInHierarchy){
                return projectile;
            }
        }
        
        ProjectileBase go = Instantiate(projectileBase);
        go.gameObject.SetActive(false);
        projectileBases.Add(go);
        Debug.Log("Get or set returning : "+go.name);
        return go;
    }
    public EnemyUnitBase GetEnemy(EnemyType type){
        foreach (EnemyUnitBase enemy in enemyUnitBases)
        {
            if(enemy.enemyType == type && !enemy.gameObject.activeInHierarchy){
                return enemy;
            }
        }
        foreach (ObjectToPool item in objectToPools)
        {
            if(item.poolType == PoolType.Enemy && item.prefab.GetComponent<EnemyUnitBase>().enemyType == type){
                GameObject go = Instantiate(item.prefab);
                go.SetActive(false);
                EnemyUnitBase pb = go.GetComponent<EnemyUnitBase>();
                enemyUnitBases.Add(pb);
                return pb;
            }
        }
        return null;
    }
    public PickableBase GetPickable(PickableType type){
        foreach (PickableBase pickable in pickableBases)
        {
            if(pickable.pickableType == type && !pickable.gameObject.activeInHierarchy){
                return pickable;
            }
        }
        foreach (ObjectToPool item in objectToPools)
        {
            if(item.poolType == PoolType.Pickable && item.prefab.GetComponent<PickableBase>().pickableType == type){
                GameObject go = Instantiate(item.prefab);
                go.SetActive(false);
                PickableBase pb = go.GetComponent<PickableBase>();
                pickableBases.Add(pb);
                return pb;
            }
        }
        return null;
    }
    public void CollectAllDiamonds(){
        GetAllDiamonds?.Invoke();
    }

    public ParticleBase GetParticle(ParticleType type){
        foreach (ParticleBase particle in particleBases)
        {
            if(particle.particleType == type && !particle.gameObject.activeInHierarchy){
                return particle;
            }
        }
        foreach (ObjectToPool item in objectToPools)
        {
            if(item.poolType == PoolType.Particle && item.prefab.GetComponent<ParticleBase>().particleType == type){
                GameObject go = Instantiate(item.prefab);
                go.SetActive(false);
                ParticleBase pb = go.GetComponent<ParticleBase>();
                particleBases.Add(pb);
                return pb;
            }
        }
        return null;
    }
    public DamageText GetDamageText(){
        foreach (DamageText damageText in damageTexts)
        {
            if(!damageText.gameObject.activeInHierarchy){
                return damageText;
            }
        }
        foreach (ObjectToPool item in objectToPools)
        {
            if(item.poolType == PoolType.damageText){
                GameObject go = Instantiate(item.prefab);
                go.SetActive(false);
                DamageText pb = go.GetComponent<DamageText>();
                particleBases.Add(pb);
                return pb;
            }
        }
        return null;
    }
}

[System.Serializable]
public struct ObjectToPool{
    public GameObject prefab;
    public int count;
    public PoolType poolType;
}

[System.Serializable]
public enum PoolType{
    Projectile = 0,
    Enemy = 1,
    Pickable = 2,
    Particle = 3,
    damageText=4,
}
