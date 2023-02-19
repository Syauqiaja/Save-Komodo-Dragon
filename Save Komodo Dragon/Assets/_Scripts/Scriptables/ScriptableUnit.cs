using UnityEngine;

public class ScriptableUnit : ScriptableObject
{
    public Faction unitType;

    [SerializeField] private Stats _stats;
    public Stats BaseStats => _stats;

    // Used in Menu
    public string description;
    public Sprite menuSprite;
}

[System.Serializable]
public enum Faction{
    Hero,
    Enemy,
}

[System.Serializable]
public struct Stats {
    public int health;
    public float travelSpeed;
}
