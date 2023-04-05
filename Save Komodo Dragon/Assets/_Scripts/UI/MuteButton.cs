using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] Sprite onMuteSpirte;
    [SerializeField] Sprite offMuteSprite;

    [SerializeField] Image _image;

    private void OnEnable() {
        CheckMute();
    }

    private void CheckMute(){
        if(PlayerPrefs.GetInt("isMute") == 1){ // -1 = off, 1 = on
            _image.sprite = onMuteSpirte;
            SoundSystem.Instance.Mute(true);
        }else
        {
            _image.sprite = offMuteSprite;
            SoundSystem.Instance.Mute(false);
        }
    }

    public void Mute(){
        PlayerPrefs.SetInt("isMute", PlayerPrefs.GetInt("isMute", 1) * -1);
        PlayerPrefs.Save();
        CheckMute();
    }
}
