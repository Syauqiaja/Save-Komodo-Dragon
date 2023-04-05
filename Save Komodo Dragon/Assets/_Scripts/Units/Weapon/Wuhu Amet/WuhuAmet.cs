using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuhuAmet : SkillBase
{
    public ProjectileBase arrowPrefab;
    [SerializeField] Point[] projPosition = new Point[5]; // Have been set manually using GetPosition(i)

    [System.Serializable] struct Point{
        public List<Vector3> Points;
    }
    
    public override void Attack(Vector3 CenterPos, Vector3 direction)
    {
        Vector3 desiredPosition = direction;
        if(heroUnit.enemyFront.Count > 0){
            desiredPosition = heroUnit.enemyFront[Random.Range(0,heroUnit.enemyFront.Count)].position - heroUnit.transform.position;
            desiredPosition.Normalize();
        }

        for (int i = 0; i < weaponStats.size; i++)
        {
            ProjectileBase projectile = ObjectPooler.Instance.GetOrSetProjectile(arrowPrefab);
            projectile.transform.position = transform.position;
            projectile.SetDamage(weaponStats.AttackPower);
            projectile.transform.rotation = 
                Quaternion.FromToRotation(projPosition[((int)weaponStats.size)-1].Points[i], desiredPosition);
            projectile.gameObject.SetActive(true);
            projectile.Launch(projectile.transform.right * weaponStats.speed);
        }
    }
    public override void Upgrade(SkillType skillType, int upgradeTo)
    {
        base.Upgrade(skillType, upgradeTo);
    }

    // // Function to get position of projectile
    // public Vector3 GetPosition(int index){
    //     float angle = Mathf.PI * (index+2) / (weaponStats.size+3);
    //     float x = Mathf.Sin(angle);
    //     float y = Mathf.Cos(angle);
    //     return new Vector3(x,y,0);
    // }
}
