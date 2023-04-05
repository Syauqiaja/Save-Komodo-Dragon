using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : StaticInstance<SoundSystem>
{
    [SerializeField] AudioSource audioSource;
    public void Mute(bool value){
        audioSource.mute = value;
    }
}
