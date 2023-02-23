using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using InGameCodeEditor;

public class SneakGame : MonoBehaviour
{
    private AlphaJargon CurrAJ;
    public AlphaJargon AlphaJargon
    {
        get
        {
            if(!CurrAJ)
            {
                CurrAJ = gameObject.AddComponent<AlphaJargon>();
            }
            return CurrAJ;
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
        LevelState.levelChangeEvent += LevelStateChange;
        GameState.gameStateChangeEvent += GameStateChange;
    }

    void OnDisable()
    {
        LevelState.levelChangeEvent -= LevelStateChange;
        GameState.gameStateChangeEvent -= GameStateChange;
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
            foreach(PixelGame.PixelGameObject pgo in AlphaJargon.PixelGameObjects.Values)
                foreach(PixelGame.PixelBehaviourScript scripts in pgo.PixelComponents.Values)
                    scripts.RunScript();
        }
        catch
        {

        }
    }
}
