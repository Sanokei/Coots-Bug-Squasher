using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelGame
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public abstract class PixelComponent : MonoBehaviour, ISpawnable
    {
        public abstract PixelGameObject parent{get;set;}
        public abstract void Create(PixelGameObject parent);
        public abstract void Remove();

        
    }
}