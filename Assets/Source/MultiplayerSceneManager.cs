using LimonadoEntertainment.Net.Multiplayer.Commands;
using LimonadoEntertainment.Net.Multiplayer.Chat;
using LimonadoEntertainment.Net.Multiplayer.Data;
using LimonadoEntertainment.Net.Multiplayer;
using LimonadoEntertainment.Debug;
using LimonadoEntertainment.Data;
using LimonadoEntertainment.Net;
using LimonadoEntertainment;
using UnityEngine.UI;
using UnityEngine;
using System.Text;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.IO;


public class MultiplayerSceneManager : GameSceneManager
{
    [SerializeField]
    private float _updateRate = 0f;

    [SerializeField]
    private float _lastUpdate = 0f;


    [SerializeField]
    private PlayerCar _carPrefab;


    [SerializeField]
    private Vector3 _position = Vector3.zero;

    [SerializeField]
    private Vector3 _rotation = Vector3.zero;


    [SerializeField]
    private Vector3 _fl_position = Vector3.zero;

    [SerializeField]
    private Vector3 _fl_rotation = Vector3.zero;


    [SerializeField]
    private Vector3 _fr_position = Vector3.zero;

    [SerializeField]
    private Vector3 _fr_rotation = Vector3.zero;


    [SerializeField]
    private Vector3 _bl_position = Vector3.zero;

    [SerializeField]
    private Vector3 _bl_rotation = Vector3.zero;


    [SerializeField]
    private Vector3 _br_position = Vector3.zero;

    [SerializeField]
    private Vector3 _br_rotation = Vector3.zero;


    [SerializeField]
    private bool _lights = false;


    [SerializeField]
    private Dictionary<string, PlayerCar> _players;

    

