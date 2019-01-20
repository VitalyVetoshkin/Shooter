using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Lesson5))]
public class Lesson5Editor : Editor
{
    public override void OnInspectorGUI()
    {
        bool isButtonPressed = false;
        
        DrawDefaultInspector();

        Lesson5 les5 = (Lesson5)target;
        //les5.Prefab = EditorGUILayout.ObjectField("Префаб", les5.Prefab, typeof(GameObject), true) as GameObject;

        var spawnButton = GUILayout.Button("Заспаунить объекты", EditorStyles.miniButton);
        if (spawnButton)
        {
            les5.CreateObjs();
            isButtonPressed = true;
        }

        if (isButtonPressed)
        {
            EditorGUILayout.HelpBox("Кнопка была нажата!", MessageType.Info);
        }
    }
}
