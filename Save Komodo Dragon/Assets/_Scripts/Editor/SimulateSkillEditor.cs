using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimulateSkill))]
public class SimulateSkillEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        SimulateSkill simulateSkill = (SimulateSkill)target;
        if(GUILayout.Button("Simulate")){
            simulateSkill.Attack();
        }
        if(GUILayout.Button("Upgrade")){
            simulateSkill.Upgrade();
        }
    }
}
