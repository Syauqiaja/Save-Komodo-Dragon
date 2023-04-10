using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouncingZoom : UITween
{
    public float startScale = 0.1f;
    public float targetScale = 1f;
    private void OnEnable() {
        transform.localScale = Vector3.one * startScale;
        LeanTween.scale(gameObject, Vector2.one * targetScale, leanTime).setEaseOutBounce().setDelay(delay);
    }
}
