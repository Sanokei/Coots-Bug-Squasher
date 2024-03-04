using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelGame
{
    public interface IPixelGame
    {
        public AlphaJargon AlphaJargon{get;}

        public static IEnumerator GetLuaFile(string filePath, System.Action<string> callback)
        {
            yield return LoadLuaFile.GetLuaFile(filePath, callback);
        }
    }
}