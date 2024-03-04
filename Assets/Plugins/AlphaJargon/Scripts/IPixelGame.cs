using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPixelGame
{
    public AlphaJargon AlphaJargon{get;}

    public static IEnumerator GetLuaFile(string filePath, System.Action<string> callback)
    {
        yield return LoadLuaFile.GetLuaFile(filePath, callback);
    }

    internal static void RunSelfCode(string script)
    {
        AlphaJargon.Instance.CodeEditor.RunScript(script);
    }
}
