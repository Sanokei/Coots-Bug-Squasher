using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using MoonSharp.Interpreter;

using UnityEngine;

namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelSprite : PixelComponent
    {
        // Psuedo Sprite
        public PixelScreen sprite; // Only use this to show on screen
        public PixelSprite add(DynValue dynValue)
        {
            string SpriteString = dynValue.ToString();
            SpriteString = new string(SpriteString.Where(c => !char.IsWhiteSpace(c)).ToArray()).Replace("\"","");   
            return add(SpriteString);
        }
        public PixelSprite add(string SpriteString)
        {
            if(SpriteString != "")
            {
                sprite.ConvertSpriteStringToScreen(SpriteString);
            }
            else
            {
                sprite.ConvertSpriteStringToScreen(String.Concat(Enumerable.Repeat("o", sprite.grid.Count)));
            }
            return this;
        }

        public override void Create(PixelGameObject parent)
        {
            sprite = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
        }
    }
}