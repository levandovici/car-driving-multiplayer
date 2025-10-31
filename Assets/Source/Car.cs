using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private GameObject _car;

    [SerializeField]
    private Transform _wheel_fl;

    [SerializeField]
    private Transform _wheel_fr;

    [SerializeField]
    private Transform _wheel_bl;

    [SerializeField]
    private Transform _wheel_br;

    [SerializeField]
    private VehicleCamera _vehicleCamera;

    [SerializeField]
    private VehicleControl _vehicleControl;

    [SerializeField]
    private PoliceLights _policeLights;



    public Transform CarTransform => _car.transform;

    public Transform FL => _wheel_fl;

    public Transform FR => _wheel_fr;

    public Transform BL => _wheel_bl;

    public Transform BR => _wheel_br;

    public VehicleCamera VehicleCamera => _vehicleCamera;

    public VehicleControl VehicleControl => _vehicleControl;

    public PoliceLights PoliceLights => _policeLights;



    public void Setup(GameObject car, VehicleControl control, PoliceLights lights = null)
    {
        _car = car;

        _vehicleControl = control;

        _policeLights = lights;


        _vehicleCamera.target = car.transform;
    }

    public void Setup(Transform FL, Transform FR, Transform BL, Transform BR)
    {
        _wheel_fl = FL;

        _wheel_fr = FR;

        _wheel_bl = BL;

        _wheel_br = BR;
    }
}
