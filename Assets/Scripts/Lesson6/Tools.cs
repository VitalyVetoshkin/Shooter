using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tools : MonoBehaviour
{
   private GameObject[] bombs;
   private GameObject[] kits;
   private GameObject[] tools;
   private string path;

   private void Awake()
   {
      bombs = GameObject.FindGameObjectsWithTag("bomb");
      kits = GameObject.FindGameObjectsWithTag("kit");

      tools = new GameObject[bombs.Length + kits.Length];
      bombs.CopyTo(tools, 0);
      kits.CopyTo(tools, bombs.Length);
      
      path = Path.Combine(Application.dataPath, "tool.xml");
   }

   public void SaveTools()
   {  
      var str = JsonUtility.ToJson(tools);
      File.WriteAllText(path, str);

      foreach (var tool in tools)
      {
         Destroy(tool);
      }
      
      Debug.Log("Data saved and clear!");
   }

   public void LoadTools()
   {
      if (!File.Exists(path)) return;

      var str = File.ReadAllText(path);
      tools = JsonUtility.FromJson<GameObject[]>(str);

      foreach (var tool in tools)
      {
         Instantiate(tool);
      }
      
      Debug.Log("Data loaded!");
   }
}
