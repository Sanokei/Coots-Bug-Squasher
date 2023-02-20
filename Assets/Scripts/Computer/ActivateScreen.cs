using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScreen : MonoBehaviour
{
    private bool _SeenControlIndicator = false;
    public GameObject Canvas;
    void Awake()
    {
        Canvas.SetActive(false);
    }
    void OnEnable()
    {
        GameState.gameStateChangeEvent += GameStateChange;
    }
    void OnDisable()
    {
        GameState.gameStateChangeEvent -= GameStateChange;
    }
    void GameStateChange()
    {
        if(GameState.Instance.CurrGameState == GameStates.InComputer && !_SeenControlIndicator)
        {
            Canvas.SetActive(true);
            _SeenControlIndicator = true;
        }
    }
}
