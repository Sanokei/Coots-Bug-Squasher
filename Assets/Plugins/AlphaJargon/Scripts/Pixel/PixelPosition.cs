using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelPosition
    {
        public int x;
        public int y;

        public PixelPosition()
        {
            x = 0;
            y = 0;
        }

        public PixelPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public PixelPosition(Vector2Int pos)
        {
            this.x = pos.x;
            this.y = pos.y;
        }
        public static PixelPosition FromIndex(int pixelPosition)
        {
            return new PixelPosition(pixelPosition % PixelScreen.GridSideSize, pixelPosition / PixelScreen.GridSideSize);
        }
        public static PixelPosition operator +(PixelPosition a, PixelPosition b) => new PixelPosition(a.x + b.x, a.y + b.y);
        public static PixelPosition operator +(PixelPosition a, int b) => new PixelPosition(a.x + b % PixelScreen.GridSideSize, a.y + b / PixelScreen.GridSideSize);
        public static PixelPosition operator +(int b, PixelPosition a) => new PixelPosition(a.x + b % PixelScreen.GridSideSize, a.y + b / PixelScreen.GridSideSize);
        public static bool operator ==(PixelPosition a, PixelPosition b)
        {
            if (a.x == b.x && a.y == b.y) 
                return true;
            if (ReferenceEquals(a, null)) 
                return false;
            if (ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }
        public static bool operator !=(PixelPosition a, PixelPosition b) => !(a == b);
    }
}