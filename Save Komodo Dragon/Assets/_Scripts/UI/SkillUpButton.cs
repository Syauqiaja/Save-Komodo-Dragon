using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillUpButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillDesc;
    [SerializeField] private TextMeshProUGUI skillLvl;

    [Header("Crystal Type")]
    [SerializeField] private GameObject typeRed;
    [SerializeField] private GameObject typeBlue;
    private ScriptableEquipment selectedEquipment;
    int lvl;

    private void Awake() {
        button.onClick.AddListener(OnClick);
    }
    void OnClick(){
        if(selectedEquipment.equipmentType == EquipmentType.Skill)
            GameManager.Instance.AddEquipment(((ScriptableSkill) selectedEquipment).skillType);
        else if(selectedEquipment.equipmentType == EquipmentType.Cloth){
            ScriptableCloth cloth = (ScriptableCloth) selectedEquipment;
            ClothHandler.Instance.WearCloth(cloth.clothType, cloth.upgradeValues[lvl+1]);
            GameManager.Instance.AddEquipment(((ScriptableCloth) selectedEquipment).clothType);
        }
    }
    public void SetUpButton(ScriptableEquipment equipment){
        selectedEquipment = equipment;
        lvl=GameManager.Instance.GetEquipmentLevel(selectedEquipment);

        skillImage.sprite = selectedEquipment.menuImage;
        skillName.text = selectedEquipment.menuName;
        skillLvl.text = lvl >= 0 ? "Lv "+(lvl+1).ToString()+" / 5" : "New";
        skillDesc.text = selectedEquipment.menuDesc[lvl+1];
        if(equipment.equipmentType == EquipmentType.Skill){
            typeBlue.SetActive(false);
            typeRed.SetActive(true);
        }else{
            typeBlue.SetActive(true);
            typeRed.SetActive(false);
        }
    }
}
