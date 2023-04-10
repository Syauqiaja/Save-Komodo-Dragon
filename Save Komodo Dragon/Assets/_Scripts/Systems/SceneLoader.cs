using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] private Image loadingImage;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private PanelTween panel;

    [Header("Loading Content")]
    [SerializeField] private List<LoadingContent> loadingContents = new List<LoadingContent>();

    public void LoadLevel(ScriptableMap map){
        loadingImage.sprite = map.mapImage;
        loadingText.text = "Moving to \n"+map.mapName;
        StartCoroutine(OpenLoading("Chapter_"+map.mapNumber.ToString()));
    }
    public void LoadScene(LoadingContentType type){
        LoadingContent content = loadingContents.Find(it => it.type == type);
        loadingImage.sprite = content.image;
        loadingText.text = content.text;
        StartCoroutine(OpenLoading(content.sceneName));
    }
    IEnumerator OpenLoading(string name){
        Time.timeScale = 0;
        panel.gameObject.SetActive(true);
        panel.Show();
        float _timer=1;
        AsyncOperation sceneLoaded = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        sceneLoaded.allowSceneActivation = false;
        while(sceneLoaded.progress < 0.89 || _timer > 0f){
            _timer -= Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1f;
        sceneLoaded.allowSceneActivation = true;
        panel.Hide();
        yield return null;
    }
}

[System.Serializable]
public struct LoadingContent{
    public string sceneName;
    public Sprite image;
    public string text;
    public LoadingContentType type;
}

public enum LoadingContentType{
    MainMenu,
    LoseMainMenu,
    WinMainMenu,
}