using System.Collections.Generic;
using FPS;
using UnityEngine;
using UnityEditor;

public class SaveLoadObjectsEditor
{
    [MenuItem("Geekbrains/Save Level Objects")]
    private static void SaveObjects()
    {
        string path = EditorUtility.SaveFilePanel("Choose file", Application.dataPath,
            "LevelData", "xml");
        var objs = Object.FindObjectsOfType<GameObject>();
        var objsList = new List<SerialazableGameObject>();

        foreach (var o in objs)
        {
            objsList.Add(new SerialazableGameObject
            {
                PrefabName = o.name,
                Pos = o.transform.position,
                Scale = o.transform.localScale,
                Rot = o.transform.rotation
            });
        }
        
        XMLSerializator.Save(objsList.ToArray(), path);
    }
    
    [MenuItem("Geekbrains/Load Level Objects")]
    private static void LoadObjects()
    {
        string path = EditorUtility.OpenFilePanel("Choose file", Application.dataPath, "xml");

        var objs = XMLSerializator.Load(path);

        foreach (var o in objs)
        {
            GameObject prefab = Resources.Load<GameObject>(o.PrefabName);
            
            var tempObj = Object.Instantiate(prefab, o.Pos, o.Rot);
            tempObj.transform.localScale = o.Scale;
            tempObj.name = o.PrefabName;
        }
    }
}
