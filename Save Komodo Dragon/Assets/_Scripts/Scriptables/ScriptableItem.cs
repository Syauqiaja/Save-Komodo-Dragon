using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Hero" ,fileName ="Heroes")]
public class ScriptableItem : ScriptableObject
{
    public ItemType ItemType;
    public List<string> levelingDescription = new List<string>();
    public string menuDescription;
    public Sprite menuSprite;
}

[System.Serializable]
public enum ItemType{
    Weapon = 0,
    Skill = 1,
    Artifact = 2,
}
