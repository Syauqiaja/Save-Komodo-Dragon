using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_KerisBali : ProjectileBase
{
    [HideInInspector] public float duration;
    [HideInInspector] public float speed;
    [HideInInspector] public Vector3 targetPosition;
    private IEnumerator launchedTask;
    public void Launch(int index, int count)
    {
        launchedTask = AsyncRotateAround(index, count, duration);
        LeanTween.moveLocal(gameObject, targetPosition, 0.2f).setOnComplete(()=>{
            StartCoroutine(launchedTask);
        }).setEaseOutSine().setOnStart(()=>{
            transform.up = targetPosition;
        });
    }

    public void Stop(){
        StopCoroutine(launchedTask);
    }

    IEnumerator AsyncRotateAround(int index, int count, float launchTime){
        float targetTime = Time.time + launchTime;
        float slice = 2f * Mathf.PI / count;
        float angle = slice * index;
        while (Time.time < targetTime)
        {   
            float newX = (float)(2f * Mathf.Cos(angle));
            float newY = (float)(2f * Mathf.Sin(angle));
            Vector2 p = new Vector2(newX, newY);
            transform.localPosition = p;
            transform.up = p;
            angle -= Time.deltaTime * speed;
            yield return null;
        }
        LeanTween.moveLocal(gameObject, Vector3.zero, 0.2f).setEaseInSine().setOnComplete(()=>{
            gameObject.SetActive(false);
        });
        yield return null;
    }
}
