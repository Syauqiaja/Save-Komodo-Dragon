using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class KerisBali : SkillBase
{
    const float RADIUS = 2f;
    [SerializeField] private P_KerisBali kerisProjectile;
    [SerializeField] private List<P_KerisBali> _projectiles;

    private bool isAnyUpgrade = false;
    int upgradeTo = 0;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, RADIUS);
    }
    public override void Awake()
    {
        GameManager.SkillUpgraded += Upgrade;
    }
    private void Start() {
        foreach (P_KerisBali item in _projectiles)
        {
            item.SetDamage(weaponStats.AttackPower);
            item.duration = weaponStats.duration;
            item.speed = weaponStats.speed;
        }
        DrawCirclePoints(RADIUS);
        StartCoroutine(AsyncAttack(Vector3.zero,Vector3.zero));
    }
    public override void OnDestroy() {
        GameManager.SkillUpgraded -= Upgrade;
    }
    public IEnumerator AsyncAttack(Vector3 CenterPos, Vector3 direction)
    {
        while (true)
        {
            if(isAnyUpgrade) DoUpgrade();
            for (int i = 0; i < _projectiles.Count; i++)
            {
                _projectiles[i].gameObject.SetActive(true);
                _projectiles[i].Launch(i, _projectiles.Count);
            }
            yield return new WaitForSeconds(weaponStats.duration + 1f);
        }
    }
    void DoUpgrade(){
        foreach (P_KerisBali item in _projectiles)
        {
            item.Stop();
        }
        SetStats(ResourceSystem.Instance.GetSkill(this.skillType).weaponStats[upgradeTo]);

        if(weaponStats.size > _projectiles.Count){
            _projectiles.Add(Instantiate(kerisProjectile,transform));
        }

        DrawCirclePoints(RADIUS);

        foreach (P_KerisBali item in _projectiles)
        {
            item.SetDamage(weaponStats.AttackPower);
            item.duration = weaponStats.duration;
            item.speed = weaponStats.speed;
        }
        isAnyUpgrade = false;
    }

    override public void Upgrade(SkillType skillType, int upgradeTo){
        if(skillType != this.skillType) return;
        isAnyUpgrade = true;
        this.upgradeTo = upgradeTo;
    }

    void DrawCirclePoints(double radius)
    {   
        float points = _projectiles.Count;
        float slice = 2f * Mathf.PI / points;
        for (int i = 0; i < points; i++)
        {
            float angle = slice * i;
            float newX = (float)(radius * Mathf.Cos(angle));
            float newY = (float)(radius * Mathf.Sin(angle));
            Vector2 p = new Vector2(newX, newY);
            _projectiles[i].targetPosition = p;
            // _projectiles[i].transform.localPosition = p;
            _projectiles[i].transform.localRotation = Quaternion.Euler(0, 0,(360f / points)*i - 90f);
        }
    }
}
