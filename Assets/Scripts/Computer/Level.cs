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
    [TextArea(13,13)]
    public string level;
        
}