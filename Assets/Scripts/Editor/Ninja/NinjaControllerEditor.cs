using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NinjaController))]
public class NinjaControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NinjaController ninja = (NinjaController)target;

        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Debug buttons only available in Play Mode.", MessageType.Info);
            return;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Debug Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Trigger Hurt"))
            ninja.TakeDamage(ninja.debugDamageAmount);

        GUI.enabled = ninja.IsAlive;
        if (GUILayout.Button("Trigger Die"))
        {
            ninja.TakeDamage(ninja.CurrentHealth);
        }
        GUI.enabled = true;
    }
}