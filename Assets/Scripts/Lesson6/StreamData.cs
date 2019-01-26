using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StreamData : IDataProvider
{
    private string path;

    public PlayerData Load()
    {
        if (!File.Exists(path)) return default(PlayerData);

        var playerData = new PlayerData();
        using (StreamReader sr = new StreamReader(path))
        {
            while (!sr.EndOfStream)
            {
                playerData.Name = sr.ReadLine();
                playerData.HP = Parse(sr.ReadLine(), 100f);
                playerData.IsVisible = Parse(sr.ReadLine(), false);
            }
        }

        Debug.Log("Data loaded!");
        return playerData;
    }

    public void Save(PlayerData playerData)
    {
        using (var sw = new StreamWriter(path))
        {
            sw.WriteLine(playerData.Name);
            sw.WriteLine(playerData.HP);
            sw.WriteLine(playerData.IsVisible);
        }

        Debug.Log("Data saved!");
    }

    public void SetOptions(string path)
    {
        this.path = Path.Combine(path, "data.txt");
    }
    
    private float Parse(string value, float defaultValue)
    {
        try  {return float.Parse(value);}
        catch{return defaultValue;}
    }
    private bool Parse(string value, bool defaultValue)
    {
        try  {return bool.Parse(value);}
        catch{return defaultValue;}
    }
}
