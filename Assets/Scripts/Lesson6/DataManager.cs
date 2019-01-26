using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private IDataProvider dataProvider;

    public void SetData<T>() where T : IDataProvider, new ()
    {
        dataProvider = new T();
    }

    public void Save(PlayerData playerData)
    {
        dataProvider?.Save(playerData);
    }

    public PlayerData Load()
    {
        if (dataProvider == null) return default(PlayerData);
        return dataProvider.Load();
    }

    public void SetOptions(string path)
    {
        dataProvider?.SetOptions(path);
    }
}
