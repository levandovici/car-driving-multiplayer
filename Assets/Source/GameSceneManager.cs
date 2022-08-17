using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameSceneManager : MonoBehaviour
{
    [SerializeField]
    protected GameUIManager _gameUIManager;

    [SerializeField]
    protected Car _car;



    protected void Awake()
    {
        _gameUIManager.GameUI.OnResetCar += () => _car.VehicleCamera.RestCar();

        _gameUIManager.GameUI.OnChangeCameraView += () => _car.VehicleCamera.CameraSwitch();

        _gameUIManager.GameUI.OnSiren += () => _car.VehicleCamera.PoliceLightSwitch();

        _gameUIManager.GameUI.OnExit += () => Debug.Log("OnExit");



        _gameUIManager.GameUI.Forward.OnPointerEnterEvent += () => _car.VehicleCamera.CarAccelForward(1f);

        _gameUIManager.GameUI.Forward.OnPointerDownEvent += () => _car.VehicleCamera.CarAccelForward(1f);

        _gameUIManager.GameUI.Forward.OnPointerExitEvent += () => _car.VehicleCamera.CarAccelForward(0f);

        _gameUIManager.GameUI.Forward.OnPointerUpEvent += () => _car.VehicleCamera.CarAccelForward(0f);


        _gameUIManager.GameUI.Backward.OnPointerEnterEvent += () => _car.VehicleCamera.CarAccelBack(-1f);

        _gameUIManager.GameUI.Backward.OnPointerDownEvent += () => _car.VehicleCamera.CarAccelBack(-1f);

        _gameUIManager.GameUI.Backward.OnPointerExitEvent += () => _car.VehicleCamera.CarAccelBack(0f);

        _gameUIManager.GameUI.Backward.OnPointerUpEvent += () => _car.VehicleCamera.CarAccelBack(0f);


        _gameUIManager.GameUI.Brake.OnPointerEnterEvent += () => _car.VehicleCamera.CarHandBrake(true);

        _gameUIManager.GameUI.Brake.OnPointerDownEvent += () => _car.VehicleCamera.CarHandBrake(true);

        _gameUIManager.GameUI.Brake.OnPointerExitEvent += () => _car.VehicleCamera.CarHandBrake(false);

        _gameUIManager.GameUI.Brake.OnPointerUpEvent += () => _car.VehicleCamera.CarHandBrake(false);


        _gameUIManager.GameUI.Left.OnPointerEnterEvent += () => _car.VehicleCamera.CarSteer(-1f);

        _gameUIManager.GameUI.Left.OnPointerDownEvent += () => _car.VehicleCamera.CarSteer(-1f);

        _gameUIManager.GameUI.Left.OnPointerExitEvent += () => _car.VehicleCamera.CarSteer(0f);

        _gameUIManager.GameUI.Left.OnPointerUpEvent += () => _car.VehicleCamera.CarSteer(0f);


        _gameUIManager.GameUI.Right.OnPointerEnterEvent += () => _car.VehicleCamera.CarSteer(1f);

        _gameUIManager.GameUI.Right.OnPointerDownEvent += () => _car.VehicleCamera.CarSteer(1f);

        _gameUIManager.GameUI.Right.OnPointerExitEvent += () => _car.VehicleCamera.CarSteer(0f);

        _gameUIManager.GameUI.Right.OnPointerUpEvent += () => _car.VehicleCamera.CarSteer(0f);


        _gameUIManager.GameUI.Nitro.OnPointerEnterEvent += () => _car.VehicleCamera.CarShift(true);

        _gameUIManager.GameUI.Nitro.OnPointerDownEvent += () => _car.VehicleCamera.CarShift(true);

        _gameUIManager.GameUI.Nitro.OnPointerExitEvent += () => _car.VehicleCamera.CarShift(false);

        _gameUIManager.GameUI.Nitro.OnPointerUpEvent += () => _car.VehicleCamera.CarShift(false);
    }
}