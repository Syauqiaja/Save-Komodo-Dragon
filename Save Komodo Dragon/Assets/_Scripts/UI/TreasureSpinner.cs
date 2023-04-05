using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureSpinner : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private TreasureCard[] results = new TreasureCard[3];
    // [SerializeField] private Button spinButton;
    [SerializeField] private Button nextButton;
    const int SPIN_COUNT = 32;
    private GameObject[] frame = new GameObject[16];
    private Image[] itemImages = new Image[16];
    private ScriptableEquipment selected;
    private int selectedIndex;

    [HideInInspector] public bool isEnd = false;

    private void Awake() {
        for (int i = 0; i < 16; i++)
        {
            frame[i] = itemsParent.GetChild(i).GetChild(1).gameObject;
            itemImages[i] = itemsParent.GetChild(i).GetChild(0).GetComponent<Image>();
        }
    }
    private void OnEnable() {
        // spinButton.interactable = true;
        nextButton.interactable = false;
        mainPanel.SetActive(true);
        resultPanel.SetActive(false);
        FillItemsAndReturn();
    }
    private void FillItemsAndReturn(){
        List<ScriptableEquipment> equipmentList = GameManager.Instance.GetEquipmentHeldList();
        int count = equipmentList.Count;

        selectedIndex = Random.Range(0,16);
        for (int i = 0; i < 16; i++)
        {
            ScriptableEquipment equipment = equipmentList[i%count];
            itemImages[i].sprite = equipment.menuImage;
            frame[i].SetActive(false);
            if(i == selectedIndex) selected = equipment;
        }
        StartCoroutine(Spinning(selectedIndex + SPIN_COUNT+1));
    }
    IEnumerator Spinning(int stopAt){
        isEnd = false;
        float timeDelay = 0.025f;
        for (int i = 0; i < stopAt-1; i++)
        {
            frame[i%16].SetActive(true);
            yield return new WaitForSecondsRealtime(timeDelay);
            frame[i%16].SetActive(false);
            timeDelay = Mathf.Min(0.4f, timeDelay*1.05f);
            if(isEnd) break;
        }
        frame[(stopAt-1)%16].SetActive(true);
        nextButton.interactable = true;
        isEnd = true;
    }
    public void ShowResult(){
        mainPanel.SetActive(false);
        int lvl = GameManager.Instance.GetEquipmentLevel(selected)+1;
        results[0].SetTreasureCard(selected.menuImage, selected.menuName, selected.menuDesc[lvl], lvl);
        resultPanel.SetActive(true);
    }
    public void ConfirmSelected(){
        resultPanel.SetActive(false);
        mainPanel.SetActive(true);
        if(selected.equipmentType == EquipmentType.Skill)
            GameManager.Instance.UpgradeEquipment(((ScriptableSkill) selected).skillType);
        else if(selected.equipmentType == EquipmentType.Cloth){
            ScriptableCloth cloth = (ScriptableCloth) selected;
            int lvl = GameManager.Instance.GetEquipmentLevel(cloth)+1;
            ClothHandler.Instance.WearCloth(cloth.clothType, cloth.upgradeValues[lvl]);
            GameManager.Instance.UpgradeEquipment(((ScriptableCloth) selected).clothType);
        }
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
