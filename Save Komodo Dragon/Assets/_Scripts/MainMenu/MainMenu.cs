using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject ShopPart, BattlePart, SetupPart;
    [SerializeField] GameObject QuitConfirmation;
    [SerializeField] DataOverScene dataHolder;
    
    private void Start() {
        OpenBattle();
    }

// Navigation Part
    public void OpenShop(){
        ShopPart.SetActive(true);
        BattlePart.SetActive(false);
        SetupPart.SetActive(false);
    }
    public void OpenBattle(){
        ShopPart.SetActive(false);
        BattlePart.SetActive(true);
        SetupPart.SetActive(false);
    }
    public void OpenSetup(){
        ShopPart.SetActive(false);
        BattlePart.SetActive(false);
        SetupPart.SetActive(true);
    }
    public void OpenQuit(){
        QuitConfirmation.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
    public void StartGame(){
        string mapSelected = "Chapter_"+dataHolder.SelectedMap.ToString();
        SceneLoader.Instance.LoadScene(mapSelected);
    }
}
