using UnityEngine;
using UnityEditor;
using Framework.Animation;

[CustomEditor(typeof(AnimationBase), true)]
public class AnimationBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Play"))
            (target as AnimationBase).Play();
    }
}
