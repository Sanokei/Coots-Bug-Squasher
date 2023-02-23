using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[MoonSharp.Interpreter.MoonSharpUserData]
public class LevelState : MonoBehaviour
{
    public delegate void onLevelChange();
    public static event onLevelChange onlevelChangeEvent;

    public List<Level> Levels = new List<Level>();
    public Level this[int index]
    {
        get
        {
            return Levels[index];
        }
        private set{}
    }
    public static LevelState Instance{get; private set;}
    private LevelStates _CurrLevelState;
    public LevelStates CurrLevelState
    {
        get
        {
            return _CurrLevelState;
        }
        set
        {
            _CurrLevelState = value;
            onlevelChangeEvent?.Invoke();
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
