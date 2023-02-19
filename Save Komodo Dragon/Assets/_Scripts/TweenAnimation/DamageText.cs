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
        LeanTween.moveY(gameObject, _t.position.y + 0.1f, 0.3f).setEaseInSine().setOnComplete(()=>{
            gameObject.SetActive(false);
        });
    }
}

[System.Serializable]
public enum ParticleType{
    DamageText,
}
