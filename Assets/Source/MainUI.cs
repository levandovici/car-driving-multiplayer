using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainUI : UIPanel
{
    [SerializeField]
    private Button _singleplayer;

    [SerializeField]
    private Button _multiplayer;



    public event Action OnClickSingleplayer;

    public event Action OnClickMultiplayer;



    private void Awake()
    {
        _singleplayer.onClick.AddListener(() => OnClickSingleplayer.Invoke());

        _multiplayer.onClick.AddListener(() => OnClickMultiplayer.Invoke());
    }



    private void OnDestroy()
    {
        _singleplayer.onClick.RemoveAllListeners();

        _multiplayer.onClick.RemoveAllListeners();
    }
}
