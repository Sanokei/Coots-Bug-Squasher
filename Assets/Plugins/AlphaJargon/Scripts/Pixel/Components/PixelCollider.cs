using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Text;

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
        public bool isTrigger = false;
        //
        public override PixelGameObject parent{get;set;}
        public List<PolygonCollider2D> pixelCollider;
        public PixelScreen screen; // Only use this to show on screen
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
            screen = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
            pixelCollider = new List<PolygonCollider2D>();
            this.parent = parent;
        }
        public void AddScreenToScreenManager()
        {
            PixelScreen.onPixelScreenCreateEvent?.Invoke(parent,screen);
        }
        public override void Remove()
        {
            PixelScreen.onPixelScreenDeleteEvent?.Invoke(parent,screen);
            parent.PixelComponents.Remove(this.ToString());
            Destroy(this);
        }
        public PixelComponent add(DynValue ColliderString, bool isTrigger = false)
        {
            string collstr = ColliderString.ToString();
            collstr = new string(collstr.Where(c => !char.IsWhiteSpace(c)).ToArray()).Replace("\"","");
            return add(collstr);
        }

        public PixelComponent add(string ColliderString, bool isTrigger = false)
        {
            List<PixelPosition> pixelPositions = new List<PixelPosition>();
            char[] str = ColliderString.ToCharArray();
            for (int i = 0; i < str.Length; i++)  
            {
                if (str[i] == 'x')
                {
                    int row = i / PixelScreen.GridSideSize;
                    int col = i % PixelScreen.GridSideSize;
                    pixelPositions.Add(new PixelPosition(new Vector2Int(col, row)));
                }
            }
            AddColliderToScreen(ColliderString);
            return this;
        }
        public PixelScreen AddColliderToScreen(string ColliderString)
        {
            // foreach(PixelPosition pp in pixelPositions)
            //     ColliderToPixel(screen.grid[pp.ToIndex()], this);
            // return screen;

            var outputStrings = Enumerable.Range(0, ColliderString.Length / PixelScreen.GridSideSize)
                .Select(i => ColliderString.Substring(i * PixelScreen.GridSideSize, PixelScreen.GridSideSize));

            string[] stringArray = outputStrings.ToArray();

            Array.Reverse(stringArray); 
            StringBuilder sb = new StringBuilder();

            foreach (var str in stringArray)
            {
                sb.Append(str);
            }
            string reversedString = sb.ToString();

            // char[] charArray = reversedString.ToCharArray();
            // Array.Reverse(charArray);
            string finalString = new string(reversedString);
            for(int i = 0; i < finalString.Count(); i++)
            {
                if(finalString[i] != 'o')
                    ColliderToPixel(screen.grid[i], this);
            }
            return screen;
        }

        // did this for condormity sake with pixelsprite
        public void ColliderToPixel(Pixel pixel, PixelCollider pc)
        {
            pixel.Collider = pc;
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

// public PixelComponent add(List<PixelPosition> pixelPositions, bool isTrigger = false)
// {
//     // convert all the pixel positions to coords
//     List<MyVector2> Points = new List<MyVector2>();
//     float _offSet = (PixelScreen.GridSideSize * PixelScreen.CellSize) / 2.000f;
//     foreach (PixelPosition pixelPosition in pixelPositions)
//     {
        
//         Points.Add(new MyVector2(pixelPosition.x * PixelScreen.CellSize - _offSet, (PixelScreen.GridSideSize - pixelPosition.y - 1) * PixelScreen.CellSize - _offSet));
//         Points.Add(new MyVector2(pixelPosition.x * PixelScreen.CellSize - _offSet, (PixelScreen.GridSideSize - pixelPosition.y) * PixelScreen.CellSize - _offSet));
//         Points.Add(new MyVector2((pixelPosition.x + 1) * PixelScreen.CellSize - _offSet, (PixelScreen.GridSideSize - pixelPosition.y) * PixelScreen.CellSize - _offSet));
//         Points.Add(new MyVector2((pixelPosition.x + 1) * PixelScreen.CellSize - _offSet, (PixelScreen.GridSideSize - pixelPosition.y - 1) * PixelScreen.CellSize - _offSet));
//     }

//     // get the perimeter using 'quickhull' convex hull algorithm
//     PolygonCollider2D pc2d = gameObject.AddComponent<PolygonCollider2D>();
//     pc2d.SetPath(0, MyVector2ToVector2(QuickhullAlgorithm2D.GenerateConvexHull(Points, false)));
//     pc2d.isTrigger = isTrigger;
//     this.isTrigger = isTrigger;
//     pixelCollider.Add(pc2d);

//     // adds the polygoncollider2d to all the pixels it contains so the pixel
//     // can be used to know which collider its apart of
//     AddColliderToScreen(pixelPositions);

//     return this;
// }