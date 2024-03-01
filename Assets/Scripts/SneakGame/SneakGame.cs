using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using InGameCodeEditor;

using PixelGame;
[MoonSharp.Interpreter.MoonSharpUserData]
public class SneakGame : MonoBehaviour
{
    public static SneakGame Instance{private set; get;}
    private GameObject AJGO;
    private AlphaJargon _AlphaJargon;
    public AlphaJargon AlphaJargon
    {
        get
        {
            if(!_AlphaJargon)
            {
                AJGO = new GameObject("AlphaJargon");
                AJGO.transform.parent = transform;
                AJGO.transform.localPosition = new Vector3(0,0,0);
                _AlphaJargon = AJGO.AddComponent<AlphaJargon>();
            }
            return _AlphaJargon;
        }
        private set
        {
            // Destroy(AlphaJargon);
            // gameObject.AddComponent<AlphaJargon>();
        }
    }
    // UI STUFF
    public CodeEditor MyCodeEditor;
    public CodeEditor PrivateCodeEditor;

    void OnEnable()
    {
        LevelState.onlevelChangeEvent += LevelStateChange;
        GameState.ongameStateChangeEvent += GameStateChange;
        PixelTransform.OnWinLevelEvent += WinLevel;
    }

    void OnDisable()
    {
        LevelState.onlevelChangeEvent -= LevelStateChange;
        GameState.ongameStateChangeEvent -= GameStateChange;
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
        StartCoroutine(GetLuaFile(Level_filePath, HandleLuaFile));

        // CodeEditor.Text = LevelState.Instance[((int)LevelState.Instance.CurrLevelState)].FileData;
        string Level_MyCodeEditor_filePath = LevelState.Instance.CurrLevel + "MCE.lua";
        StartCoroutine(GetLuaFile(Level_MyCodeEditor_filePath, luaFileContent => { MyCodeEditor.Text = luaFileContent; }));

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
            StartCoroutine(GetLuaFile(filePath, luaFileContent =>
            {
                PrivateCodeEditor.Text = PrivateCodeEditor.Text + $"-- {filePath}\n" + luaFileContent + "\n";
            }));
        }

    }
    private IEnumerator GetLuaFile(string filePath, System.Action<string> callback)
    {
        yield return LoadLuaFile.GetLuaFile(filePath, callback);
    }

    private void HandleLuaFile(string text)
    {
        AlphaJargon.FileData = text;
        AlphaJargon.Ready();
        AlphaJargon.Set();
        AlphaJargon.Go();
    }
    void GameStateChange()
    {
        if(AlphaJargon.CurrAJState == AJState.Set && GameState.Instance.CurrGameState == GameStates.InComputer)
        {
            AlphaJargon.Set();
            // StartCoroutine(RunAlphaJargonScripts());
        }
    }

    public void RunSelfCode()
    {
        AlphaJargon.CodeEditor.RunScript(MyCodeEditor.Text);
    }

    // FIXME:
    // this bug is stupid and its whatever for now.
        // bug is that when pressing W to get into the computer the lua code also intakes W with the onKeyDown delegate
    // see: https://forum.unity.com/threads/do-not-use-waitforendofframe.883648/

    IEnumerator CreateRoomsForScript(string filePath)
    {
        yield return null;
    }
}
