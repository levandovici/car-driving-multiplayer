using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameUI : UIPanel
{
    [SerializeField]
    private Button _resetCar;

    [SerializeField]
    private Button _camera;

    [SerializeField]
    private Button _siren;

    [SerializeField]
    private Button _exit;



    [SerializeField]
    public UIButton Nitro;

    [SerializeField]
    public UIButton Brake;

    [SerializeField]
    public UIButton Forward;

    [SerializeField]
    public UIButton Backward;

    [SerializeField]
    public UIButton Left;

    [SerializeField]
    public UIButton Right;



    public event Action OnResetCar;

    public event Action OnChangeCameraView;

    public event Action OnSiren;

    public event Action OnExit;



    private void Awake()
    {
        _resetCar.onClick.AddListener(() => OnResetCar.Invoke());
        
        _camera.onClick.AddListener(() => OnChangeCameraView.Invoke());
        
        _siren.onClick.AddListener(() => OnSiren.Invoke());

        _exit.onClick.AddListener(() => OnExit.Invoke());
    }



    private void OnDestroy()
    {
        _resetCar.onClick.RemoveAllListeners();

        _camera.onClick.RemoveAllListeners();

        _siren.onClick.RemoveAllListeners();

        _exit.onClick.RemoveAllListeners();
    }
}