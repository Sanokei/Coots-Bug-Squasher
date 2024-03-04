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
    private GameObject AJGO;
    public AlphaJargon AlphaJargon
    {
        get
        {
            if(!AlphaJargon.Instance)
            {
                AJGO = new GameObject("AlphaJargon");
                AJGO.transform.parent = transform;
                AJGO.transform.localPosition = Vector3.zero;
                AlphaJargon.Instance = AJGO.AddComponent<AlphaJargon>();
            }
            return AlphaJargon.Instance;
        }
    }
    // UI STUFF
    public CodeEditor MyCodeEditor;
    public CodeEditor PrivateCodeEditor;
    void OnEnable()
    {
        LevelState.onlevelChangeEvent += LevelStateChange;
        PixelTransform.OnWinLevelEvent += WinLevel;
    }

    void OnDisable()
    {
        LevelState.onlevelChangeEvent -= LevelStateChange;
        PixelTransform.OnWinLevelEvent -= WinLevel;
    }
    
    void WinLevel()
    {
        Destroy(AJGO);
        LevelState.Instance.CurrLevel += 1;
    }
    void LevelStateChange()
    { // FIXME THIS IS AWFUL
        PrivateCodeEditor.Text = "-- Scroll to see more\n";
        string Level_filePath = LevelState.Instance.CurrLevel + ".lua";
        StartCoroutine(IPixelGame.GetLuaFile(Level_filePath, luaFileContent => { AlphaJargon.FileData = luaFileContent; }));
        
        // CodeEditor.Text = LevelState.Instance[((int)LevelState.Instance.CurrLevelState)].FileData;
        string Level_MyCodeEditor_filePath = LevelState.Instance.CurrLevel + "MCE.lua";
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

    // for button
    public void RunSelfCode()
    {
        IPixelGame.RunSelfCode(MyCodeEditor.Text);
    }
}
