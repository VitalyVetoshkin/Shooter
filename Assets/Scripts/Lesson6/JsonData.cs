using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonData : IDataProvider
{
    private string path;

    public PlayerData Load()
    {
        if (!File.Exists(path)) return default(PlayerData);

        var playerData = new PlayerData();

        var str = File.ReadAllText(path);
        playerData = JsonUtility.FromJson<PlayerData>(str);

        Debug.Log("Data loaded!");
        return playerData;
    }

    public void Save(PlayerData playerData)
    {
        var str = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, str);

        Debug.Log("Data saved!");
    }

    public void SetOptions(string path)
    {
        this.path = Path.Combine(path, "data.json");
    }
}
