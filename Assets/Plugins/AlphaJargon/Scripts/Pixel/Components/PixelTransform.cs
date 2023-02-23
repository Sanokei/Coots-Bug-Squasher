using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Habrador_Computational_Geometry;

namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelTransform : PixelComponent
    {
        PixelPosition position;
        PixelGameObject parent;

        public override void Create(PixelGameObject parent)
        {
            position = new PixelPosition(0,0);
            this.parent = parent;
        }

        public PixelTransform add(PixelPosition pp /*hehe*/)
        {
            position = pp;
            move(pp);
            return this;
        }

        public PixelTransform add(int x, int y)
        {
            return add(new PixelPosition(x,y));
        }

        public PixelPosition move(PixelPosition pixelPosition)
        {
            return move(pixelPosition.x, pixelPosition.y);
        }

        public PixelPosition move(int x, int y)
        {
            // FIXME:
            // funky stuff happens when I try to make this 
            // gameobject.transform.Translate
            // no idea why
            Vector3 trans = new Vector3(x * PixelScreen.CellSize,y * PixelScreen.CellSize);
            if(!CheckCollision(trans))
                transform.Translate(trans);
            position = new PixelPosition((int)(transform.position.x / PixelScreen.CellSize),(int)(transform.position.y / PixelScreen.CellSize));
            return position;
        }


        // check all other pixelColliders in jargon and see if they match
        // if its itself then dont count it
        private bool CheckCollision(Vector3 translation)
        {
            foreach(PixelCollider pgo in parent.PixelComponents.Values.OfType<PixelCollider>().ToArray())
                foreach(PolygonCollider2D poly in pgo.pixelCollider)
                    return _Intersections.PointPolygon(poly.ToMyVector2List(), translation.ToMyVector2());    
            return false;
        }
    }
}