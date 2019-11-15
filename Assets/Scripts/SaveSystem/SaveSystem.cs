using System;
using System.IO;
using UnityEngine;
using OdinSerializer;

public static class SaveSystem
{
    public static string PATH = Application.persistentDataPath + "/Saves/";
    
    #region Binary

    
    public static void SaveBinary<T>(T objectToSave,String path)
    {
        byte[] bytes = SerializationUtility.SerializeValue(objectToSave, DataFormat.Binary);
        File.WriteAllBytes(PATH + path, bytes);

    }

    public static T LoadBinary<T>(String path)
    {
        byte[] bytes = File.ReadAllBytes(PATH+path);
        return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);

    }
    
    #endregion

    #region JSON

    public static void SaveJSON<T>(T objectToSave, String path)
    {
        var bytes = SerializationUtility.SerializeValue(objectToSave, DataFormat.JSON);
        File.WriteAllBytes(PATH +path, bytes);
    }

    public static T LoadJSON<T>(String path)
    {
        if (File.Exists(PATH + path))
        {

            var bytes = File.ReadAllBytes(PATH + path);
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
        }

        Debug.LogError("No Data saves in "+path);
        return default;

    }

    #endregion

    #region PlayerPrefs
    public static void SavePrefs(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public static void SavePrefs(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public static void SavePrefs(string key, int value)
    {

        PlayerPrefs.SetInt(key, value);
    }
    

    public static float LoadFloat(string key)
    {
        if(PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetFloat(key);
        return -1;
    }

    public static string LoadString(string key)
    {
        if(PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetString(key);

        return "Key Vacia";
    }

    public static int LoadInt(string key)
    {
        if(PlayerPrefs.HasKey(key))
            return PlayerPrefs.GetInt(key);

        return -1;
    }
    #endregion

    public static bool Exists(string path)
    {
        return Directory.Exists(PATH);
    }
}
