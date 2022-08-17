using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    [SerializeField]
    private GameObject _car;

    [SerializeField]
    private GameObject _fl;

    [SerializeField]
    private GameObject _fr;

    [SerializeField]
    private GameObject _bl;

    [SerializeField]
    private GameObject _br;

    [SerializeField]
    private PoliceLights _lights;



    public GameObject Car => _car;

    public GameObject FrontLeftWheel => _fl;

    public GameObject FrontRightWheel => _fr;
    
    public GameObject BackLeftWheel => _bl;
    
    public GameObject BackRightWheel => _br;

    public PoliceLights PoliceLights => _lights;
}
