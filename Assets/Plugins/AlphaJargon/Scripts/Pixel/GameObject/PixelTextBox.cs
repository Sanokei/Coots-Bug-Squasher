using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using PixelGame;
using System;

[MoonSharp.Interpreter.MoonSharpUserData]
public class PixelTextBox : MonoBehaviour, IPixelObject
{
    public TextMeshProUGUI[,] Textbox = new TextMeshProUGUI[8,8];

    public TextMeshProUGUI InstantiateContent(PixelGameObject parent, string content, int x, int y)
    {
        TextMeshProUGUI box = Textbox[x,y];
        PixelTransform pixelTransform = parent.hasPixelComponent("Transform") ? parent.GetComponent<PixelTransform>() : parent.add("Transform","PixelTransform");
        
        x = y *= (int)PixelScreenManager.Instance[parent].CellSize;
        // y *= (int)PixelScreen.CellSize;

        box = gameObject.AddComponent<TextMeshProUGUI>();
        box.font = Resources.Load<TMP_FontAsset>("TextMeshPro/AprilSans");
        box.alignment = TextAlignmentOptions.Center;
        box.autoSizeTextContainer = true;
        
        pixelTransform.move(new PixelPosition(x,y));

        box.text = content;

        box.fontSize = 50;
        box.color = new Color(0,0,0);
        
        box.ForceMeshUpdate();

        return box;
    }
    
    
}
