using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : PersistentSingleton<SoundSystem>
{
    [SerializeField] AudioSource audioSource;
    public void Mute(bool value){
        audioSource.mute = value;
    }
}
