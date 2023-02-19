using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using BuildingBlocks.DataTypes;


[CreateAssetMenu(fileName = "NewLevel", menuName = "Custom/Level")]
public class Level : ScriptableObject
{
    // file 
    [TextArea(15,20)]
    public string FileData;
    // level
    public InspectableDictionary<Layers, string> Layers = new InspectableDictionary<Layers, string> {
        { global::Layers.Background, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" },
        { global::Layers.Items, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" },
        { global::Layers.IOT, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" },
        { global::Layers.Characters, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" },
        { global::Layers.Walls, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" },
        { global::Layers.Exit, "oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" }
    };
        
}

public enum Layers
{
    Background,
    Items,
    IOT,
    Characters,
    Walls,
    Exit
}
