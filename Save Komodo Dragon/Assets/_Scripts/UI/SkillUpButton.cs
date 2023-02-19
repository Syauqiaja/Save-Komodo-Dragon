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
    private ScriptableSkill selectedSkill;

    private void Awake() {
        button.onClick.AddListener(OnClick);
    }
    void OnClick(){
        GameManager.Instance.AddSkill(selectedSkill.skillType);
    }
    public void SetUpButton(SkillType skillType){
        selectedSkill = ResourceSystem.Instance.GetSkill(skillType);
        skillImage.sprite = selectedSkill.menuImage;
        skillName.text = selectedSkill.skillName;
        skillDesc.text = selectedSkill.skillDesc;
    }
    public void SetUpButton(){
        selectedSkill = ResourceSystem.Instance.GetRandomSkill();
        skillImage.sprite = selectedSkill.menuImage;
        skillName.text = selectedSkill.skillName;
        skillDesc.text = selectedSkill.skillDesc;
    }
}
