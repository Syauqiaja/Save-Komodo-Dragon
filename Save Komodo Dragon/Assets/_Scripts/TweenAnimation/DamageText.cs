using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public ParticleType particleType;
    [HideInInspector] public TextMeshPro textMesh;
    [HideInInspector] public Transform _t;

    private RectTransform rect;
    private Color startColor;
    private void Awake() {
        textMesh = GetComponent<TextMeshPro>();
        startColor = textMesh.color;
        _t = transform;
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable() {
        textMesh.color = startColor;
        LeanTween.value(gameObject, 1f, 4f, 0.2f).setEaseOutSine().setOnUpdate((float value)=>{
            textMesh.fontSize = value;
        }).setOnComplete(()=>{
            gameObject.SetActive(false);
        }).setLoopPingPong(1);
    }
}

[System.Serializable]
public enum ParticleType{
    DamageText,
}
