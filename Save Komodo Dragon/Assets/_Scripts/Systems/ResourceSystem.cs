using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public class ResourceSystem : StaticInstance<ResourceSystem> {
    public List<ScriptableSkill> Skills { get; private set; }
    private Dictionary<HeroType, ScriptableHero> HerosDict;
    private Dictionary<SkillType, ScriptableSkill> SkillsDict;

    protected override void Awake() {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources() {
        List<ScriptableHero>  Heroes = Resources.LoadAll<ScriptableHero>("Heroes").ToList();
        HerosDict = Heroes.ToDictionary(r => r.HeroType, r => r);

        Skills = Resources.LoadAll<ScriptableSkill>("Skills").ToList();
        SkillsDict = Skills.ToDictionary(r => r.skillType, r => r);
    }

    public ScriptableHero GetHero(HeroType t) => HerosDict[t];
    public ScriptableSkill GetSkill(SkillType t) => SkillsDict[t];
    public void AddHeroSkill(ScriptableSkill skill){
        SkillsDict.Add(SkillType.HeroBaseSkill, skill);
        Skills.Add(skill);
    }
    public ScriptableSkill GetRandomSkill() => Skills[Random.Range(0, Skills.Count)];
}   