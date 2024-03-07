using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

using PixelGame.Object;
using PixelGame;

[MoonSharpUserData]
public class AlphaJargonCodeEditor : MonoBehaviour
{
    [HideInInspector] public string FileData;

    Script script = new Script();

    public void add(string FileData)
    {
        this.FileData = FileData;
    }
    public void addPixelGameObjectToJargonScriptGlobals(string key, IPixelObject value)
    {
        UserData.RegisterAssembly();
        script.Globals[key] = value;
    }
    public void RunScript()
    {
        RunScript(this.FileData);
    }
    public void RunScript(string FileData)
    {
        UserData.RegisterAssembly();
        
        // Add internal globals
        script.Globals["Event"] = new TrollEvent(); // :3

        // sets default options
        script.Options.DebugPrint = (x) => {PixelGame.Console.Instance.add(x, Color.white);};
        ((ScriptLoaderBase)script.Options.ScriptLoader).IgnoreLuaPathGlobal = true;

        DynValue fn = script.DoString(FileData);
    }

    [MoonSharpUserData]
    struct TrollEvent
    {
        public void Invoke(string Name, params string[] args)
        {
            if(Name == "oAuthClear")
            {
                throw new ScriptRuntimeException("UNLAWFUL USE OF \"Event.Invoke\" COMMAND DETECTED");
            }
        }
    }
}


