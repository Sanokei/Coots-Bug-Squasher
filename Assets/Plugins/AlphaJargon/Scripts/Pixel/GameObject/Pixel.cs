using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelGame;

// Physical Pixels
public class Pixel : MonoBehaviour, IPixelObject
{
    public bool isOn
    {
        get
        {
            return Image.color.a != 0f;
        }
        set
        {
            Color C = Image.color;
            C.a = value ? 255f : 0f; 
            Image.color = C;
        }
    }
    public bool isWin
    {
        get
        {
            return Image.color.Equals(PixelSprite.RGBToColor(1164219232255));
        }
        set
        {
            Color C = Image.color;
            C = value ? PixelSprite.RGBToColor(1164219232255) : PixelSprite.RGBToColor(1000000000000); 
            Image.color = C;
        }
    }
    
    public Image Image;
    public PixelCollider Collider;
    public PixelSprite Sprite;
}