using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using BuildingBlocks.DataTypes;

using PixelGame;

public class PixelScreen : MonoBehaviour, IPixelObject
{
    public delegate void onPixelScreenChangeDelegate(PixelGameObject parent,PixelScreen pixelScreen);
    public static onPixelScreenChangeDelegate onPixelScreenCreateEvent, onPixelScreenDeleteEvent;
    void OnEnable()
    {
        _GridLayout = gameObject.GetComponent<GridLayoutGroup>();
        // _ParentCanvasScaler = gameObject.GetComponentInParent<CanvasScaler>();
        // _GridLayout.cellSize = new Vector2(_GridLayout.cellSize.x * _ParentCanvasScaler.scaleFactor, _GridLayout.cellSize.y * _ParentCanvasScaler.scaleFactor);
        _GridLayout.cellSize = _GridLayout.cellSize * GetSizeDeltaToProduceSize();
        CellSize = (int)_GridLayout.cellSize.x;
        GridSideSize = _GridLayout.constraintCount;
    }
    // Every Physical Pixel 
    GridLayoutGroup _GridLayout;
    // CanvasScaler _ParentCanvasScaler;
    // DO NOT EDIT THIS VARIABLE, IT WILL RESET ITS DATA.
    public InspectableDictionary<int, Pixel> grid;
    public static int CellSize{get; private set;}
    public static int GridSideSize{get; private set;} // constraintCount
    
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

    // Copying code from AspectRatioFitter
    private RectTransform m_Rect;
    private RectTransform rectTransform
    {
        get
        {
            if (m_Rect == null)
            {
                m_Rect = GetComponent<RectTransform>();
            }

            return m_Rect;
        }
    }
    private Vector2 GetParentSize()
    {
        RectTransform rectTransform = this.rectTransform.parent as RectTransform;
        if ((bool)rectTransform)
        {
            return rectTransform.rect.size;
        }

        return Vector2.zero;
    }

    private Vector2 GetSizeDeltaToProduceSize()
    {
        return GetParentSize() - GetParentSize() * (rectTransform.anchorMax - rectTransform.anchorMin);
    }
}
