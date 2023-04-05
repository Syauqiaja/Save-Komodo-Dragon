using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapSelector : MonoBehaviour{
    [SerializeField] DataOverScene dataHolder;
    [SerializeField] Transform mapContainer;

    [Header("Selected Map UI")]
    [SerializeField] Image mapSelectedImage;
    [SerializeField] TextMeshProUGUI bestSurviveTime;
    [SerializeField] TextMeshProUGUI mapSelectedText;
    private MapButton[] mapButtons;
    private ScriptableMap selectedMap;

    private void Awake() {
        DataPresistenceManager.OnAfterLoad += InitMap;
    }
    private void Start() {
        InitMap();
    }
    private void OnDestroy() {
        DataPresistenceManager.OnAfterLoad -= InitMap;
    }

    void InitMap(){
        if(!dataHolder.IsLoaded) return;
        mapButtons = mapContainer.GetComponentsInChildren<MapButton>(true);
        for (int i = 0; i < mapButtons.Length; i++)
        {
            ScriptableMap scriptableMap = ResourceSystem.Instance.GetMap(i);
            mapButtons[i].mapSelector = this;
            mapButtons[i].mapNumber = scriptableMap.mapNumber;
            mapButtons[i].mapImage.sprite = scriptableMap.mapImage;

            if(i < dataHolder.MapUnclokedBundles.Count){
                mapButtons[i].mapText.text = scriptableMap.mapName;
                mapButtons[i].mapImage.color = Color.white;
                // mapButtons[i].blackPanel.SetActive(false);
                mapButtons[i].button.interactable = true;
                mapButtons[i].mapFrame.color = scriptableMap.frameColor;
            }else{
                mapButtons[i].mapText.text = "???";
                mapButtons[i].mapImage.color = Color.grey;
                // mapButtons[i].blackPanel.SetActive(true);
                mapButtons[i].button.interactable = false;
                mapButtons[i].mapFrame.color = Color.clear;
            }
            if(i == dataHolder.SelectedMap) selectedMap = scriptableMap;
        }
        SetSelectedMap();
    }

    public void SelectMap(int mapNumber){ 
        dataHolder.SelectedMap = mapNumber;
        selectedMap = ResourceSystem.Instance.GetMap(mapNumber);
        SetSelectedMap();
    }
    void SetSelectedMap(){
        mapSelectedImage.sprite = selectedMap.mapImage;
        mapSelectedText.text = selectedMap.mapName;
        Debug.Log("Selected map : "+selectedMap.mapNumber);
        int bestTime = dataHolder.MapUnclokedBundles[selectedMap.mapNumber].bestSurviveTime;
        bestSurviveTime.text = "Longest survive : "+bestTime/60+"m "+bestTime%60+"s";
    }
}