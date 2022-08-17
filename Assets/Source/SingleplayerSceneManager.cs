using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleplayerSceneManager : GameSceneManager
{
    private new void Awake()
    {
        base.Awake();
            
        _gameUIManager.GameUI.OnExit += () => SceneManager.LoadScene(0);
    }
}
