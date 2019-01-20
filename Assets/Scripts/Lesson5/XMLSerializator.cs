using System.IO;
using UnityEngine;
using System.Xml.Serialization;

public static class XMLSerializator
{
    private static XmlSerializer serializer;

    static XMLSerializator()
    {
        serializer = new XmlSerializer(typeof(SerialazableGameObject[]));
    }

    public static void Save(SerialazableGameObject[] objs, string path)
    {
        if (objs == null || objs.Length == 0 || string.IsNullOrEmpty(path)) return;

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(fs, objs);
        }
    }

    public static SerialazableGameObject[] Load(string path)
    {
        if (!File.Exists(path)) return null;

        SerialazableGameObject[] result;
        
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            result = (SerialazableGameObject[])serializer.Deserialize(fs);
        }

        return result;
    }
}
