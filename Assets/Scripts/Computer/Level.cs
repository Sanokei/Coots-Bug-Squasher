using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using BuildingBlocks.DataTypes;

using PixelGame;
using System;

[Obsolete("Replaced by renderstreaming assets")]
[CreateAssetMenu(fileName = "NewLevel", menuName = "Custom/Level")]
public class Level : ScriptableObject
{
    /*

        RAN OUT OF TIME FIXME

    */
    // // file 
    [TextArea(15,20)]
    public string FileData;
    // dynamically add gameobjects and pixelcomponents by adding to a master lua file
    // //  List: GameObjects
    // //  {
    // //      List: PixelComponent
    // //      {
    // //          PixelComponent,
    // //          add string
    // //      }
    // //  }
    // //
    // public List<InspectableDictionary<string,string>> PixelGameObjects;
    public string PrivateFileData;
    // Temporary hard coding for now (probably forever)
}