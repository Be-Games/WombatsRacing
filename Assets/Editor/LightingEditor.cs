using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(EnvironmentSettingsManager))]
public class LightingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnvironmentSettingsManager lighting = (EnvironmentSettingsManager)target;
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Day Mode"))
        {
            lighting.DayMode();
        }
        
        if (GUILayout.Button("Night Mode"))
        {
            lighting.NightMode();
        }
        
        GUILayout.EndHorizontal();
    }
}
