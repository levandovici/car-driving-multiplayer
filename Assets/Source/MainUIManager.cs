using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private MainUI _main;

    [SerializeField]
    private ShopUI _shop;

    [SerializeField]
    private PlayUI _play;

    [SerializeField]
    private SettingsUI _settings;


    private EPanel _current = EPanel.None;



    public event Action<EPanel> OnChangePanel;



    public EPanel Current => _current;

    public MainUI Main => _main;

    public ShopUI Shop => _shop;

    public PlayUI Play => _play;

    public SettingsUI Settings => _settings;



    private void Awake()
    {
        Main.OnSettings += () => Change(EPanel.Settings);

        Main.OnShop += () => Change(EPanel.Shop);

        Main.OnPlay += () => Change(EPanel.Play);


        Shop.OnSettings += () => Change(EPanel.Settings);

        Shop.OnBack += () => Change(EPanel.Main);


        Play.OnSettings += () => Change(EPanel.Settings);

        Play.OnBack += () => Change(EPanel.Main);


        Settings.OnBack += () => Change(EPanel.Main);


        Change(EPanel.Main);
    }



    private void Change(EPanel panel)
    {
        Hide(_current);

        Show(panel);

        _current = panel;

        OnChangePanel?.Invoke(panel);
    }


    private void Show(EPanel panel)
    {
        switch (panel)
        {
            case EPanel.Main:
                Main.Show();
                break;

            case EPanel.Shop:
                Shop.Show();
                break;

            case EPanel.Play:
                Play.Show();
                break;

            case EPanel.Settings:
                Settings.Show();
                break;
        }
    }

    private void Hide(EPanel panel)
    {
        switch(panel)
        {
            case EPanel.Main:
                Main.Hide();
                break;

            case EPanel.Shop:
                Shop.Hide();
                break;

            case EPanel.Play:
                Play.Hide();
                break;

            case EPanel.Settings:
                Settings.Hide();
                break;
        }
    }



    public enum EPanel
    {
        None, Main, Shop, Play, Settings
    }
}
