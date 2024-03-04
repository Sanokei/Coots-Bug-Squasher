using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelGame.Object;

namespace PixelGame.Component
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelTransform : PixelComponent
    {
        public override PixelGameObject parent{get;set;}

        public override void Create(PixelGameObject parent)
        {
            this.parent = parent;
        }
        public PixelTransform add(PixelPosition pp /*hehe*/)
        {
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
            Vector3 trans = new Vector3(x * PixelScreenManager.Instance[parent].CellSize, y * PixelScreenManager.Instance[parent].CellSize);
            PixelPosition translation = new PixelPosition(x,y);
            if(!CheckCollision(translation))
            {
                transform.localPosition += trans;
                parent.position += translation;
            }
            return parent.position;
        }

        private bool CheckCollision(PixelPosition translation)
        {
            // List<KeyValuePair<PixelPosition, Pixel>>
            List<KeyValuePair<PixelPosition, Pixel>> selfpixels = PixelScreenManager.Instance.GetPixelsWithColliderAfterTranslation(parent, translation);
            List<KeyValuePair<PixelGameObject,List<KeyValuePair<PixelPosition, Pixel>>>> otherpixels = PixelScreenManager.Instance.GetPixelsWithColliderOtherThan(parent);


            foreach(KeyValuePair<PixelPosition, Pixel> self in selfpixels)
            {
                foreach(KeyValuePair<PixelGameObject,List<KeyValuePair<PixelPosition, Pixel>>> otherParent in otherpixels)
                {
                    foreach(KeyValuePair<PixelPosition, Pixel> other in otherParent.Value)
                        if(self.Key == other.Key)
                        {
                            if(other.Value.Collider.isTrigger)
                            {
                                PixelCollider.onTriggerEvent?.Invoke(parent, otherParent.Key);
                                return false;
                            }
                            else
                            {
                                PixelCollider.onCollisionEvent?.Invoke(parent, otherParent.Key);
                                return true;
                            }
                        }
                }
            }
            return false;
        }
        public override void Remove()
        {
            Destroy(this);
        }
    }
}