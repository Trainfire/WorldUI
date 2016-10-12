using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Toggleable), true)]
public class ToggleableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Toggle"))
            (target as Toggleable).Toggle();
    }
}
