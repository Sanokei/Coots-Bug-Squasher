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


        // FIXME: this is so unoptomized 
        private bool CheckCollision(Vector3 translation)
        {
            foreach(PixelGameObject pgo in FindObjectsOfType(typeof(PixelGameObject)))
            {
                try
                {
                    if (!pgo.Equals(this) && !pgo.PixelComponents.Values.Any(x => x.GetType() == typeof(PixelCollider)));
                }
                catch
                {
                    continue;
                }
                    foreach(PixelCollider pc in pgo.PixelComponents.Values.OfType<PixelCollider>().ToArray())
                        foreach(PolygonCollider2D poly in pc.pixelCollider)
                        {
                            Debug.Log($"Poly: {poly.ToMyVector2List()}\nPoints: {translation.ToMyVector2()}");
                            return _Intersections.PointPolygon(poly.ToMyVector2List(), translation.ToMyVector2());    
                        }
            }
            return false;
        }
    }
}