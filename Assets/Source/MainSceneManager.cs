using Michitai.Lan.Net.Multiplayer.Commands;
using Michitai.Lan.Net.Multiplayer.Data;
using Michitai.Lan.Net.Multiplayer;
using Michitai.Lan.Debug;
using Michitai.Lan.Data;
using Michitai.Lan.Net;
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
    private const string PRIVACY = "https://games.michitai.com/privacy-policy.html";

    private const string TERMS = "https://games.michitai.com/terms-and-conditions.html";



    [SerializeField]
    private MainUIManager _mainUIManager;

    [SerializeField]
    private CarsController _carsController;

    [SerializeField]
    private SoundController _soundController;



    [SerializeField]
    private Transform _localHostButtons;



    [SerializeField]
    private Button _button_prefab;

    private List<LocatedServerInfoButton> _server_info_buttons = new List<LocatedServerInfoButton>();

    private LocatedServerInfoStack _stack = new LocatedServerInfoStack();

    private int _max_stack_length = 256;

    private Michitai.Lan.EPlatform _platform;

    private int _shopIndex = -1;



    private void SetUpHost()
    {
        for (int i = 0; i < _localHostButtons.childCount; i++)
        {
            Destroy(_localHostButtons.GetChild(i).gameObject);
        }
    }



    private void Awake()
    {
        InitialisePlatform();

        InitialiseUI();

        InitialiseDebug();

        InitialiseMultiplayer();

        InitialiseCarsController();

        _soundController.SetupSFX(SaveLoadManager.PlayerData.sfxVolume);

        _soundController.SetupMusic(SaveLoadManager.PlayerData.musicVolume);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.M))
        {
            SaveLoadManager.PlayerData.money += 5000;

            OnChangePanel(_mainUIManager.Current);
        }
