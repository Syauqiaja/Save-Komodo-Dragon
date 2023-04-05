using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackingZoom : MonoBehaviour
{
    public float startScale = 0.1f;
    public float targetScale = 1f;
    public float leanTime = 0.5f;
    public float delay = 0f;
    private void OnEnable() {
        transform.localScale = Vector3.one * startScale;
        LeanTween.scale(gameObject, Vector2.one * targetScale, leanTime).setEaseOutBack().setDelay(delay);
    }
}
