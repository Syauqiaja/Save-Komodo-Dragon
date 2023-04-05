using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map" ,fileName ="NewMap")]
public class ScriptableMap : ScriptableObject
{
    public List<ScriptableWave> waves;
    public Sprite mapImage;
    public Color frameColor;
    public string mapName;
    public int mapNumber;
}
