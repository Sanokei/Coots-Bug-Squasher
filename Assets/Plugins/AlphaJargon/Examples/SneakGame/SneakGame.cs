using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using InGameCodeEditor;

using PixelGame;
using MoonSharp.Interpreter;
[MoonSharp.Interpreter.MoonSharpUserData]
public class SneakGame : MonoBehaviour, IPixelGame
{
    // UI STUFF
    public CodeEditor MyCodeEditor;
    public CodeEditor PrivateCodeEditor;
    public AlphaJargon AlphaJargon{get;private set;}
    void Awake()
    {
        AlphaJargon = AlphaJargon.Instance.CreateInstance(gameObject);
    }
    void OnEnable()
    {
        LevelState.onlevelChangeEvent += LevelStateChange;
        PixelEvent.onEvent += OnPixelGameEvent;
    }

    void OnDisable()
    {
        LevelState.onlevelChangeEvent -= LevelStateChange;
        PixelEvent.onEvent -= OnPixelGameEvent;
    }
    
    void WinLevel()
    {
        Destroy(AlphaJargon.Instance.gameObject);
        AlphaJargon = AlphaJargon.Instance.CreateInstance(gameObject);
        LevelState.Instance.CurrLevel += 1;
    }
    void LevelStateChange()
    { 
        // FIXME THIS IS AWFUL
        // the way i am storing levels and such is so bad.

        string Level_filePath = LevelState.Instance.CurrLevel + ".lua";
        string Level_MyCodeEditor_filePath = LevelState.Instance.CurrLevel + "MCE.lua";

        PrivateCodeEditor.Text = "-- Scroll to see more\n";
        

        StartCoroutine(IPixelGame.GetLuaFile(Level_filePath, luaFileContent => { AlphaJargon.FileData = luaFileContent; }));
        StartCoroutine(IPixelGame.GetLuaFile(Level_MyCodeEditor_filePath, luaFileContent => { MyCodeEditor.Text = luaFileContent; }));

        // LINQ is sometimes impossible to freaking read man 
        // foreach (KeyValuePair<int, string> Level_PrivateCodeEditor_filePath in (LevelState.Instance.PCE_Files.Where(x => x.Key == LevelState.Instance.CurrLevel).ToList()))
        //     StartCoroutine(GetLuaFile(Level_PrivateCodeEditor_filePath.Value, luaFileContent => { PrivateCodeEditor.Text += $"-- {Level_PrivateCodeEditor_filePath.Value}\n" + luaFileContent + "\n"; }));
        // Get the PCE_Files that match the current level.

        List<KeyValuePair<int, string>> matchingFiles = new List<KeyValuePair<int, string>>();
        foreach (KeyValuePair<int, string> file in LevelState.Instance.PCE_Files)
        {
            if (file.Key == LevelState.Instance.CurrLevel)
            {
                matchingFiles.Add(file);
            }
        }

        // Iterate over the matching files and start a coroutine for each one.
        foreach (KeyValuePair<int, string> file in matchingFiles)
        {
            string filePath = file.Value;
            StartCoroutine(IPixelGame.GetLuaFile(filePath, luaFileContent =>
            {
                PrivateCodeEditor.Text = PrivateCodeEditor.Text + $"-- {filePath}\n" + luaFileContent + "\n";
            }));
        }

    }

    public void OnPixelGameEvent(string Name,params string[] args)
    {
        switch(Name)
        {
            case "win":
                WinLevel();
            break;
        }
    }

    // for the run button
    public void RunSelfCode()
    {
        AlphaJargon.CodeEditor.RunScript(MyCodeEditor.Text);
    }
}
