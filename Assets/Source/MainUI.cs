using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainUI : UIPanel
{
    [SerializeField]
    private Text _money;

    [SerializeField]
    private Button _settings;

    [SerializeField]
    private Button _shop;

    [SerializeField]
    private Button _play;



    public event Action OnSettings;

    public event Action OnShop;

    public event Action OnPlay;



    private void Awake()
    {
        _settings.onClick.AddListener(() => OnSettings?.Invoke());

        _shop.onClick.AddListener(() => OnShop?.Invoke());

        _play.onClick.AddListener(() => OnPlay?.Invoke());
    }



    public void Setup(int money)
    {
        _money.text = $"{money}";
    }



    private void OnDestroy()
    {
        _settings.onClick.RemoveAllListeners();

        _shop.onClick.RemoveAllListeners();

        _play.onClick.RemoveAllListeners();
    }
}
