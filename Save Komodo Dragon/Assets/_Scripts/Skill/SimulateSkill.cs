using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulateSkill : MonoBehaviour
{
    private SkillBase skillBase;
    private HeroUnitBase heroUnit;
    public Vector3 direction;
    private int currentLevel = 0;

    public bool isSimulating=false;

    private void Awake() {
        skillBase = GetComponent<SkillBase>();
        heroUnit = new HeroUnitBase();
        heroUnit.facingDirection = Vector3.right;
        skillBase.heroUnit = heroUnit;
    }

    private void Start() {
        skillBase.Upgrade(skillBase.skillType, currentLevel);
    }
    public void Attack(){
        skillBase.Attack(transform.position, direction);
    }
    public void Upgrade(){
        currentLevel++;
        skillBase.Upgrade(skillBase.skillType, currentLevel);
    }
}