#endif
    }

    private void InitialisePlatform()
    {
#if UNITY_STANDALONE

        _platform = Michitai.Lan.EPlatform.Standalone;

#elif UNITY_ANDROID

        _platform = Michitai.Lan.EPlatform.Android;

#endif
    }

    private void InitialiseUI()
    {
        _mainUIManager.OnChangePanel += OnChangePanel;


        _mainUIManager.Shop.OnLeft += OnShopLeft;

        _mainUIManager.Shop.OnRight += OnShopRight;

        _mainUIManager.Shop.OnSelect += OnShopSelect;

        _mainUIManager.Shop.OnBuy += OnShopBuy;

        _mainUIManager.Shop.OnBack += OnShopBack;


        _mainUIManager.Play.OnSingleplayer += () =>
        {
            SceneManager.LoadScene(1);
        };

        _mainUIManager.Play.OnMultiplayer += () =>
        {
            Multiplayer.StopBroadcastClient();

            IPAddress[] ips = null;

            bool success = Lan.TryGetLocalIPv4Addresses(_platform, out ips);

            Multiplayer.Name = "Car Driving Multiplayer";

            if (success)
            {
                Multiplayer.IpAddress = ips[0];
            }
            else
            {
                Multiplayer.IpAddress = IPAddress.Any;
            }

            Debug.LogError($"IP: {Multiplayer.IpAddress}");

            SaveLoadManager.SetUp(true, true);

            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        };


        _mainUIManager.Settings.OnSfxChanged += OnSfxChanged;

        _mainUIManager.Settings.OnMusicChanged += OnMusicChanged;

        _mainUIManager.Settings.OnPrivacy += () => Application.OpenURL(PRIVACY);

        _mainUIManager.Settings.OnTerms += () => Application.OpenURL(TERMS);

        _mainUIManager.Settings.OnBack += () => SaveLoadManager.Save();
    }

    private void InitialiseDebug()
    {
        DebugConsole.Enabled = true;

        DebugConsole.OnLog += (log) =>
        {
            Debug.Log(log);
        };

        DebugConsole.OnLogWarning += (log) =>
        {
            Debug.LogWarning(log);
        };

        DebugConsole.OnLogError += (log) =>
        {
            Debug.LogError(log);
        };
    }

    private void InitialiseMultiplayer()
    {
        SetUpHost();

        Multiplayer.StartBroadcastClient(_platform, new AppMessage(1, "car-driving-multiplayer", JsonUtility.ToJson(Command.New("get-server-info"))), (lm) =>
        {
            if (lm != null && lm.Message != null)
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

        StartCoroutine(CheckServerInfoStack());
    }

    private void InitialiseCarsController()
    {
        _carsController.Setup(SaveLoadManager.PlayerData.carIndex);
    }



    private void OnChangePanel(MainUIManager.EPanel panel)
    {
        switch(panel)
        {
            case MainUIManager.EPanel.Main:
                OnSetupMain();
                break;

            case MainUIManager.EPanel.Shop:
                OnSetupShop();
                break;

            case MainUIManager.EPanel.Play:
                OnSetupPlay();
                break;

            case MainUIManager.EPanel.Settings:
                OnSetupSettings();
                break;
        }
    }

    private void OnSetupMain()
    {
        _mainUIManager.Main.Setup(SaveLoadManager.PlayerData.money);
    }

    private void OnSetupShop()
    {
        _mainUIManager.Shop.Setup(SaveLoadManager.PlayerData.money);

        if(_shopIndex == -1)
        {
            _shopIndex = SaveLoadManager.PlayerData.carIndex;
        }

        bool isBought = SaveLoadManager.PlayerData.carsInfo[_shopIndex].isBought;

        CarSetup setup = Resources.Load<CarSetup>($"Car-{_shopIndex + 1}");

        _mainUIManager.Shop.Setup(_shopIndex > 0, _shopIndex + 1 < CarSetup.CarsCount, isBought, setup.Price, _shopIndex == SaveLoadManager.PlayerData.carIndex);

        _carsController.Setup(_shopIndex);
    }

    private void OnSetupPlay()
    {
        _mainUIManager.Play.Setup(SaveLoadManager.PlayerData.money);
    }

    private void OnSetupSettings()
    {
        _mainUIManager.Settings.Setup(SaveLoadManager.PlayerData.sfxVolume, SaveLoadManager.PlayerData.musicVolume);
    }



    private void OnShopLeft()
    {
        if (_shopIndex > 0)
        {
            _shopIndex--;

            OnSetupShop();
        }
    }

    private void OnShopRight()
    {
        if(_shopIndex + 1 < CarSetup.CarsCount)
        {
            _shopIndex++;

            OnSetupShop();
        }
    }

    private void OnShopSelect()
    {
        if (SaveLoadManager.PlayerData.carsInfo[_shopIndex].isBought)
        {
            SaveLoadManager.PlayerData.carIndex = _shopIndex;

            SaveLoadManager.Save();

            OnSetupShop();
        }
    }

    private void OnShopBuy()
    {
        if (!SaveLoadManager.PlayerData.carsInfo[_shopIndex].isBought)
        {
            CarSetup setup = Resources.Load<CarSetup>($"Car-{_shopIndex + 1}");

            if (SaveLoadManager.PlayerData.money >= setup.Price)
            {
                SaveLoadManager.PlayerData.money -= setup.Price;

                SaveLoadManager.PlayerData.carsInfo[_shopIndex].isBought = true;

                SaveLoadManager.Save();

                OnSetupShop();
            }
        }
    }

    private void OnShopBack()
    {
        if (!SaveLoadManager.PlayerData.carsInfo[_shopIndex].isBought || 
            SaveLoadManager.PlayerData.carIndex != _shopIndex)
        {
            _shopIndex = SaveLoadManager.PlayerData.carIndex;

            _carsController.Setup(_shopIndex);
        }
    }

    private void OnSfxChanged(float volume)
    {
        SaveLoadManager.PlayerData.sfxVolume = volume;

        _soundController.SetupSFX(volume);
    }

    private void OnMusicChanged(float volume)
    {
        SaveLoadManager.PlayerData.musicVolume = volume;

        _soundController.SetupMusic(volume);
    }



    private void OnDestroy()
    {
        Multiplayer.StopBroadcastClient();

        DebugConsole.ClearEvents();

        StopAllCoroutines();
    }



    private IEnumerator CheckServerInfoStack()
    {
        while (true)
        {
            while (_stack.Count() > 0)
            {
                LocatedServerInfo located = _stack.Pop();

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
