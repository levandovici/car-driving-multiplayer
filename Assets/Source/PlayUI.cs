using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayUI : UIPanel
{
    [SerializeField]
    private Text _money;

    [SerializeField]
    private Button _settings;

    [SerializeField]
    private Button _back;

    [SerializeField]
    private Button _singleplayer;

    [SerializeField]
    private Button _multiplayer;



    public event Action OnSettings;

    public event Action OnBack;

    public event Action OnSingleplayer;

    public event Action OnMultiplayer;



    private void Awake()
    {
        _settings.onClick.AddListener(() => OnSettings?.Invoke());

        _back.onClick.AddListener(() => OnBack?.Invoke());

        _singleplayer.onClick.AddListener(() => OnSingleplayer.Invoke());

        _multiplayer.onClick.AddListener(() => OnMultiplayer.Invoke());
    }




    public void Setup(int money)
    {
        _money.text = $"{money}";
    }



    private void OnDestroy()
    {
        _settings.onClick.RemoveAllListeners();

        _back.onClick.RemoveAllListeners();

        _singleplayer.onClick.RemoveAllListeners();

        _multiplayer.onClick.RemoveAllListeners();
    }
}
