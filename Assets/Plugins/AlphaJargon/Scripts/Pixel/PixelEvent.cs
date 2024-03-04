using MoonSharp.Interpreter;
using PixelGame.Object;

namespace PixelGame
{
    [MoonSharpUserData]
    public class PixelEvent : IPixelObject
    {
        public delegate void OnPixelGameEvent(string Name,params string[] args);
        public static OnPixelGameEvent onEvent;

        public void Invoke(DynValue Name)
        {
            onEvent?.Invoke(Name.String, new string[0]); // so args isnt null? i forgot why new string[0] is best practice compared to it being blank
        }
        public void Invoke(DynValue Name, DynValue arg0)
        {
            onEvent?.Invoke(Name.String, new string[1]{arg0.String});
        }
        public void Invoke(DynValue Name, DynValue arg0, DynValue arg1)
        {
            onEvent?.Invoke(Name.String, new string[2]{arg0.String, arg1.String});
        }
        public void Invoke(DynValue Name, DynValue arg0, DynValue arg1, DynValue arg2)
        {
            onEvent?.Invoke(Name.String, new string[3]{arg0.String, arg1.String, arg2.String});
        }
        public void Invoke(DynValue Name, DynValue arg0, DynValue arg1, DynValue arg2, DynValue arg3)
        {
            onEvent?.Invoke(Name.String, new string[4]{arg0.String, arg1.String, arg2.String, arg3.String});
        }
        public void Invoke(DynValue Name, DynValue arg0, DynValue arg1, DynValue arg2, DynValue arg3, DynValue arg4)
        {
            onEvent?.Invoke(Name.String, new string[5]{arg0.String, arg1.String, arg2.String, arg3.String, arg4.String});
        }
        public void Invoke(DynValue Name, DynValue arg0, DynValue arg1, DynValue arg2, DynValue arg3, DynValue arg4, DynValue arg5)
        {
            onEvent?.Invoke(Name.String, new string[6]{arg0.String, arg1.String, arg2.String, arg3.String, arg4.String, arg5.String});
        }
        public void Invoke(DynValue Name, DynValue arg0, DynValue arg1, DynValue arg2, DynValue arg3, DynValue arg4, DynValue arg5, DynValue arg6)
        {
            onEvent?.Invoke(Name.String, new string[7]{arg0.String, arg1.String, arg2.String, arg3.String, arg4.String, arg5.String, arg6.String});
        }
    }
}