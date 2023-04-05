using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : StaticInstance<SceneLoader>
{
    public void LoadScene(int index){
        SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(index).name, LoadSceneMode.Single);
    }
    public void LoadScene(string name){
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}
