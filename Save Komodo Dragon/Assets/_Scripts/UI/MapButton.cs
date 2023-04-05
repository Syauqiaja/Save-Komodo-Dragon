using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI mapText;

    public Button button;
    public Image mapImage;
    public Image mapFrame;
    [HideInInspector] public bool isUnlocked;
    [HideInInspector] public string mapName;
    [HideInInspector] public int mapNumber;
    [HideInInspector] public MapSelector mapSelector;
    
    private void Awake() {
        button.onClick.AddListener(()=>{
            mapSelector.SelectMap(mapNumber);
        });
    }
}
