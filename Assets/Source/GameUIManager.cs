using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private GameUI _gameUI;



    public GameUI GameUI => _gameUI;
}
