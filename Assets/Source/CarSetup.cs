using UnityEngine;

[CreateAssetMenu(fileName = "CarSetup", menuName = "Scriptable Objects/CarSetup")]
public class CarSetup : ScriptableObject
{
    public const int CarsCount = 3;



    [SerializeField]
    private int _price = 0;

    [SerializeField]
    private GameObject _shopPrefab;

    [SerializeField]
    private GameObject _carPrefab;



    public int Price
    {
        get
        {
            return _price;
        }
    }

    public GameObject ShopPrefab
    {
        get
        {
            return _shopPrefab;
        }
    }

    public GameObject CarPrefab
    {
        get
        {
            return _carPrefab;
        }
    }
}
