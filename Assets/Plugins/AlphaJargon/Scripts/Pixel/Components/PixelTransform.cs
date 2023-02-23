using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelTransform : PixelComponent
    {
        PixelPosition position;
        PixelGameObject parent;
        float _cellSize;

        public override void Create(PixelGameObject parent)
        {
            position = new PixelPosition(0,0);
            
            PixelScreen sprite = Instantiate<PixelScreen>(Resources.Load<PixelScreen>("Prefabs/Game/PixelScreen"),parent.gameObject.transform);
            _cellSize = sprite.gridLayout.cellSize.x;
            Destroy(sprite.gameObject);
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
            transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition,new Vector3(x * _cellSize,y * _cellSize), Time.deltaTime * 1f);
            position = new PixelPosition((int)(gameObject.transform.localPosition.x / _cellSize),(int)(gameObject.transform.localPosition.y / _cellSize));
            return position;
        }
    }
}