using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using InGameCodeEditor;

public class SneakGame : MonoBehaviour
{
    public static SneakGame Instance{private set; get;}

    private AlphaJargon _AlphaJargon;
    public AlphaJargon AlphaJargon
    {
        get
        {
            if(!_AlphaJargon)
            {
                _AlphaJargon = gameObject.AddComponent<AlphaJargon>();
            }
            return _AlphaJargon;
        }
        set
        {
            Destroy(AlphaJargon);
            gameObject.AddComponent<AlphaJargon>();
        }
    }
    public CodeEditor CodeEditor;
    void OnEnable()
    {
        LevelState.onlevelChangeEvent += LevelStateChange;
        GameState.ongameStateChangeEvent += GameStateChange;
    }

    void OnDisable()
    {
        LevelState.onlevelChangeEvent -= LevelStateChange;
        GameState.ongameStateChangeEvent -= GameStateChange;
    }

    void LevelStateChange()
    {
        string filePath = "SneakGame.lua";
        StartCoroutine(GetLuaFile(filePath));
        CodeEditor.Text = LevelState.Instance[((int)LevelState.Instance.CurrLevelState)].FileData;
    }
    private IEnumerator GetLuaFile(string filePath)
    {
        yield return LoadLuaFile.GetLuaFile(filePath, HandleLuaFile);
    }

    private void HandleLuaFile(string text)
    {
        AlphaJargon.FileData = text;
        AlphaJargon.Set();
    }
    void GameStateChange()
    {
        if(AlphaJargon.CurrAJState == AJState.Set && GameState.Instance.CurrGameState == GameStates.InComputer)
        {
            AlphaJargon.Run();
            // StartCoroutine(RunAlphaJargonScripts());
        }
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
