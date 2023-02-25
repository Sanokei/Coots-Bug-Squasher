using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using InGameCodeEditor;

using PixelGame;
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
        PixelCollider.onTriggerEvent += ColliderTrigger;
        // FIXME
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
        PrivateCodeEditor.Text = "";
        string Level_filePath = LevelState.Instance.CurrLevel + ".lua";
        StartCoroutine(GetLuaFile(Level_filePath, HandleLuaFile));

        // CodeEditor.Text = LevelState.Instance[((int)LevelState.Instance.CurrLevelState)].FileData;
        string Level_MyCodeEditor_filePath = LevelState.Instance.CurrLevel + "MCE.lua";
        StartCoroutine(GetLuaFile(Level_MyCodeEditor_filePath, luaFileContent => { MyCodeEditor.Text = luaFileContent; }));
        foreach (KeyValuePair<int, string> Level_PrivateCodeEditor_filePath in (LevelState.Instance.PCE_Files.Where(x => x.Key == LevelState.Instance.CurrLevel)))
            StartCoroutine(GetLuaFile(Level_PrivateCodeEditor_filePath.Value, luaFileContent => { PrivateCodeEditor.Text += $"\n-- {Level_PrivateCodeEditor_filePath.Value}\n" + luaFileContent; }));
    }
    private IEnumerator GetLuaFile(string filePath, System.Action<string> callback)
    {
        yield return LoadLuaFile.GetLuaFile(filePath, callback);
    }

    private void HandleLuaFile(string text)
    {
        AlphaJargon.FileData = text;
        AlphaJargon.Set();
        AlphaJargon.Run();
    }
    void GameStateChange()
    {
        if(AlphaJargon.CurrAJState == AJState.Set && GameState.Instance.CurrGameState == GameStates.InComputer)
        {
            AlphaJargon.Run();
            // StartCoroutine(RunAlphaJargonScripts());
        }
    }

    void ColliderTrigger(Pixel other, PixelGameObject pgo)
    {
        foreach(KeyValuePair<PixelPosition, Pixel> pixels in PixelScreenManager.Instance.GetPixelsWithColliderOtherThan(pgo))
        {
            if(pixels.Value.Equals(other))
            {

            }
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
