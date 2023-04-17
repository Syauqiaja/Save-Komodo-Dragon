using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UITween : MonoBehaviour
{
    public float leanTime = 0.5f;
    public float delay{get{
        return _delay+_d2;
    }set{
        _delay = value;
    }}
    [SerializeField] private float _delay = 0f;
    private float _d2 = 0f;
    public LeanTweenType easeType;

    public virtual void Show(){}
    public void Show(float delay){
        _d2 = delay;
        Show();
    }
    public virtual void Show(Action action){}
    public virtual void Hide(){}
    public virtual void Hide(float delay){
        _d2 = delay;
        Hide();
    }
    public virtual void Hide(Action action){}
}
