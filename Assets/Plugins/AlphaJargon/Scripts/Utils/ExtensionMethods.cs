using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildingBlocks.DataTypes;
using System.Linq;
using System;

using PixelGame.Object;
using PixelGame;

public static class ExtensionMethods
{
    public static int ToIndex(this PixelPosition pixelPosition)
    {
        return pixelPosition.x * PixelScreen.GridSideSize + pixelPosition.y;
    }
    [Obsolete("Use InspectableDictonary.Dictionary instead.")]
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this InspectableDictionary<TKey, TValue> inspectableDictionary) {
        var dictionary = new Dictionary<TKey, TValue>();
        // It was a bug with how the InspectableDictonary was doing InspectorToDictonary.
            // // NOTE: Added OrderBy, enumerable dictonaries are not deterministic. Stupid as fuck bug.
            //     // OrderBy(elm => elm.Key) was giving me a Enumrable count of 0. Will fix later.
        Dictionary<TKey,TValue> indicKey = inspectableDictionary.Dictionary;
        foreach (var kvp in indicKey.OrderBy( key => key.Key)) {
            dictionary.Add(kvp.Key, kvp.Value);
        }
        return dictionary;
    }

    public static InspectableDictionary<TKey, TValue> FromDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) {
        var inspectableDictionary = new InspectableDictionary<TKey, TValue>();
        foreach (var kvp in dictionary) {
            inspectableDictionary.Add(kvp.Key, kvp.Value);
        }   
        return inspectableDictionary;
    }
}