using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using PixelGame.Component;

// Physical Pixels
namespace PixelGame.Object
{
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
        
        public Image Image;
        public PixelCollider Collider;
        public PixelSprite Sprite;
    }
}