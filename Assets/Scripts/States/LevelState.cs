using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingBlocks.DataTypes;
using System;

[MoonSharp.Interpreter.MoonSharpUserData]
public class LevelState : MonoBehaviour
{

    /*

        RAN OUT OF TIME FIXME

    */

    public delegate void onLevelChange();
    public static event onLevelChange onlevelChangeEvent;

    // public List<Level> Levels = new List<Level>();
    // public Level this[int index]
    // {
    //     get
    //     {
    //         return Levels[index];
    //     }
    //     private set{}
    // }
    public static LevelState Instance{get; private set;}
    private int _CurrLevelState;
    public int CurrLevel
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
    public InspectableDictionary<int,string> PCE_Files;
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
        // CurrLevelState = LevelStates.Tutorial;
        CurrLevel = 0;
    }
}

// public enum LevelStates
// {
//     Tutorial,
//     Level_1 //come up with betternames later
// }