    private new void Awake()
    {
        base.Awake();



        DebugConsole.Enabled = true;

        DebugConsole.OnLog += Debug.Log;

        DebugConsole.OnLogWarning += Debug.LogWarning;

        DebugConsole.OnLogError += Debug.LogError;



        Multiplayer.OnStartClient += () =>
        {
            string server_id = "";

            Multiplayer.Client.OnResponse += (m) =>
            {
                Debug.Log("[RESPONSE]");

                string message = m.GetMessage;

                Terminal terminal = JsonUtility.FromJson<Terminal>(message);

                Command[] commands = terminal.Commands;


                for (int i = 0; i < commands.Length; i++)
                {
                    Debug.Log($"[COMMAND][{i}][{commands[i].ToString()}]");
                }
               

                for (int i = 0; i < commands.Length; i++)
                {
                    if (commands[i] == Command.New("server-id"))
                    {
                        string arg = commands[i].Arguments[1];

                        server_id = arg;

                        Debug.Log($"[SERVER-ID][{arg}]");

                        string data = JsonUtility.ToJson(new GameData(new CharacterData(0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, false)));

                        Multiplayer.Client.GameData = new JsonStorage(data);

                        Multiplayer.Client.Request(new Message(JsonUtility.ToJson(Terminal.New("register").Arg(data))));
                    }
                    else if (commands[i] == Command.New("credentials"))
                    {
                        string arg = commands[i].Arguments[1];

                        Credentials credentials = JsonUtility.FromJson<Credentials>(arg);

                        Multiplayer.Client.ClientData = new ClientGameData(server_id, credentials);

                        Multiplayer.Client.Request(new Message(JsonUtility.ToJson(Terminal.New("log-in").Arg($"{JsonUtility.ToJson(credentials)}"))));
                    }
                    else if (commands[i] == Command.New("log-in-successful"))
                    {
                        Debug.Log("[LOG-IN-SUCCESSFUL]");

                        Multiplayer.Client.Request(new Message(JsonUtility.ToJson(Terminal.New("get-server-data"))));
                    }
                    else if (commands[i] == Command.New("log-in-error"))
                    {
                        Debug.Log("[LOG-IN-ERROR]");

                        Multiplayer.Client.Request(new Message(JsonUtility.ToJson(Terminal.New("disconnect"))));
                    }
                    else if (commands[i] == Command.New("server-data"))
                    {
                        Debug.Log("[SERVER-DATA]");

                        ServerGameData server_data = JsonUtility.FromJson<ServerGameData>(commands[i].Arguments[1]);

                        Multiplayer.Client.ServerData = server_data;
                    }
                }
            };

            Multiplayer.Client.OnDisconnected += () => Debug.LogWarning("[DISCONNECTED]");
        };

        Multiplayer.OnStartServer += () =>
        {
            Multiplayer.Server.OnClientConnected += (s) =>
            {
                Debug.LogWarning($"[ID][{s}][CONNECTED]");
            };

            Multiplayer.Server.OnClientDisconnected += (s) =>
            {
                Debug.LogWarning($"[ID][{s}][DISCONNECTED]");
            };

            Multiplayer.Server.OnRequest += (im) =>
            {
                Debug.Log("[REQUEST]");

                string message = im.Message.GetMessage;

                Debug.Log($"[REQUEST-MESSAGE][{message}]");


                Terminal terminal = JsonUtility.FromJson<Terminal>(message);

                Command[] commands = terminal.Commands;

                Terminal response_terminal = Terminal.New();


                for (int i = 0; i < commands.Length; i++)
                {
                    Debug.Log($"[COMMAND][{i}][{commands[i].ToString()}]");
                }


                for (int i = 0; i < commands.Length; i++)
                {
                    if (commands[i] == Command.New("get-server-id"))
                    {
                        response_terminal.Next("server-id").Arg($"{Multiplayer.Server.PublicServerData.ServerID}");
                    }
                    else if(commands[i] == Command.New("get-server-info"))
                    {
                        response_terminal.Next("server-info").Arg(JsonUtility.ToJson(new ServerInfo(Multiplayer.Server.IPEndPoint.Port, Multiplayer.Name, Multiplayer.Server.PublicServerData.ServerID, Multiplayer.Server.PublicServerData.Clients.Length)));
                    }
                    else if (commands[i] == Command.New("register"))
                    {
                        if (commands[i].Arguments.Length > 1)
                        {
                            JsonStorage data = new JsonStorage(commands[i].Arguments[1]);

                            Credentials credentials = Multiplayer.Server.RegisterNewPlayer(data);

                            Debug.Log(credentials);

                            response_terminal.Next($"credentials").Arg($"{JsonUtility.ToJson(credentials)}");
                        }
                    }
                    else if (commands[i] == Command.New("log-in"))
                    {
                        if (commands[i].Arguments.Length > 1)
                        {
                            Credentials credentials = JsonUtility.FromJson<Credentials>(commands[i].Arguments[1]);

                            if (Multiplayer.Server.Contains(credentials))
                            {
                                Multiplayer.Server.LogInPlayer(im.ID, credentials);

                                response_terminal.Next($"log-in-successful");
                            }
                            else
                            {
                                response_terminal.Next($"log-in-error");
                            }
                        }
                    }
                    else if (commands[i] == Command.New("disconnect"))
                    {
                        Multiplayer.Server.Disconnect(im.ID);
                    }
                    else if (commands[i] == Command.New("set-game-data"))
                    {
                        if (commands[i].Arguments.Length > 1)
                        {
                            ServerClientGameData data;

                            bool success = Multiplayer.Server.TryGetLoggedInPlayerPrivateData(im.ID, out data);

                            if (success)
                            {
                                data.Data.Json = commands[i].Arguments[1];
                            }
                        }
                    }
                    else if (commands[i] == Command.New("get-server-data"))
                    {
                        response_terminal.Next($"server-data").Arg($"{JsonUtility.ToJson(Multiplayer.Server.PublicServerData)}");
                    }
                }

                Multiplayer.Server.Response(new IdentifiedMessage(
                        new Message(JsonUtility.ToJson(response_terminal)), im.ID));
            };

            Multiplayer.Server.OnDisconnected += () => Debug.LogWarning("DISCONNECTED");
        };

        Multiplayer.OnClientStarted += () =>
        {
            Multiplayer.Client.Request(new Message(JsonUtility.ToJson(Terminal.New("get-server-id"))));
        };

        Multiplayer.OnServerStarted += () => Debug.LogWarning("[SERVER-STARTED]");

        _gameUIManager.GameUI.OnExit += () =>
        {
            Multiplayer.Stop();

            DebugConsole.ClearEvents();

            Multiplayer.ClearEvents();

            SceneManager.LoadScene(0);
        };
    }



    private void Start()
    {
        if (SaveLoadManager.StartServer)
        {
            EPlatform platform;

#if UNITY_STANDALONE || UNITY_EDITOR

            platform = EPlatform.Standalone;

#elif UNITY_ANDROID

            platform = EPlatform.Android;

#endif

            Multiplayer.StartServer(platform, new ServerGameData(System.Guid.NewGuid().ToString()), (locatedMessage) =>
            {
                Debug.LogError(locatedMessage.Message);

                AppMessage appMessage = locatedMessage.Message;

                if (appMessage.Name == "car-driving-multiplayer")
                {
                    ServerInfo info = new ServerInfo(Multiplayer.Server.IPEndPoint.Port, Multiplayer.Name, Multiplayer.Server.PublicServerData.ServerID, Multiplayer.Server.PublicServerData.Clients.Length);

                    return new AppMessage(1, "car-driving-multiplayer", $"{JsonUtility.ToJson(Command.New("server-info").Arg(JsonUtility.ToJson(info)))}");
                }
                else
                {
                    return new AppMessage(1, "car-driving-multiplayer", "denied");
                }
            }, 0);
        }

        if (SaveLoadManager.StartClient)
        {
            Multiplayer.StartClient();
        }

        _car.CarTransform.position = new Vector3(UnityEngine.Random.Range(5f, 20f), 1f, UnityEngine.Random.Range(-200f, 200f));
    }



