using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InComputerBounds : MonoBehaviour
{
    [HideInInspector] public bool SeenControlIndicator;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coots"))
        {
            GameState.Instance.CurrGameState = GameStates.NearComputer;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coots"))
        {
            GameState.Instance.CurrGameState = GameStates.Playing;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && GameState.Instance.CurrGameState == GameStates.NearComputer)
        {
            SeenControlIndicator = true;
        }
    }
}
