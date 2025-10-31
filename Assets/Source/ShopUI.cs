using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIPanel
{
    [SerializeField]
    private Text _money;

    [SerializeField]
    private Button _settings;

    [SerializeField]
    private Button _left;

    [SerializeField]
    private Button _right;

    [SerializeField]
    private GameObject _pricePanel;

    [SerializeField]
    private Text _price;

    [SerializeField]
    private Button _back;

    [SerializeField]
    private Button _buy;

    [SerializeField]
    private Button _select;



    public event Action OnSettings;

    public event Action OnLeft;

    public event Action OnRight;

    public event Action OnBack;

    public event Action OnBuy;

    public event Action OnSelect;



    private void Awake()
    {
        _settings.onClick.AddListener(() => OnSettings?.Invoke());

        _left.onClick.AddListener(() => OnLeft?.Invoke());

        _right.onClick.AddListener(() => OnRight?.Invoke());

        _back.onClick.AddListener(() => OnBack?.Invoke());

        _buy.onClick.AddListener(() => OnBuy?.Invoke());

        _select.onClick.AddListener(() => OnSelect?.Invoke());
    }



    public void Setup(int money)
    {
        _money.text = $"{money}";
    }

    public void Setup(bool canLeft, bool canRight, bool isBought, int price, bool isSelected)
    {
        _price.text = $"{price}";

        _pricePanel.SetActive(!isBought);

        _left.gameObject.SetActive(canLeft);

        _right.gameObject.SetActive(canRight);

        _buy.gameObject.SetActive(!isBought);

        _select.gameObject.SetActive(isBought && !isSelected);
    }



    private void OnDestroy()
    {
        _settings.onClick.RemoveAllListeners();

        _left.onClick.RemoveAllListeners();

        _right.onClick.RemoveAllListeners();

        _back.onClick.RemoveAllListeners();

        _buy.onClick.RemoveAllListeners();
    }
}
