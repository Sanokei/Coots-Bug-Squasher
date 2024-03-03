using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using MoonSharp.Interpreter;

using UnityEngine;
using UnityEngine.UI;

namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelSprite : PixelComponent
    {
        // Psuedo Sprite
        public PixelScreen screen; // Only use this to show on screen
        public override PixelGameObject parent{get;set;}
        void OnEnable()
        {
            AlphaJargon.startGameEvent += AddScreenToScreenManager;
        }
        void OnDisable()
        {
            AlphaJargon.startGameEvent -= AddScreenToScreenManager;
        }
        public override void Create(PixelGameObject parent)
        {
            this.parent = parent;
            screen = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
        }
        public void AddScreenToScreenManager()
        {
            PixelScreen.onPixelScreenCreateEvent?.Invoke(parent,screen);
        }
        public override void Remove()
        {
            PixelScreen.onPixelScreenDeleteEvent?.Invoke(parent,screen);
            Destroy(screen);
            Destroy(this);
        }
        public PixelSprite add(DynValue dynValue)
        {
            // This is all done because the written 
            /**
            [[
                0000
                0c00
                0000
            ]]
                will read from top to bottom, but will write to the dictonary bottom to top.
                This can be changed by the gridlayouts Start Corner.
                
                All its doing is spliting the  
            **/
            string inputString = new(dynValue.String
                .Where(c => !char.IsWhiteSpace(c) && c != '\"')
                .ToArray());
            
            var outputStrings = Enumerable
                .Range(0, inputString.Length / PixelScreen.GridSideSize)
                .Select(i => inputString.Substring(i * PixelScreen.GridSideSize, PixelScreen.GridSideSize));

            string[] stringArray = outputStrings.ToArray();
            
            Array.Reverse(stringArray); 
            StringBuilder sb = new StringBuilder();

            foreach (var str in stringArray)
            {
                sb.Append(str);
            }
            string reversedString = sb.ToString();

            // This mirrors each string slice so its commented out

            // char[] charArray = reversedString.ToCharArray();
            // Array.Reverse(charArray);
            // string finalString = new(charArray);

            return add(reversedString);
        }
        internal PixelSprite add(string SpriteString)
        {
            if(SpriteString != "")
            {
                AddSpriteStringToScreen(SpriteString);
            }
            else
            {
                // AddSpriteStringToScreen(String.Concat(Enumerable.Repeat("o", screen.grid.Count)));
                string oString = "";
                for (int i = 0; i < screen.grid.Count; i++)
                    oString += "o";
                AddSpriteStringToScreen(oString);
            }
            return this;
        }
        public PixelScreen AddSpriteStringToScreen(string SpriteString)
        {
            // Enumerable.Range(0, SpriteString.Length)
            //     .ToList()
            //     .ForEach(index => CharToPixel(screen.grid[index], SpriteString[index])
            // );

            for(int index = 0; index < SpriteString.Length; index++)
            {
                CharToPixel(screen.grid[index], SpriteString[index]);
            }
            return screen;
        }
        // Use pseudo signed bit of 1
        public void CharToPixel(Pixel pixel, char letter)
        {
            // localScale gets set to a seemingly random number otherwise.
            // refer to: https://forum.unity.com/threads/grid-layout-group-completely-ignores-canvas-scaler-solved.440520/
            pixel.transform.localScale = Vector3.one;

            if(imageChars.Contains(letter))
            {
                pixel.Image.sprite = image[Array.IndexOf(imageChars,letter)];
                pixel.Image.color = RGBToColor(1255255255255);
            }
            else if (System.Enum.TryParse<PixelColor>(letter.ToString().ToLower(), out PixelColor pixelColor))
            {
                pixel.Image.color = RGBToColor((long)pixelColor);
            }
            pixel.Sprite = this;
        }

        char[] imageChars = {'c','w','d','f','u','m'};
        List<Sprite> image = new List<Sprite>();
        void Awake()
        {
            //                                                           'c',                                'w',                                    'd',                                     'f',                                     'u',                                      'm'
            image.AddRange(new List<Sprite>{Resources.Load<Sprite>("Art/Coots"), Resources.Load<Sprite>("Art/Wall"), Resources.Load<Sprite>("Art/Door_Closed"), Resources.Load<Sprite>("Art/Door_Open"), Resources.Load<Sprite>("Art/Camera_On"), Resources.Load<Sprite>("Art/Camera_Off")}); 
        }
        // cannot for the life of me tell you why i did it this way, i forgor
        public static Color RGBToColor(long rgb)
        {  
            //    r   g   b
            // 1 000 000 000
            byte r = byte.Parse(rgb.ToString().Substring(1,3), System.Globalization.NumberStyles.Integer);
            byte g = byte.Parse(rgb.ToString().Substring(4,3), System.Globalization.NumberStyles.Integer);
            byte b = byte.Parse(rgb.ToString().Substring(7,3), System.Globalization.NumberStyles.Integer);
            byte a = byte.Parse(rgb.ToString().Substring(10,3), System.Globalization.NumberStyles.Integer);
            return new Color32(r,g,b,a);
        }
    }
}

public enum PixelColor : long
{
    o = 1000000000000,
    p = 1255160122255,
    y = 1255255000255,
    b = 1164219232255
}