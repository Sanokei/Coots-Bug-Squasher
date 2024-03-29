using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using BuildingBlocks.DataTypes;
using System.Text;

using MoonSharp.Interpreter;

using PixelGame;
using PixelGame.Component;
using PixelGame.Object;

[MoonSharpUserData]
public class AlphaJargon : MonoBehaviour, IPixelObject
{
    public GameObject parent;
    static AlphaJargon _Instance;
    public static AlphaJargon Instance
    {
        get
        {
            if(!_Instance)
                // The way this is set up, it means AlphaJargon gets created twice. Once as a husk and then again for real. idk if this is fine or can be changed tbh
                _Instance = new GameObject("Temporary").AddComponent<AlphaJargon>();
            return _Instance;
        }
        set
        {
            Destroy(_Instance.gameObject);
            _Instance = value;
        }
    }
    public AlphaJargon CreateInstance(GameObject parent)
    {
        AlphaJargon AJ = new GameObject("AlphaJargon").AddComponent<AlphaJargon>();
        AJ.parent = parent;
        AJ.gameObject.transform.parent = parent.transform;
        AJ.gameObject.transform.localPosition = Vector3.zero;
        AJ.gameObject.transform.localScale = Vector3.one;
        Instance = AJ;
        return AJ;
    }
    // Button Delegates
    public delegate void OnButtonClickDelegate(string KeyCode);
    public static OnButtonClickDelegate onKeyDownEvent;
    public AJState CurrAJState = AJState.PreSet;  
    [SerializeField,TextArea(15,20)]
    private string _FileData;
    public string FileData
    {
        get
        {
            return _FileData;
        }
        set
        {
            _FileData = value;
            Ready();
            Set();
            Go();
        }
    }

    // Totality Execution Order
        // Awake, start, onenable and ondisable need to be native to the script
    public delegate void OnUpdateDelegate();
    public static OnUpdateDelegate onUpdateEvent;

    // Execution Order
    public delegate void TotalityExecutionOrder();
    public static TotalityExecutionOrder awakeGameEvent, initializeGameEvent, startGameEvent;

    // Managers
    PixelScreenManager PixelScreenManager;
    void OnEnable()
    {
        // localScale gets set to a seemingly random number otherwise.
        // refer to: https://forum.unity.com/threads/grid-layout-group-completely-ignores-canvas-scaler-solved.440520/
        this.transform.localScale = Vector3.one;
        PixelScreenManager = gameObject.AddComponent<PixelScreenManager>();
    }
    void FixedUpdate()
    {
        // Input and FixedUpdate dont play nicely
        onUpdateEvent?.Invoke();
    }
    
    public void OnGUI()
    {
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
                onKeyDownEvent?.Invoke(e.keyCode.ToString());
        }
    }
    [HideInInspector] public JargonCompiler Compiler;
    [HideInInspector] public AlphaJargonCodeEditor CodeEditor;
    public void Ready()
    {
        Compiler = gameObject.AddComponent<JargonCompiler>();
        CodeEditor = gameObject.AddComponent<AlphaJargonCodeEditor>(); 
        Compiler.Init(this);
        Compiler.add(FileData);
        CurrAJState = AJState.Set;
    }

    public void Set()
    {
        CurrAJState = AJState.Running;
        Compiler.RunScript();
        // StartCoroutine(RunInGameScripts());
    }
    public void Go()
    {
        AwakeGame();
        InitializeGame();
        StartCoroutine(StartGame());
    }
    

    public void AwakeGame()
    {
        awakeGameEvent?.Invoke();
    }

    public void InitializeGame()
    {
        initializeGameEvent?.Invoke();
    }

    public IEnumerator StartGame()
    {
        yield return null;
        startGameEvent?.Invoke();
    }
    public IEnumerator RunInGameScripts()
    {
        yield return null;
        foreach(PixelGameObject pgo in PixelGameObjects.Values)
            foreach(PixelComponent comp in pgo.PixelComponents.Values)
                if(comp is PixelBehaviourScript)
                {
                    PixelBehaviourScript script = (PixelBehaviourScript)comp;
                    script.RunScript();
                }
    }
    public Image Skybox;
    public InspectableDictionary<string,PixelGameObject> PixelGameObjects = new InspectableDictionary<string, PixelGameObject>();

    public PixelGameObject this[string key] {
        get 
        {
            return PixelGameObjects[key];
        }
        set
        {
            PixelGameObjects[key] = value;
        }
    }

    public PixelGameObject add(string key)
    {
        return add(key,transform);
    }
    // add gameobjects to jargon
    public PixelGameObject add(string key, Transform parentTransform)
    {
        if(!PixelGameObjects.Keys.Contains(key))  
        {
            PixelGameObject value = Instantiate<PixelGameObject>(Resources.Load<PixelGameObject>("Prefabs/Game/PixelGameObject"), parentTransform);
            value.name = key;
            // localScale gets set to a seemingly random number otherwise.
            // refer to: https://forum.unity.com/threads/grid-layout-group-completely-ignores-canvas-scaler-solved.440520/
            value.transform.localScale = Vector3.one;

            Compiler.addPixelGameObjectToJargonScriptGlobals(key,value);
            CodeEditor.addPixelGameObjectToJargonScriptGlobals(key,value);
            PixelGameObjects.Add(key, value);
            return value;
        }
        throw new ScriptRuntimeException($"Key \"{key}\" already used to make PixelGameObject");
    }

/*****************************************************/

//     public string SpriteStringMaker(DynValue SpriteString)
//     {
//         // https://github.com/moonsharp-devs/moonsharp/blob/master/src/MoonSharp.Interpreter/Interop/Converters/TableConversions.cs
//         // The level of abstraction in their code makes me want to commit sepukku
//         // layers and layers of fucking private internal
//         return SpriteStringMaker((Dictionary<PixelPosition,char>) SpriteString.ToObject(typeof(Dictionary<PixelPosition,char>)));
//     }
//     public string SpriteStringMaker(Dictionary<PixelPosition,char> SpriteString)
//     {
//         StringBuilder Default = new StringBuilder("oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo");
//         foreach(PixelPosition key in SpriteString.Keys)
//         {
//             Default[(int)((key.x * 8) + key.y)] = SpriteString[key];
//         }
//         return Default.ToString();
//     }
}

public enum AJState
{
    PreSet,
    Set,
    Running
}