    private void Update()
    {
        _position = _car.CarTransform.position;

        _rotation = _car.CarTransform.eulerAngles;


        _fl_position = _car.FL.localPosition;

        _fl_rotation = _car.FL.localEulerAngles;


        _fr_position = _car.FR.localPosition;

        _fr_rotation = _car.FR.localEulerAngles;


        _bl_position = _car.BL.localPosition;

        _bl_rotation = _car.BL.localEulerAngles;


        _br_position = _car.BR.localPosition;

        _br_rotation = _car.BR.localEulerAngles;


        _lights = _car.PoliceLights.activeLight;



        if (Multiplayer.IsServer)
        {
            ServerGameData data = Multiplayer.Server.PublicServerData;

            Debug.LogError($"Clients: {data.Clients.Length}");
        }

        if (Multiplayer.IsClient)
        {
            if (Multiplayer.Client.IsInitialized)
            {
               SetUpPlayers(Multiplayer.Client.ServerData);
            }

            if (Multiplayer.Client.CanRequest)
            {
                if (Time.time >= _updateRate + _lastUpdate)
                {
                    _lastUpdate = Time.time;

                    if (!Multiplayer.Client.IsClosed)
                    {
                        Terminal commands = Terminal.New();

                        commands.Next("set-game-data").Arg($"{JsonUtility.ToJson(new GameData(new CharacterData(_position.x, _position.y, _position.z, _rotation.x, _rotation.y, _rotation.z, _fl_position.x, _fl_position.y, _fl_position.z, _fl_rotation.x, _fl_rotation.y, _fl_rotation.z, _fr_position.x, _fr_position.y, _fr_position.z, _fr_rotation.x, _fr_rotation.y, _fr_rotation.z, _bl_position.x, _bl_position.y, _bl_position.z, _bl_rotation.x, _bl_rotation.y, _bl_rotation.z, _br_position.x, _br_position.y, _br_position.z, _bl_rotation.y, _br_rotation.y, _br_rotation.z, _lights)))}");

                        commands.Next("get-server-data");

                        Multiplayer.Client.Request(new Message(JsonUtility.ToJson(commands)));
                    }

                }
            }
        }
    }

    

    private void OnDestroy()
    {
        Multiplayer.Stop();

        DebugConsole.ClearEvents();

        Multiplayer.ClearEvents();

        StopAllCoroutines();
    }



    private void SetUpPlayers(ServerGameData data)
    {
        if(_players == null)
        {
            _players = new Dictionary<string, PlayerCar>();
        }

        for(int i = 0; i < data.Clients.Length; i++)
        {
            if (data.Clients[i].Credentials.ID != Multiplayer.Client.ClientData.Credentials.ID)
            {
                PlayerCar playerCar = null;

                bool contains = _players.TryGetValue(data.Clients[i].Credentials.ID, out playerCar);

                try
                {
                    GameData gameData = data.Clients[i].Data.Get<GameData>();

                    CharacterData characterData = gameData.CharacterData;

                    if (!contains)
                    {
                        playerCar = Instantiate(_carPrefab);

                        _players.Add(data.Clients[i].Credentials.ID, playerCar);
                    }

                    playerCar.Car.transform.position = new Vector3(characterData.PositionX, characterData.PositionY, characterData.PositionZ);

                    playerCar.Car.transform.eulerAngles = new Vector3(characterData.RotationX, characterData.RotationY, characterData.RotationZ);


                    playerCar.FrontLeftWheel.transform.localPosition = new Vector3(characterData.FLPositionX, characterData.FLPositionY, characterData.FLPositionZ);

                    playerCar.FrontLeftWheel.transform.localEulerAngles = new Vector3(characterData.FLRotationX, characterData.FLRotationY, characterData.FLRotationZ);


                    playerCar.FrontRightWheel.transform.localPosition = new Vector3(characterData.FRPositionX, characterData.FRPositionY, characterData.FRPositionZ);

                    playerCar.FrontRightWheel.transform.localEulerAngles = new Vector3(characterData.FRRotationX, characterData.FRRotationY, characterData.FRRotationZ);


                    playerCar.BackLeftWheel.transform.localPosition = new Vector3(characterData.BLPositionX, characterData.BLPositionY, characterData.BLPositionZ);

                    playerCar.BackLeftWheel.transform.localEulerAngles = new Vector3(characterData.BLRotationX, characterData.BLRotationY, characterData.BLRotationZ);


                    playerCar.BackRightWheel.transform.localPosition = new Vector3(characterData.BRPositionX, characterData.BRPositionY, characterData.BRPositionZ);

                    playerCar.BackRightWheel.transform.localEulerAngles = new Vector3(characterData.BRRotationX, characterData.BRRotationY, characterData.BRRotationZ);


                    playerCar.PoliceLights.activeLight = characterData.Lights;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
    }
}