using LimonadoEntertainment.Net.Multiplayer.Commands;
using LimonadoEntertainment.Net.Multiplayer.Data;
using LimonadoEntertainment.Net.Multiplayer;
using LimonadoEntertainment.Debug;
using LimonadoEntertainment.Data;
using LimonadoEntertainment.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine.UI;
using UnityEngine;
using System.Net;
using System;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    private MainUIManager _mainUIManager;



    [SerializeField]
    private Transform _localHostButtons;



    [SerializeField]
    private Button _button_prefab;

    private List<LocatedServerInfoButton> _server_info_buttons = new List<LocatedServerInfoButton>();

    private LocatedServerInfoStack _stack = new LocatedServerInfoStack();

    private int _max_stack_length = 256;

    private StringBuilder stringBuilder = new StringBuilder();

    private object @lock = new object();

    [SerializeField]
    private Text _debug;



    private void SetUpHost()
    {
        for (int i = 0; i < _localHostButtons.childCount; i++)
        {
            Destroy(_localHostButtons.GetChild(i).gameObject);
        }
    }



    private void Awake()
    {
        _mainUIManager.MainUI.OnClickSingleplayer += () =>
        {
            SceneManager.LoadScene(1);
        };

        _debug.text = "Enable DebugConsole \n";

        DebugConsole.Enabled = true;

        DebugConsole.OnLog += (log) =>
        {
            Debug.Log(log);

            lock(@lock)
            {
                stringBuilder.AppendLine(log);
            }
        };

        DebugConsole.OnLogWarning += (log) =>
        {
            Debug.LogWarning(log);

            lock (@lock)
            {
                stringBuilder.AppendLine(log);
            }
        };

        DebugConsole.OnLogError += (log) =>
        {
            Debug.LogError(log);

            lock (@lock)
            {
                stringBuilder.AppendLine(log);
            }
        };

        _debug.text = _debug.text + "Set Up Host \n";

        SetUpHost();

        LimonadoEntertainment.EPlatform platform;

#if UNITY_STANDALONE || UNITY_EDITOR

        platform = LimonadoEntertainment.EPlatform.Standalone;

#elif UNITY_ANDROID

        platform = LimonadoEntertainment.EPlatform.Android;

#endif

        _debug.text = _debug.text + $"Platform: {platform} \n";

        _debug.text = _debug.text + $"OnClickMultiplayer \n";

        _mainUIManager.MainUI.OnClickMultiplayer += () =>
        {
            Multiplayer.StopBroadcastClient();

            IPAddress[] ips = null;

            bool success = Lan.TryGetLocalIPv4Addresses(platform, out ips);

            Multiplayer.Name = "Car Driving Multiplayer";

            if (success)
            {
                Multiplayer.IpAddress = ips[0];

                _debug.text = _debug.text + $"IP: {ips[0]} \n";
            }
            else
            {
                Multiplayer.IpAddress = IPAddress.Any;

                _debug.text = _debug.text + $"Any IP \n";
            }

            Debug.LogError($"IP: {Multiplayer.IpAddress}");

            SaveLoadManager.SetUp(true, true);

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        };


        _debug.text = _debug.text + $"StartBroadcastClient \n";

        try
        {
            Multiplayer.StartBroadcastClient(platform, new AppMessage(1, "car-driving-multiplayer", JsonUtility.ToJson(Command.New("get-server-info"))), (lm) =>
            {
                Debug.LogWarning(lm.Message.Message);

                if (lm != null && lm.Message != null && lm.Message.Name == "car-driving-multiplayer")
                {
                    try
                    {
                        Command command = JsonUtility.FromJson<Command>(lm.Message.Message);

                        if (command == Command.New("server-info") && command.Arguments.Length > 1)
                        {
                            ServerInfo serverInfo = JsonUtility.FromJson<ServerInfo>(command.Arguments[1]);

                            Debug.Log(lm.IPEndPoint);

                            if (_stack.Count() < _max_stack_length)
                            {
                                _stack.Push(new LocatedServerInfo(serverInfo, lm.IPEndPoint));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[ERROR][ReceiveServerInfo][{e.Message}]");
                    }
                }
            });
        }
        catch(Exception e)
        {
            _debug.text = _debug.text + $"{e.Message} \n";

            _debug.text = _debug.text + $"{e.Source} \n";

            _debug.text = _debug.text + $"{e.StackTrace} \n";
        }

        _debug.text = _debug.text + $"StartCheckServerInfo \n";

        StartCoroutine(CheckServerInfoStack());

        _debug.text = _debug.text + $"End \n";
    }



    private void OnDestroy()
    {
        Multiplayer.StopBroadcastClient();

        DebugConsole.ClearEvents();

        StopAllCoroutines();
    }

    private void OnApplicationQuit()
    {
        lock (@lock)
        {
            string path;

#if UNITY_STANDALONE || UNITY_EDITOR

            path = Path.Combine(Application.dataPath, "DebugLog.txt");

#elif UNITY_ANDROID

            path = Path.Combine(Application.persistentDataPath, "DebugLog.txt");

#endif

            File.WriteAllText(path, stringBuilder.ToString());

            Debug.Log(stringBuilder.ToString());
        }
    }



    private IEnumerator CheckServerInfoStack()
    {
        while (true)
        {
            _debug.text = _debug.text + $"Mailbox is free \n";

            while (_stack.Count() > 0)
            {
                LocatedServerInfo located = _stack.Pop();

                _debug.text = _debug.text + $"Message: {located.IPEndPoint} \n";

                _debug.text = _debug.text + $"Message: {located.ServerInfo.Port} \n";

                lock (@lock)
                {
                    stringBuilder.AppendLine(located.ServerInfo.Port.ToString());
                }


                bool contains = false;

                foreach (LocatedServerInfoButton s in _server_info_buttons)
                {
                    if (s.locatedServerInfo.IPEndPoint.Equals(located.IPEndPoint))
                    {
                        s.locatedServerInfo = located;

                        contains = true;

                        Debug.LogWarning("Contains!");


                        s.button.onClick.RemoveAllListeners();

                        s.button.onClick.AddListener(() =>
                        {
                            Multiplayer.IpAddress = s.locatedServerInfo.IPEndPoint.Address;

                            Multiplayer.Port = s.locatedServerInfo.ServerInfo.Port;

                            SaveLoadManager.SetUp(false, true);

                            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                        });


                        IPEndPoint point = new IPEndPoint(s.locatedServerInfo.IPEndPoint.Address, s.locatedServerInfo.ServerInfo.Port);

                        s.text.text = $"Server: {point}";
                    }
                }

                if (!contains)
                {
                    Button button = Instantiate(_button_prefab, _localHostButtons);

                    Text text = button.transform.GetChild(0).GetComponent<Text>();


                    button.onClick.AddListener(() =>
                    {
                        Multiplayer.IpAddress = located.IPEndPoint.Address;

                        Multiplayer.Port = located.ServerInfo.Port;

                        SaveLoadManager.SetUp(false, true);

                        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                    });


                    IPEndPoint point = new IPEndPoint(located.IPEndPoint.Address, located.ServerInfo.Port);

                    text.text = $"Server: {point}";


                    LocatedServerInfoButton serverInfoButton = new LocatedServerInfoButton(located, button, text);

                    _server_info_buttons.Add(serverInfoButton);
                }
            }


            yield return new WaitForSeconds(1f);
        }
    }



    public class LocatedServerInfoButton
    {
        public LocatedServerInfo locatedServerInfo;

        public Button button;

        public Text text;



        public LocatedServerInfoButton(LocatedServerInfo locatedServerInfo, Button button, Text text)
        {
            this.locatedServerInfo = locatedServerInfo;

            this.button = button;

            this.text = text;
        }
    }
}
