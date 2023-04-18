using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject ShopPart, BattlePart, SetupPart;
    [SerializeField] ItemSetup setupPart;
    [SerializeField] GameObject QuitConfirmation;
    [SerializeField] DataOverScene dataHolder;
    
    private void Start() {
        OpenBattle();
    }

// Navigation Part
    public void OpenShop(){
        ShopPart.SetActive(true);
        BattlePart.SetActive(false);
        setupPart.CloseItemSetup();
    }
    public void OpenBattle(){
        ShopPart.SetActive(false);
        BattlePart.SetActive(true);
        setupPart.CloseItemSetup();
    }
    public void OpenSetup(){
        setupPart.OpenItemSetup(()=>{
            ShopPart.SetActive(false);
            BattlePart.SetActive(false);
        });
    }
    public void OpenQuit(){
        QuitConfirmation.SetActive(true);
    }
    public void Quit(){
        Application.Quit();
    }
    public void StartGame(){
        dataHolder.Energy -= 5;
        SceneLoader.Instance.LoadLevel(ResourceSystem.Instance.GetMap(dataHolder.SelectedMap));
    }
}
