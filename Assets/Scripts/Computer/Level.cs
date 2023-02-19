using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BuildingBlocks.DataTypes;

public class Level : MonoBehaviour
{
    // file 
    public string FileData;
    // level
    public InspectableDictionary<Layers,string> Layers;
}

public enum Layers
{
    background,
    items,
    iot,
    characters,
    walls,
    exit
}