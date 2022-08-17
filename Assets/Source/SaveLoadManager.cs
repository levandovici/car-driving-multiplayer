using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoadManager
{
    private static bool _StartServer = false;

    private static bool _StartClient = false;



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



    public static void SetUp(bool startServer, bool startClient)
    {
        StartServer = startServer;

        StartClient = startClient;
    }
}
