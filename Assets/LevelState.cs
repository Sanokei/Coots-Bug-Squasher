using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : MonoBehaviour
{
    public static LevelState Instance{get; private set;}
    public LevelStates CurrLevelState;
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
        CurrLevelState = LevelStates.PreStart;
    }
}

public enum LevelStates
{
    PreStart,
    // Start, // On start playing
    Playing,
    NearComputer,
    InComputer,
    Played,
    Finish
}
