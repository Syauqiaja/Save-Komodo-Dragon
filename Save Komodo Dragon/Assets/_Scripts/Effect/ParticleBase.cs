using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    public ParticleSystem particle;
    public ParticleType particleType;
}

[System.Serializable]
public enum ParticleType{
    DamageText,
    StoneExplode,
}