using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Tools))]
public class ToolsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Tools tools = (Tools)target;
        
        var saveButton = GUILayout.Button("Сохранить и удалить объекты", EditorStyles.miniButton);
        if (saveButton)
        {
            tools.SaveTools();
        }
        
        var loadButton = GUILayout.Button("Загрузить объекты", EditorStyles.miniButton);
        if (loadButton)
        {
            tools.LoadTools();
        }
    }
}
