using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    Transform _t;
    private void Awake() {
        _t = transform;
    }
    private void LateUpdate() {
        _t.position = UnitManager.Instance.heroUnit.transform.position;
    }
    public List<SpawnAreaPos> spawnAreas = new List<SpawnAreaPos>();
    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        foreach (SpawnAreaPos item in spawnAreas)
        {
            Gizmos.DrawCube(transform.TransformPoint(item.pos1), Vector3.one);
            Gizmos.DrawCube(transform.TransformPoint(item.pos2), Vector3.one);
        }
    }
    public Vector3 GetRandomArea(){
        SpawnAreaPos spawnArea = spawnAreas[Random.Range(0,4)];
        return Vector3.Lerp(_t.TransformPoint(spawnArea.pos1), _t.TransformPoint(spawnArea.pos2), Random.Range(0f, 1f));
    }
    public Vector3 GetCenter(){
        return transform.position;
    }        
}

[System.Serializable]
public struct SpawnAreaPos{
    public Vector3 pos1, pos2;
}
