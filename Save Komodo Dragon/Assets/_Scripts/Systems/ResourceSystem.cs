using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// One repository for all scriptable objects. Create your query methods here to keep your business logic clean.
/// I make this a MonoBehaviour as sometimes I add some debug/development references in the editor.
/// If you don't feel free to make this a standard class
/// </summary>
public class ResourceSystem : PersistentSingleton<ResourceSystem> {
    [SerializeField] private List<Sprite> raritySprite;
    public List<ScriptableSkill> Skills { get; private set; }
    public List<ScriptableCloth> Cloths { get; private set; }
    public List<ScriptableHero>  Heroes { get; private set; }
    public List<ScriptableItem> Items {get; private set;}
    private List<ScriptableMap> Maps;
    private List<ScriptableEquipment> Equipments = new List<ScriptableEquipment>();
    private Dictionary<HeroType, ScriptableHero> HerosDict;
    private Dictionary<SkillType, ScriptableSkill> SkillsDict;
    private Dictionary<ClothType, ScriptableCloth> ClothDict;
    private Dictionary<ItemType, ScriptableItem> ItemDict;

    protected override void Awake() {
        base.Awake();
        AssembleResources();
        Application.targetFrameRate = 60;
    }

    private void AssembleResources() {
        Heroes = Resources.LoadAll<ScriptableHero>("Heroes").ToList();
        Heroes.Sort((h1,h2) => ((int)h1.heroType).CompareTo(((int)h2.heroType)));
        HerosDict = Heroes.ToDictionary(r => r.heroType, r => r);

        Skills = Resources.LoadAll<ScriptableSkill>("Skills").ToList();
        SkillsDict = Skills.ToDictionary(r => r.skillType, r => r);

        Cloths = Resources.LoadAll<ScriptableCloth>("Clothes").ToList();
        ClothDict = Cloths.ToDictionary(r => r.clothType, r => r);

        Items = Resources.LoadAll<ScriptableItem>("Items").ToList();
        ItemDict = Items.ToDictionary(r => r.itemType, r => r);

        Maps = Resources.LoadAll<ScriptableMap>("Maps").ToList();

        Equipments.AddRange(Skills);
        Equipments.AddRange(Cloths);
    }

    public ScriptableSkill GetSkill(SkillType t) => SkillsDict[t];
    public ScriptableCloth GetCloth(ClothType t) => ClothDict[t];
    public ScriptableHero GetHero(HeroType t) => HerosDict[t];
    public ScriptableItem GetItem(ItemType t) => ItemDict[t];
    public ScriptableMap GetMap(int index) => Maps.Find(map=> map.mapNumber == index);
    

    public async Task<List<ScriptableEquipment>> GetUpgradableEquipment(){
        List<ScriptableEquipment> _result = new List<ScriptableEquipment>();
        List<ScriptableEquipment> _availableEquipments = new List<ScriptableEquipment>();
        await Task.Run(()=>{
            Equipments.RemoveAll(equipment => GameManager.Instance.GetEquipmentLevel(equipment) >= 4);
            _availableEquipments.AddRange(Equipments);
        });
        for (int i = 0; i < 3; i++)
        {
            if(_availableEquipments.Count == 0) _result.Add(null);
            int index = Random.Range(0, _availableEquipments.Count);
            _result.Add(_availableEquipments[index]);
            _availableEquipments.RemoveAt(index);
        }  

        return _result;
    }
    public ScriptableItem GetRandomItem(){
        return Items[Random.Range(0,Items.Count)];
    }
    public Sprite GetRaritySprite(Rarity rarity){
        return raritySprite[((int)rarity)];
    }
}   