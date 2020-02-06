using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        PlayerStats myTarget = (PlayerStats)target;

        myTarget.currentExperience = EditorGUILayout.IntField("Experience Again", myTarget.currentExperience);
        EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
        if(GUILayout.Button("Print Level"))
        {
            myTarget.PrintLevel();
        }
    }
    
    
}


