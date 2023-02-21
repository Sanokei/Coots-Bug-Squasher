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
            AlphaJargon.Run();
        }
    }

    void OnEnable()
    {
        LevelState.levelChangeEvent += LevelStateChange;
    }

    void OnDisable()
    {
        LevelState.levelChangeEvent -= LevelStateChange;
    }

    void LevelStateChange()
    {
        string filePath = "SneakGame.lua";
        StartCoroutine(LoadLuaFile(filePath));

        CodeEditor.Text = LevelState.Instance[((int)LevelState.Instance.CurrLevelState)].FileData;
    }
}
