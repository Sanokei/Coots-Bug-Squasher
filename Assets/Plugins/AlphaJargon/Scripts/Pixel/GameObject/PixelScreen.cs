using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using BuildingBlocks.DataTypes;

using PixelGame;
using System;

public class PixelScreen : MonoBehaviour, IPixelObject
{
    public delegate void onPixelScreenChangeDelegate(PixelGameObject parent,PixelScreen pixelScreen);
    public static onPixelScreenChangeDelegate onPixelScreenCreateEvent, onPixelScreenDeleteEvent;
    // Vector2 _DefaultGridCellSize;
    void OnEnable()
    {
        _GridLayout = gameObject.GetComponent<GridLayoutGroup>();
        GridSideSize = _GridLayout.constraintCount;
    }
    void Start()
    {
        // _GridLayout.cellSize = _DefaultGridCellSize * SneakGame.Instance.gameObject.transform.localScale;
        // int xy = (int)SneakGame.Instance.gameObject.GetComponent<RectTransform>().rect.width / GridSideSize;
        // _GridLayout.cellSize = new Vector2(xy,xy);
        // CellSize = (int)_GridLayout.cellSize.x; // Asume aspect ratio of 1
    }
    void OnRectTransformDimensionsChange()
    {
        // int xy = (int)SneakGame.Instance.gameObject.GetComponent<RectTransform>().rect.width / GridSideSize;
        // _GridLayout.cellSize = new Vector2(xy,xy);
    }
    // Every Physical Pixel 
    GridLayoutGroup _GridLayout;
    // DO NOT EDIT THIS VARIABLE, IT WILL RESET ITS DATA.
    public InspectableDictionary<int, Pixel> grid;
    public int CellSize
    {
        get
        {
            return (int)_GridLayout.cellSize.x;
        }
    }
    public static int GridSideSize{get; private set;} // constraintCount can be static cuz it wont change unless the whole engine changes.
    
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
}
