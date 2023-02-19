using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameCodeEditor;
using System;

public class LevelState : MonoBehaviour
{
    public delegate void LevelChange();
    public static event LevelChange levelChangeEvent;
    
    public CodeEditor CodeEditor;
    public AlphaJargon AlphaJargon;

    public List<Level> Levels;
    public Level this[int index]
    {
        get
        {
            return Levels[index];
        }
        private set{}
    }
    public static LevelState Instance{get; private set;}
    public LevelStates CurrLevelState
    {
        get
        {
            return CurrLevelState;
        }
        set
        {
            CurrLevelState = value;
            levelChangeEvent?.Invoke();
        }
    }

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
        CurrLevelState = LevelStates.Tutorial;
    }
}

public enum LevelStates
{
    Tutorial,
    Level_1 //come up with betternames later
}
