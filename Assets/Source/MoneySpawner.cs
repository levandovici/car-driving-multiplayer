using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _moneyParent;

    [SerializeField]
    private Transform[] _moneyPoints;

    [SerializeField]
    private GameObject _moneyPrefab;



    private void Awake()
    {
        Spawn();
    }

    private void Update()
    {
        if(_moneyParent.childCount <= 0)
        {
            Spawn();
        }
    }



    private void Spawn()
    {
        for (int i = 0; i < _moneyPoints.Length; i++)
        {
            GameObject obj = Instantiate(_moneyPrefab, _moneyPoints[i].position + Vector3.up * 2f, Quaternion.identity, _moneyParent);
        }
    }
}
