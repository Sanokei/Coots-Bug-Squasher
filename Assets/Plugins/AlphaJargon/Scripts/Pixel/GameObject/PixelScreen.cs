using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using BuildingBlocks.DataTypes;

using PixelGame;

public class PixelScreen : PixelGameObject
{
    // Every Physical Pixel 
    public InspectableDictionary<int, Pixel> grid;

    public Pixel this[int index]
    {
        get
        {
            return grid[index];
        }
        set
        {
            grid[index] = value;
        }
    }

    public PixelScreen ConvertSpriteStringToScreen(string SpriteString)
    {
        Enumerable.Range(0, SpriteString.Length)
            .ToList()
            .ForEach(index => grid[index].CharToPixel(SpriteString[index])
        );
        
        return this;
    }
}
