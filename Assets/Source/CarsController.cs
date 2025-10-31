using UnityEngine;

public class CarsController : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private GameObject _car = null;

    [SerializeField]
    private int _index = -1;



    public int Index
    {
        get
        {
            return _index;
        }

        set
        {
            _index = value;
        }
    }



    public void Setup(int index)
    {
        if(_index != index)
        {
            _index = index;

            if(_car != null)
            {
                Destroy(_car.gameObject);
            }

            CarSetup setup = Resources.Load<CarSetup>($"Car-{index + 1}");

            _car = Instantiate(setup.ShopPrefab, Vector3.zero, _parent.rotation, _parent);
        }
    }
}
