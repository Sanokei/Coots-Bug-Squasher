using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScreen : MonoBehaviour
{
    private bool _SeenControlIndicator = false;
    public Canvas Canvas;
    void Awake()
    {
        Canvas.renderMode = RenderMode.WorldSpace;
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
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            _SeenControlIndicator = true;
        }
    }
}
