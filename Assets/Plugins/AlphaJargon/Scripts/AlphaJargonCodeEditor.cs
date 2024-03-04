using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

using PixelGame.Object;

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
        script.Options.DebugPrint = (x) => {Debug.Log(x);};
        ((ScriptLoaderBase)script.Options.ScriptLoader).IgnoreLuaPathGlobal = true;

        // ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(System.IO.Path.Combine(Application.persistentDataPath,"/modules/","?") + ".lua");
        try
        {
            DynValue fn = script.LoadString(FileData);
            fn.Function.Call();
        }
        catch (ScriptRuntimeException e)
        {
            Debug.LogError(e.DecoratedMessage);
        }
    }

    [MoonSharpUserData]
    struct TrollEvent
    {
        public void Invoke(string Name)
        {
            if(Name == "win")
            {
                throw new ScriptRuntimeException("Nice try dummy");
            }
        }
    }
}


