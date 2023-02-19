using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCootsMove : MonoBehaviour
{
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0 && GameState.Instance.CurrGameState == GameStates.PreStart)
        {
            GameState.Instance.CurrGameState = GameStates.Playing;
        }
    }
}
