using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LandingPage : MonoBehaviour
{
    [SerializeField] private RectTransform button;
    [SerializeField] private CanvasGroup blackPanel;
    AsyncOperation asyncOperation;
    private void Start() {
        LeanTween.color(button, new Vector4(0.75f,0.75f,0.75f,1), 1f)
            .setLoopPingPong()
            .setEaseInOutSine();
        LeanTween.alphaCanvas(blackPanel, 0f, 1f).setOnComplete(()=>{
            blackPanel.gameObject.SetActive(false);
        });
        asyncOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        asyncOperation.allowSceneActivation = false;
    }

    public void GoToMainMenu(){
        asyncOperation.allowSceneActivation = true;
    }
}
