using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OdinSerializer;
using UnityEngine.Windows;

public static class SaveSystem<T>
{
    public static string PATH = Application.persistentDataPath + "/Saves/";
    public static string INTERNAL_PATH = Application.dataPath + "/Saves/";
    public static string FILENAME = "Save";
    public static string FILE_EXTENSION = ".porro";
    
    #region Binary

    public static void SaveInternalBinary<T>(T objectToSave, int fileID)
    {
        if (!Exists(fileID, true))
        {
            Directory.CreateDirectory(INTERNAL_PATH);
        }
        byte[] bytes = SerializationUtility.SerializeValue(objectToSave, DataFormat.Binary);
        File.WriteAllBytes(INTERNAL_PATH+FILENAME+fileID+FILE_EXTENSION, bytes);
    }

    public static T LoadInternalBinary<T>(int fileID)
    {
        byte[] bytes = File.ReadAllBytes(INTERNAL_PATH + FILENAME + fileID + FILE_EXTENSION);
        return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);
    }

    public static void SaveInternalJSON<T>(T objectToSave, int fileID)
    {
        if (!Exists(fileID, true))
        {
            Directory.CreateDirectory(INTERNAL_PATH);
        }
        byte[] bytes = SerializationUtility.SerializeValue(objectToSave, DataFormat.JSON);
        File.WriteAllBytes(INTERNAL_PATH+FILENAME+fileID+FILE_EXTENSION, bytes);
    }

    public static T LoadInternalJSON<T>(int fileID)
    {
        if (Exists(fileID, true))
        { 
            byte[] bytes = File.ReadAllBytes(INTERNAL_PATH + FILENAME + fileID + FILE_EXTENSION);
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
        }
        return default(T);
}

    public static void SaveBinary<T>(T objectToSave, int fileID)
    {
        if (!Exists(fileID))
        {
            Directory.CreateDirectory(PATH);
        }
        byte[] bytes = SerializationUtility.SerializeValue(objectToSave, DataFormat.Binary);
        File.WriteAllBytes(PATH+FILENAME+fileID+FILE_EXTENSION, bytes);
        
    }

    public static T LoadBinary<T>(int fileID)
    {
        if (!Exists(fileID))
        {
            byte[] bytes = File.ReadAllBytes(PATH + FILENAME + fileID + FILE_EXTENSION);
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);
        }
        return default(T);
        
    }
    
    #endregion

    #region JSON

    public static void SaveJSON<T>(T objectToSave, int fileID)
    {
        if (!Exists(fileID))
        {
            Directory.CreateDirectory(PATH);
        }
        byte[] bytes = SerializationUtility.SerializeValue(objectToSave, DataFormat.JSON);
        File.WriteAllBytes(PATH+FILENAME+fileID+FILE_EXTENSION, bytes);
    }

    public static T LoadJSON<T>(int fileID)
    {
        if (Exists(fileID))
        {
            byte[] bytes = File.ReadAllBytes(PATH + FILENAME + fileID + FILE_EXTENSION);
            return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.JSON);
        }

        return default(T);
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
    
    
    public static bool Exists(int fileID, bool internalPath = false)
    {
        if (internalPath)
        {
            return Directory.Exists(INTERNAL_PATH + FILENAME + fileID + FILE_EXTENSION);
        }
        return Directory.Exists(PATH + FILENAME + fileID + FILE_EXTENSION);
    }
    
}
