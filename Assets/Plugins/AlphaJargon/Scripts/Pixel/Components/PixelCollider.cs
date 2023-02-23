using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Habrador_Computational_Geometry;
using MoonSharp.Interpreter;
/*
    I think this will only work for this specific layout of Anchors
    So if you are going to change the rect transforms and stuff keep that in mind
*/
namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelCollider : PixelComponent
    {
        public delegate void OnTriggerDelegate(Collider2D other, PixelGameObject parent);
        public static OnTriggerDelegate onTriggerEnter, onTriggerStay, onTriggerExit;
        public delegate void OnCollisionDelegate(Collision2D other, PixelGameObject parent);
        public static OnCollisionDelegate onCollisionEnter, onCollisionStay, onCollisionExit;

        //
        PixelGameObject parent;
        List<PolygonCollider2D> pixelCollider;

        public override void Create(PixelGameObject parent)
        {
            pixelCollider = new List<PolygonCollider2D>();
            this.parent = parent;
        }
        public PixelComponent add(DynValue ColliderString, bool isTrigger = false)
        {
            string collstr = ColliderString.ToString();
            string grid = (new string((collstr.Split('\n')).ToList()[0].Where(c => !char.IsWhiteSpace(c)).ToArray())).Replace("\"","");
            collstr = new string(collstr.Where(c => !char.IsWhiteSpace(c)).ToArray()).Replace("\"","");
            return add(collstr,grid.Length);
        }

        public PixelComponent add(string ColliderString, int grid, bool isTrigger = false)
        {
            List<PixelPosition> pixelPositions = new List<PixelPosition>();
            if (parent.PixelComponents.Values.Any(x => x is PixelSprite))
            {
                char[] str = ColliderString.ToCharArray();
                for (int i = 0; i < str.Length; i++)  
                {
                    if (str[i] == 'x')
                    {
                        int row = i / grid;
                        int col = i % grid;
                        pixelPositions.Add(new PixelPosition(new Vector2Int(col, row)));
                    }
                }
            }
            return add(pixelPositions, grid);
        }
        public PixelComponent add(List<PixelPosition> pixelPositions, int grid, bool isTrigger = false)
        {
            // convert all the pixel positions to coords
            List<MyVector2> Points = new List<MyVector2>();
            PixelScreen sprite = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
            float _cellSize = sprite.gridLayout.cellSize.x;
            float _offSet = (sprite.gridLayout.constraintCount * _cellSize) / 2.000f;;
            Destroy(sprite.gameObject);
            foreach (PixelPosition pixelPosition in pixelPositions)
            {
                Points.Add(new MyVector2(pixelPosition.x * _cellSize - _offSet, (grid - pixelPosition.y - 1) * _cellSize - _offSet));
                Points.Add(new MyVector2(pixelPosition.x * _cellSize - _offSet, (grid - pixelPosition.y) * _cellSize - _offSet));
                Points.Add(new MyVector2((pixelPosition.x + 1) * _cellSize - _offSet, (grid - pixelPosition.y) * _cellSize - _offSet));
                Points.Add(new MyVector2((pixelPosition.x + 1) * _cellSize - _offSet, (grid - pixelPosition.y - 1) * _cellSize - _offSet));
            }

            // get the perimeter using 'quickhull' convex hull algorithm
            PolygonCollider2D pc2d = gameObject.AddComponent<PolygonCollider2D>();
            pc2d.SetPath(0, MyVector2ToVector2(QuickhullAlgorithm2D.GenerateConvexHull(Points, false)));
            pc2d.isTrigger = isTrigger;
            pixelCollider.Add(pc2d);

            return this;
        }

        List<Vector2> MyVector2ToVector2(List<MyVector2> myVector2List)
        {
            List<Vector2> vector2List = new List<Vector2>();
            foreach (MyVector2 myVector2 in myVector2List)
            {
                vector2List.Add(new Vector2(myVector2.x, myVector2.y));
            }
            return vector2List;
        }

        // Trigger and Collision
        void OnTriggerEnter2D(Collider2D other)
        {
            onTriggerEnter?.Invoke(other, this.parent);
        }
        void OnTriggerStay2D(Collider2D other)
        {
            onTriggerStay?.Invoke(other, this.parent);
        }
        void OnTriggerExit2D(Collider2D other)
        {
            onTriggerExit?.Invoke(other, this.parent);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            onCollisionEnter?.Invoke(other, this.parent);
        }
        void OnCollisionStay2D(Collision2D other)
        {
            onCollisionStay?.Invoke(other, this.parent);
        }
        void OnCollisionExit2D(Collision2D other)
        {
            onCollisionExit?.Invoke(other, this.parent);
        }
    }
}