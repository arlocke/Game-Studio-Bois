using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    public static void SavePlayer(PlayerManager player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.test";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.test";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveObject(ObjectSaver obj, string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/";
        if(!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += key + ".txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        ObjectData data = new ObjectData(obj);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ObjectData LoadObject(string key)
    {
        string path = Application.persistentDataPath + "/saves/";
        if(Directory.Exists(path))
        {
            path += key + ".txt";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                ObjectData data = formatter.Deserialize(stream) as ObjectData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.LogError("Save file not found in " + path);
                return null;
            }
        }
        else
        {
            return null;
        }
        
    }

    public static void SaveObjectDestruction(string key)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        path += key + ".txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        ObjectData data = new ObjectData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
}
