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
    private int _CurrLevelState = 0;
    public int CurrLevel
    {
        get
        {
            return _CurrLevelState;
        }
        set
        {
            var temp = _CurrLevelState; // this is so the order of execution dont change.
            _CurrLevelState = value;
            if(temp != value)
                onlevelChangeEvent?.Invoke();
        }
    }
    [SerializeField] List<int> PCE_Files_Int;
    [SerializeField] List<string> PCE_Files_String;

    [HideInInspector] public List<KeyValuePair<int, string>> PCE_Files
    {
        get
        {
            List<KeyValuePair<int, string>> files = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < PCE_Files_Int.Count && i < PCE_Files_String.Count; i++)
            {
                files.Add(new KeyValuePair<int, string>(PCE_Files_Int[i], PCE_Files_String[i]));
            }
            return files;
        }
        set
        {
            PCE_Files_Int = new List<int>();
            PCE_Files_String = new List<string>();
            foreach (KeyValuePair<int, string> file in value)
            {
                PCE_Files_Int.Add(file.Key);
                PCE_Files_String.Add(file.Value);
            }
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
        ResetLevel();
    }
    public static void ResetLevel()
    {
        onlevelChangeEvent?.Invoke();
    }
}

// public enum LevelStates
// {
//     Tutorial,
//     Level_1 //come up with betternames later
// }
