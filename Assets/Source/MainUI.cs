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

    [SerializeField]
    private Button _pirvacy_policy;



    public event Action OnClickSingleplayer;

    public event Action OnClickMultiplayer;



    private void Awake()
    {
        _singleplayer.onClick.AddListener(() => OnClickSingleplayer.Invoke());

        _multiplayer.onClick.AddListener(() => OnClickMultiplayer.Invoke());

        _pirvacy_policy.onClick.AddListener(() => Application.OpenURL("https://limonado.nikitalnc.com/legal/car-driving-multiplayer/"));
    }



    private void OnDestroy()
    {
        _singleplayer.onClick.RemoveAllListeners();

        _multiplayer.onClick.RemoveAllListeners();

        _pirvacy_policy.onClick.RemoveAllListeners();
    }
}
