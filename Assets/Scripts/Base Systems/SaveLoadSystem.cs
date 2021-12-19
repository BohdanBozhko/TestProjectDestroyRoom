using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SLS
{
    public static GameData Data => SaveLoadSystem.Instance.Data;

    public static void Save()
    {
        SaveLoadSystem.Instance.SaveData();
    }
}

public class SaveLoadSystem : MonoSingleton<SaveLoadSystem>
{
    public GameData Data;

    private const string DataKey = "Data";


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        LoadData();
    }

    public void SaveData()
    {
        if (Data == null) return;

        var json = JsonUtility.ToJson(Data);
        PlayerPrefs.SetString(DataKey, json);
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(DataKey))
        {
            Data = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(DataKey));
        }
        else
        {
            Data = new GameData();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnDisable()
    {
        SaveData();
    }
}

