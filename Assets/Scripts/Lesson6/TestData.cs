using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestData : MonoBehaviour 
{
    public enum DataProviders
    {
        TXT,
        XML,
        PLAYER_PREFS,
        JSON
    }

    [ContextMenuItem("RunTest", nameof(RunTest))]
    public DataProviders DataProvider;

    private DataManager m_ToolManager;
    
    private void RunTest()
    {
        var path = Application.dataPath;
        var player = new PlayerData
        {
            Name = "Player777",
            HP = 88.8f,
            IsVisible = true
        };

        m_ToolManager = new DataManager();
        switch (DataProvider)
        {
            case DataProviders.JSON:
                m_ToolManager.SetData<JsonData>();
                break;
            case DataProviders.TXT:
                m_ToolManager.SetData<StreamData>();
                break;
            case DataProviders.PLAYER_PREFS:
                m_ToolManager.SetData<PlayerPrefsData>();
                break;
            case DataProviders.XML:
                m_ToolManager.SetData<XMLData>();
                break;
        }

        m_ToolManager.SetOptions(path);

        m_ToolManager.Save(player);
        var playerLoaded = m_ToolManager.Load();

        Debug.Log(player);
        Debug.Log(playerLoaded);
    }
}
