using System.Collections;

using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Interop;
using MoonSharp.Interpreter.Execution.VM;

using PixelGame.Object;
using System.IO;

/*
script.Globals["test"] = new Action<string, MyEnum>(this.TestMethod);
```lua
test('hello world', MyEnum.Value1)
```
*/
namespace PixelGame.Component
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class PixelBehaviourScript : PixelComponent
    {
        public string FileData;
        bool _AddParentToGlobal = true;
        public override PixelGameObject parent{get;set;}
        public Script script = new Script();
        ScriptFunctionDelegate onKeyDown, onKeyUp;
        ScriptFunctionDelegate onUpdate, onStart;

        // public ScriptFunctionDelegate's to use as the pixel components
        ScriptFunctionDelegate onTrigger, onCollision;
        
        void OnEnable()
        {
            AlphaJargon.onKeyDownEvent += KeyDown;

            AlphaJargon.onUpdateEvent += OnUpdateEventHandler;

            PixelCollider.onTriggerEvent += TriggerEvent;
            PixelCollider.onCollisionEvent  += CollisionEvent;
        }

        void OnDisable()
        {
            AlphaJargon.onKeyDownEvent -= KeyDown;
            
            AlphaJargon.onUpdateEvent -= OnUpdateEventHandler;

            PixelCollider.onTriggerEvent -= TriggerEvent;
            PixelCollider.onCollisionEvent  -= CollisionEvent;
        }

        internal DynValue run(DynValue function, params DynValue[] args)
		{
            UserData.RegisterAssembly();
            DynValue func = script.Globals.Get(function.String);
            Closure closure;
            try
            {
                closure = func.Function;
            }
            catch
            {
                throw new ScriptRuntimeException($"Function \"{function.String}\" does not exist in this context. Check spelling.");
            }
            int numOfArgs = closure.GetUpvaluesCount() - 1; // it counts from 1 because lua.
            if(args.Length > numOfArgs)
                throw new ScriptRuntimeException($"Too many arguments for function \"{function.String}\". Expected {numOfArgs} got {args.Length}.");
            return script.Call(func,args);
        }
        public DynValue run(DynValue function)
		{
            return run(function, new DynValue[0]);
        }
        // I honestly hate this but its how its going to have to be
        public DynValue run(DynValue function, DynValue arg1)
        {
            return run(function,new DynValue[1]{arg1});
        }
        public DynValue run(DynValue function, DynValue arg1, DynValue arg2)
        {
            return run(function,new DynValue[2]{arg1,arg2});
        }
        public DynValue run(DynValue function, DynValue arg1, DynValue arg2, DynValue arg3)
        {
            return run(function,new DynValue[3]{arg1,arg2,arg3});
        }
        public DynValue run(DynValue function, DynValue arg1, DynValue arg2, DynValue arg3, DynValue arg4)
        {
            return run(function,new DynValue[4]{arg1,arg2,arg3,arg4});
        }
        public DynValue run(DynValue function, DynValue arg1, DynValue arg2, DynValue arg3, DynValue arg4, DynValue arg5)
        {
            return run(function,new DynValue[5]{arg1,arg2,arg3,arg4,arg5});
        }
        public DynValue run(DynValue function, DynValue arg1, DynValue arg2, DynValue arg3, DynValue arg4, DynValue arg5, DynValue arg6)
        {
            return run(function,new DynValue[6]{arg1,arg2,arg3,arg4,arg5,arg6});
        }
        public void add(DynValue FileData)
        {
            // Im not sure if the best practice is to use the variable, or to just make it true
            // since the variable is set to true, but also maybe the redunancy is good?
            // add(FileData, _AddParentToGlobal);

            // because it sets _AddParentToGlobal, imma play it safe and just make it true instead
            add(FileData,true);
        }
        public void add(DynValue FileData, bool AddParentToGlobal)
        {
            // string multiliteralString = FileData.ToString(); // Get the multiliteral string from the DynValue
            // string normalString = multiliteralString.Substring(1, multiliteralString.Length - 2); // Remove the first and last quotes enclosing the string
            // this.FileData = normalString;
            this.FileData = FileData.String;
            _AddParentToGlobal = AddParentToGlobal;
        }
        //
        public void addFile(DynValue FileData)
        {
            addFile(FileData, true);
        }
        public void addFile(DynValue FileData, bool AddParentToGlobal)
        {
            StartCoroutine(GetLuaFile(FileData.String));
            _AddParentToGlobal = AddParentToGlobal;
        }
        private IEnumerator GetLuaFile(string filePath)
        {
            yield return LoadLuaFile.GetLuaFile(filePath, HandleLuaFile);
        }
        private void HandleLuaFile(string text)
        {
            this.FileData = text;
        }
        public override void Remove()
        {
            Destroy(this);
        }
        public void addPixelGameObjectToScriptGlobals(DynValue key)
        {
            addPixelGameObjectToScriptGlobals(key.String);
        }
        public void addPixelGameObjectToScriptGlobals(string key)
        {
            // Bad Solution
            addPixelGameObjectToScriptGlobals(key,AlphaJargon.Instance[key]);
        }
        public void addPixelGameObjectToScriptGlobals(string key, IPixelObject value)
        {
            // Debug.Log($"key: {key} + value: {value}");
            UserData.RegisterAssembly();
            script.Globals[key] = value;
        }
        public override void Create(PixelGameObject parent)
        {
            this.parent = parent;
            if(_AddParentToGlobal)
                addPixelGameObjectToScriptGlobals(parent.name,parent); 
        }

        public void RunScript()
        {
            RunScript(this.FileData);
        }
        public void RunScript(string FileData)
        {
            UserData.RegisterAssembly();

            // sets default options
            script.Options.DebugPrint = (x) => {Debug.Log(x);};
            ((ScriptLoaderBase)script.Options.ScriptLoader).IgnoreLuaPathGlobal = true;
            ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(System.IO.Path.Combine(Application.persistentDataPath,"/modules/","?") + ".lua");
            
            // adds a lot of the internal commands
            // script.Globals["internal"] = new Internal();
            script.Globals["Event"] = new PixelEvent();

            DynValue fn = script.DoString(FileData);

            // cant do null checks cuz .Get returns DynValue.Nil not null
            // onStart = script.Globals.Get("Start").Function.GetDelegate() ?? null;

            onStart = script.Globals.Get("Start") != DynValue.Nil ? script.Globals.Get("Start").Function.GetDelegate() : null;

            onKeyDown = script.Globals.Get("OnKeyDown") != DynValue.Nil ? script.Globals.Get("OnKeyDown").Function.GetDelegate(): null;


            onCollision = script.Globals.Get("OnCollision") != DynValue.Nil ? script.Globals.Get("OnCollision").Function.GetDelegate() : null;

            onTrigger = script.Globals.Get("OnTrigger") != DynValue.Nil ? script.Globals.Get("OnTrigger").Function.GetDelegate() : null;
            
            // onAwake
            onStart?.Invoke();
            onUpdate = script.Globals.Get("Update") != DynValue.Nil ? script.Globals.Get("Update").Function.GetDelegate() : null;
        }

        // all event handlers that invoke script delegate
        private void OnUpdateEventHandler()
        {
            onUpdate?.Invoke();
        }

        // Key up and down
        private void KeyDown(string KeyCode)
        {
            if(KeyCode != "None")
                onKeyDown?.Invoke(DynValue.NewString(KeyCode));
        }
        //
        private void TriggerEvent(PixelGameObject self, PixelGameObject other)
        {
            onTrigger?.Invoke(DynValue.NewString(self.name), DynValue.NewString(other.name));
        }
        //
        private void CollisionEvent(PixelGameObject self, PixelGameObject other)
        {
            onCollision?.Invoke(DynValue.NewString(self.name), DynValue.NewString(other.name));
        } 
        // private void TriggerEvent(KeyValuePair<PixelPosition, Pixel> other, PixelGameObject parent)
        // {
        //     onTrigger?.Invoke(DynValue.NewNumber(other.Key[0]),DynValue.NewNumber(other.Key[1]),DynValue.NewString(parent.name));
        // }
        // //
        // private void CollisionEvent(KeyValuePair<PixelPosition, Pixel> other, PixelGameObject parent)
        // {
        //     onCollision?.Invoke(DynValue.NewNumber(other.Key[0]),DynValue.NewNumber(other.Key[1]),DynValue.NewString(parent.name));
        // } 
    }
}