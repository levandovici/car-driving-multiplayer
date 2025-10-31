using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoadManager
{
    private static bool _StartServer = false;

    private static bool _StartClient = false;

    private static PlayerData _Data = null;



    public static bool StartServer
    {
        get
        {
            return _StartServer;
        }

        private set
        {
            _StartServer = value;
        }
    }

    public static bool StartClient
    {
        get
        {
            return _StartClient;
        }

        private set
        {
            _StartClient = value;
        }
    }

    public static PlayerData PlayerData
    {
        get
        {
            if (_Data == null)
            {
                Load();
            }

            return _Data;
        }
    }



    public static void SetUp(bool startServer, bool startClient)
    {
        StartServer = startServer;

        StartClient = startClient;
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(_Data);

        PlayerPrefs.SetString("data", json);

        PlayerPrefs.Save();
    }

    public static void Load()
    {
        if (PlayerPrefs.HasKey("data"))
        {
            string json = PlayerPrefs.GetString("data");

            _Data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            _Data = new PlayerData();
        }
    }
}
