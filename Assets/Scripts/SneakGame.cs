using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Networking;
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
    IEnumerator LoadLuaFile(string filePath)
    {
        string fileUrl = System.IO.Path.Combine(Application.streamingAssetsPath, filePath);
        UnityWebRequest webRequest = UnityWebRequest.Get(fileUrl);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading lua file: " + webRequest.error);
        }
        else
        {
            AlphaJargon.FileData = webRequest.downloadHandler.text;
            AlphaJargon.Set();
        }
    }

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
        StartCoroutine(LoadLuaFile(filePath));
        CodeEditor.Text = LevelState.Instance[((int)LevelState.Instance.CurrLevelState)].FileData;
    }

    void GameStateChange()
    {
        if(AlphaJargon.CurrAJState == AJState.Set && GameState.Instance.CurrGameState == GameStates.InComputer)
        {
            AlphaJargon.Run();
            StartCoroutine(RunAJScripts());
        }
    }

    IEnumerator RunAJScripts()
    {
        yield return new WaitForEndOfFrame();
        try
        {
            foreach(PixelGame.PixelGameObject pgo in AlphaJargon.PixelGameObjects.Values.OfType<PixelGame.PixelGameObject>().ToArray())
                foreach(PixelGame.PixelBehaviourScript scripts in pgo.PixelComponents.Values.OfType<PixelGame.PixelBehaviourScript>().ToArray())
                    scripts.RunScript();
        }
        catch
        {

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
