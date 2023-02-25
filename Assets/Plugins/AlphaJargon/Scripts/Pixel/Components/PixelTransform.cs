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
        PixelGameObject parent;

        public override void Create(PixelGameObject parent)
        {
            this.parent = parent;
        }
        public PixelTransform add(PixelPosition pp /*hehe*/)
        {
            parent.position = pp;
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
            PixelPosition translation = new PixelPosition(x,y);
            Debug.Log($"Position: {(parent.position + translation).x},{(parent.position + translation).y}");
            if(!CheckCollision(translation))
            {
                transform.Translate(trans);
                parent.position += translation;
            }
            return parent.position;
        }

        private bool CheckCollision(PixelPosition translation)
        {
            // List<KeyValuePair<PixelPosition, Pixel>>
            List<KeyValuePair<PixelPosition, Pixel>> box = PixelScreenManager.Instance.GetPixelsWithCollider(parent, translation);
            foreach(KeyValuePair<PixelPosition, Pixel> cell in box)
            {
                Debug.Log($"Cell: {cell.Key.x},{cell.Key.y}");
            }
            // List<KeyValuePair<PixelPosition, Pixel>> other = PixelScreenManager.Instance.GetPixelsWithColliderOtherThan(parent,translation + parent.position);

            // foreach(KeyValuePair<PixelPosition, Pixel> cell in box)
            // {
            //     Debug.Log($"Cell: {cell.Key.x},{cell.Key.y}");
            //     foreach(KeyValuePair<PixelPosition, Pixel> othercell in other)
            //     {
            //         Debug.Log($"Other: {othercell.Key.x},{othercell.Key.y}");
            //         return (cell.Key == othercell.Key);
            //     }
            // }
            return false;
        }

    }
}