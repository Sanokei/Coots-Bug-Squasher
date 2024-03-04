using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelGame;

// Physical Pixels
public class Pixel : MonoBehaviour, IPixelObject
{
    void OnEnable()
    {
        // localScale gets set to a seemingly random number otherwise.
        // refer to: https://forum.unity.com/threads/grid-layout-group-completely-ignores-canvas-scaler-solved.440520/
        this.transform.localScale = Vector3.one;
    }
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