using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using UnityEngine;

using MoonSharp.Interpreter;
using PixelGame.Object;
/*
    I think this will only work for this specific layout of Anchors
    So if you are going to change the rect transforms and stuff keep that in mind
*/
namespace PixelGame.Component
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelCollider : PixelComponent
    {
        public delegate void OnTrigger(PixelGameObject self, PixelGameObject other);
        public static OnTrigger onTriggerEvent;
        public delegate void OnCollision(PixelGameObject self, PixelGameObject other);
        public static OnCollision onCollisionEvent;
        //
        public bool isTrigger = false;
        //
        public override PixelGameObject parent{get;set;}

        // Used to use Unity's built-in Collider and a quick hull algo, however, switched over to PixelScreen. Forgetting why. 
        // public List<PolygonCollider2D> pixelCollider;
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
            this.parent = parent;
            screen = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
            screen.transform.localPosition = Vector3.one;
            // pixelCollider = new List<PolygonCollider2D>();
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
        public PixelCollider add(DynValue ColliderString, DynValue Boolean)
        {
            return add(ColliderString, Boolean.CastToBool());
        }
        
        public PixelCollider add(DynValue dynValue, bool isTrigger = false)
        {
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
            
            // char[] charArray = reversedString.ToCharArray();
            // Array.Reverse(charArray);
            // string finalString = new(charArray);

            return add(reversedString, isTrigger);
        }

        internal PixelCollider add(string ColliderString, bool isTrigger = false)
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
            AddColliderToScreen(ColliderString, isTrigger);
            return this;
        }
        internal PixelScreen AddColliderToScreen(string ColliderString, bool isTrigger = false)
        {
            for(int i = 0; i <  ColliderString.Count(); i++)
            {
                if(ColliderString[i] != 'o')
                    ColliderToPixel(screen.grid[i], this, isTrigger);
            }
            return screen;
        }

        // did this for condormity sake with pixelsprite
        internal void ColliderToPixel(Pixel pixel, PixelCollider pc, bool isTrigger = false)
        {
            pixel.Collider = pc;
            pixel.Collider.isTrigger = isTrigger;
        }

        // List<Vector2> MyVector2ToVector2(List<MyVector2> myVector2List)
        // {
        //     List<Vector2> vector2List = new List<Vector2>();
        //     foreach (MyVector2 myVector2 in myVector2List)
        //     {
        //         vector2List.Add(new Vector2(myVector2.x, myVector2.y));
        //     }
        //     return vector2List;
        // }
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