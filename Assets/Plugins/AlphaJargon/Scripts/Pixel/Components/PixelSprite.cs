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
        [TextArea(20,20)] // arbatrary number
        public string SpriteString;
        public PixelSprite add(Table dynValue)
        {
            return add(dynValue.ToString());
        }
        public PixelSprite add(string SpriteString)
        {
            SpriteString = new string(SpriteString.Where(c => !char.IsWhiteSpace(c)).ToArray());
            if(SpriteString != "")
            {
                this.SpriteString = SpriteString;
                sprite.ConvertSpriteStringToScreen(SpriteString);
            }
            else
            {
                this.SpriteString = String.Concat(Enumerable.Repeat("o", sprite.grid.Count));
            }
            return this;
        }

        public override void Create(PixelGameObject parent)
        {
            // FIXME: add to PixelGameObject instead as "Child"
            sprite = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
        }
    }
}