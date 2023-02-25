using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using BuildingBlocks.DataTypes;

using PixelGame;

// this file makes me want to die inside
// please.. please dont touch it...

public class PixelScreenManager : MonoBehaviour
{
    public static PixelScreenManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        PixelScreen.onPixelScreenCreateEvent += AddToPixelScreen;
        PixelScreen.onPixelScreenDeleteEvent += RemoveFromPixelScreen;
    }

    void OnDisable()
    {
        PixelScreen.onPixelScreenCreateEvent -= AddToPixelScreen;
        PixelScreen.onPixelScreenDeleteEvent -= RemoveFromPixelScreen;
    }

    public List<KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>> Layers = new List<KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>>();

    void AddToPixelScreen(PixelGameObject parent, PixelScreen pixelScreen)
    {
        Layers.Add(new KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>(parent, pixelScreen.grid.ToDictionary()));
    }
    void RemoveFromPixelScreen(PixelGameObject parent, PixelScreen pixelScreen)
    {
        Layers.Remove(new KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>(parent, pixelScreen.grid.ToDictionary()));
    }

    public List<KeyValuePair<PixelPosition, Pixel>> GetPixelsWithCollider(PixelGameObject pgo, PixelPosition translation)
    {
        List<KeyValuePair<PixelPosition, Pixel>> pixels = new List<KeyValuePair<PixelPosition, Pixel>>();
        
        foreach (KeyValuePair<PixelGameObject, Dictionary<int, Pixel>> layer in Layers)
        {
            if (layer.Key.Equals(pgo))
            {
                foreach (KeyValuePair<int, Pixel> pixel in layer.Value)
                {
                    if (pixel.Value.Collider != null)
                    {
                        pixels.Add(new KeyValuePair<PixelPosition, Pixel>(pixel.Key + pgo.position + translation, pixel.Value));
                    }
                }
            }
        }
        
        return pixels;
    }
    //
    public List<KeyValuePair<PixelPosition, Pixel>> GetPixelsWithColliderOtherThan(PixelGameObject pgo)
    {
        List<KeyValuePair<PixelPosition, Pixel>> pixels = new List<KeyValuePair<PixelPosition, Pixel>>();

        foreach (KeyValuePair<PixelGameObject, Dictionary<int, Pixel>> layer in Layers)
        {
            if (!layer.Key.Equals(pgo))
            {
                foreach (KeyValuePair<int, Pixel> pixel in layer.Value)
                {
                    if (pixel.Value.Collider != null)
                    {
                        pixels.Add(new KeyValuePair<PixelPosition, Pixel>(pixel.Key + layer.Key.position, pixel.Value));
                    }
                }
            }
        }
        return pixels;
    }

    public List<KeyValuePair<int, Pixel>> GetSpriteLayerPixels(PixelGameObject pgo)
    {
        List<KeyValuePair<int, Pixel>> pixels = new List<KeyValuePair<int, Pixel>>();

        foreach (KeyValuePair<PixelGameObject, Dictionary<int, Pixel>> layer in Layers)
        {
            if (layer.Key.Equals(pgo))
            {
                foreach (KeyValuePair<int, Pixel> pixel in layer.Value)
                {
                    if(pixel.Value.isOn)
                        pixels.Add(new KeyValuePair<int, Pixel>(pixel.Key, pixel.Value));
                }
            }
        }

        return pixels;
    }

    public List<KeyValuePair<PixelPosition, Pixel>> GetSpritePixelsAtPosition(PixelGameObject pgo, PixelPosition position)
    {
        List<KeyValuePair<PixelPosition, Pixel>> pixels = new List<KeyValuePair<PixelPosition, Pixel>>();

        foreach (KeyValuePair<int, Pixel> layer in GetSpriteLayerPixels(pgo))
        {
            if (PixelPosition.FromIndex(layer.Key) == position)
            {
                pixels.Add(new KeyValuePair<PixelPosition,Pixel>(PixelPosition.FromIndex(layer.Key) + pgo.position + position,layer.Value));
            }
        }

        return pixels;
    }
}