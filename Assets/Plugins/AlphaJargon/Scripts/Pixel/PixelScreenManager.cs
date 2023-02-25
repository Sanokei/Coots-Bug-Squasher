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
    }

    void OnDisable()
    {
        PixelScreen.onPixelScreenCreateEvent -= AddToPixelScreen;
    }

    public List<KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>> Layers = new List<KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>>();

    void AddToPixelScreen(PixelGameObject parent, PixelScreen pixelScreen)
    {
        Layers.Add(new KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>(parent, pixelScreen.grid.ToDictionary()));
    }

    private static Dictionary<int, Pixel> FilterPixelsWithColliders(Dictionary<int, Pixel> pixels)
    {
        var filteredPixels = new Dictionary<int, Pixel>();

        foreach (KeyValuePair<int, Pixel> pixel in pixels)
        {
            if (pixel.Value.Collider != null)
            {
                filteredPixels.Add(pixel.Key, pixel.Value);
            }
        }

        return filteredPixels;
    }

    private List<KeyValuePair<PixelPosition, Dictionary<int, Pixel>>> FindPixelsWithColliders()
    {
        List<KeyValuePair<PixelPosition, Dictionary<int, Pixel>>> filtered = new List<KeyValuePair<PixelPosition, Dictionary<int, Pixel>>>();

        foreach (KeyValuePair<PixelGameObject, Dictionary<int, Pixel>> layer in Layers)
        {
            Dictionary<int, Pixel> pixelsWithColliders = FilterPixelsWithColliders(layer.Value);

            if (pixelsWithColliders.Count > 0)
            {
                filtered.Add(new KeyValuePair<PixelPosition, Dictionary<int, Pixel>>(layer.Key.position, pixelsWithColliders));
            }
        }

        return filtered;
    }

    // use this stuff
    public List<KeyValuePair<PixelPosition, Pixel>> GetPixelsWithCollider()
    {
        List<KeyValuePair<PixelPosition, Pixel>> pixels = new List<KeyValuePair<PixelPosition, Pixel>>();

        foreach (KeyValuePair<PixelPosition, Dictionary<int, Pixel>> layer in FindPixelsWithColliders())
        {
            PixelPosition layerPosition = layer.Key;

            foreach (KeyValuePair<int, Pixel> pixel in layer.Value)
            {
                PixelPosition pixelPosition = PixelPosition.FromIndex(pixel.Key);
                PixelPosition absolutePosition = layerPosition + pixelPosition;

                pixels.Add(new KeyValuePair<PixelPosition, Pixel>(absolutePosition, pixel.Value));
            }
        }

        return pixels;
    }

    public List<KeyValuePair<PixelPosition, Pixel>> GetPixelsWithCollider(PixelPosition pixelPosition)
    {
        List<KeyValuePair<PixelPosition, Pixel>> pixels = new List<KeyValuePair<PixelPosition, Pixel>>();

        foreach (KeyValuePair<PixelPosition, Pixel> pixel in GetPixelsWithCollider())
        {
            if (pixel.Key.Equals(pixelPosition))
            {
                pixels.Add(pixel);
            }
        }

        return pixels;
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
                         Debug.Log($"This({(pixel.Key + pgo.position + translation).x},{(pixel.Key + pgo.position + translation).y})");
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
                        Debug.Log($"Other({(pixel.Key + layer.Key.position).x},{(pixel.Key + layer.Key.position).y})");
                        pixels.Add(new KeyValuePair<PixelPosition, Pixel>(pixel.Key + layer.Key.position, pixel.Value));
                    }
                }
            }
        }
        return pixels;
    }
}