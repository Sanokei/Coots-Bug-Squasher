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
    void Update()
    {
        if(GameState.Instance.CurrGameState == GameStates.InComputer && !_SeenControlIndicator)
        {
            Canvas.SetActive(true);
            _SeenControlIndicator = true;
        }
    }
}
