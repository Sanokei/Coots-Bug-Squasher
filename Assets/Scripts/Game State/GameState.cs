using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance{get; private set;}
    public GameStates CurrGameState;
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        CurrGameState = GameStates.PreStart;
    }
}

public enum GameStates
{
    PreStart,
    // Start, // On start playing
    Playing,
    NearComputer,
    InComputer,
    End
}
