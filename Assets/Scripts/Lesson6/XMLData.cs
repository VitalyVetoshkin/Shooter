using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class XMLData : IDataProvider
{
    private string path;

    public PlayerData Load()
    {
        if (!File.Exists(path)) return default(PlayerData);

        var playerData = new PlayerData();

        using (XmlTextReader reader = new XmlTextReader(path))
        {
            while (reader.Read())
            {
                string key = "Name";
                if (reader.IsStartElement(key)) playerData.Name = reader.GetAttribute("value");

                key = "HP";
                if (reader.IsStartElement(key)) playerData.HP = Parse(reader.GetAttribute("value"), 100f);

                key = "IsVisible";
                if (reader.IsStartElement(key)) playerData.IsVisible = Parse(reader.GetAttribute("value"), false);
            }
        }

        Debug.Log("Data loaded!");
        return playerData;
    }

    public void Save(PlayerData playerData)
    {
        var xmlDoc = new XmlDocument();
        XmlNode rootNode = xmlDoc.CreateElement("PlayerData");
        xmlDoc.AppendChild(rootNode);

        var element = xmlDoc.CreateElement("Name");
        element.SetAttribute("value", playerData.Name);
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("HP");
        element.SetAttribute("value", playerData.HP.ToString());
        rootNode.AppendChild(element);

        element = xmlDoc.CreateElement("IsVisible");
        element.SetAttribute("value", playerData.IsVisible.ToString());
        rootNode.AppendChild(element);

        xmlDoc.Save(path);

        Debug.Log("Data saved!");
    }

    public void SetOptions(string path)
    {
        this.path = Path.Combine(path, "data.xml");
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
