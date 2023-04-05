using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : MonoBehaviour
{
    [SerializeField] Sprite onVibrationSprite;
    [SerializeField] Sprite offVibartionSprite;
    [SerializeField] Image _image;

    private void OnEnable() {
        CheckVib();
    }

    private void CheckVib(){
        if(PlayerPrefs.GetInt("vibration") == -1){ // -1 = off, 1 = on
            _image.sprite = offVibartionSprite;
        }else
        {
            _image.sprite = onVibrationSprite;
        }
    }

    public void Vibrate(){
        PlayerPrefs.SetInt("vibration", PlayerPrefs.GetInt("vibration", 1) * -1);
        PlayerPrefs.Save();
        CheckVib();
    }
}
