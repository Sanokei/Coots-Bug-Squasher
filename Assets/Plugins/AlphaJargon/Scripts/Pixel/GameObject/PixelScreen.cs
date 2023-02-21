using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using BuildingBlocks.DataTypes;

using PixelGame;

public class PixelScreen : PixelGameObject
{
    void OnEnable()
    {
        gridLayout = gameObject.GetComponent<GridLayout>();
    }
    // Every Physical Pixel 
    public GridLayout gridLayout;
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
        Enumerable.Range(0, SpriteString.Length - 1)
            .ToList()
            .ForEach(index => grid[index].CharToPixel(SpriteString[index])
        );
        
        return this;
    }
}
