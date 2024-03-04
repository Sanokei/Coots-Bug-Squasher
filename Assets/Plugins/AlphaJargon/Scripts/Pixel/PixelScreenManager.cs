using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using BuildingBlocks.DataTypes;

using PixelGame;

// just dont touch.
    // this file makes me want to die inside
    // please.. please dont touch it...

    // Eventually convert to using Sutherland-Hodgman algo to check
    // for polygon on polygon clipping (not Griner-Hormann cuz slow and not really needed for squares)

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

    // public List<KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>> Layers = new List<KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>>();
    public List<KeyValuePair<PixelGameObject, PixelScreen>> Layers = new();


    void AddToPixelScreen(PixelGameObject parent, PixelScreen pixelScreen)
    {
        // Layers.Add(new KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>(parent, pixelScreen.grid.Dictionary));
        Layers.Add(new KeyValuePair<PixelGameObject, PixelScreen>(parent, pixelScreen));
    }
    void RemoveFromPixelScreen(PixelGameObject parent, PixelScreen pixelScreen)
    {
        // Layers.Remove(new KeyValuePair<PixelGameObject, Dictionary<int, Pixel>>(parent, pixelScreen.grid.Dictionary));
        Layers.Remove(new KeyValuePair<PixelGameObject, PixelScreen>(parent, pixelScreen));
    }  


    // return FIRST pixel screen found for pixelgameobject.
    public PixelScreen this[PixelGameObject parent]
    {
        get
        {
            foreach(var pxs in Layers)
                if(pxs.Key == parent)
                    return pxs.Value;
            throw new MoonSharp.Interpreter.ScriptRuntimeException("Screen for parent doesn't exist. Probably means you forgot an <end> somewhere.");
        }
    }

    // i am so sorry...
    public List<KeyValuePair<PixelGameObject,List<KeyValuePair<PixelPosition, Pixel>>>> GetPixelsWithColliderOtherThan(PixelGameObject pgo)
    {
        List<KeyValuePair<PixelGameObject,List<KeyValuePair<PixelPosition, Pixel>>>> pixels = new();

        foreach (KeyValuePair<PixelGameObject, PixelScreen> layer in Layers)
        {
            if (!layer.Key.Equals(pgo))
            {
                List<KeyValuePair<PixelPosition, Pixel>> p = new();
                foreach (KeyValuePair<int, Pixel> pixel in layer.Value.grid.Dictionary)
                {
                    if (pixel.Value.Collider != null)
                    {
                        p.Add(new KeyValuePair<PixelPosition, Pixel>(pixel.Key + layer.Key.position, pixel.Value));
                    }
                }
                pixels.Add(new KeyValuePair<PixelGameObject,List<KeyValuePair<PixelPosition, Pixel>>>(layer.Key,p));
                
            }
        }
        return pixels;
    }

    public List<KeyValuePair<PixelPosition, Pixel>> GetPixelsWithColliderAfterTranslation(PixelGameObject pgo, PixelPosition translation)
    {
        List<KeyValuePair<PixelPosition, Pixel>> pixels = new List<KeyValuePair<PixelPosition, Pixel>>();
        
        foreach (KeyValuePair<PixelGameObject, PixelScreen> layer in Layers)
        {
            if (layer.Key.Equals(pgo))
            {
                foreach (KeyValuePair<int, Pixel> pixel in layer.Value.grid.Dictionary)
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
    public List<Pixel> GetSpritePixelsAtPosition(PixelPosition translation)
    {
        List<Pixel> pixels = new List<Pixel>();

        foreach(KeyValuePair<PixelGameObject, PixelScreen> layer in Layers)
        {
            foreach(KeyValuePair<int, Pixel> pixel in layer.Value.grid.Dictionary)
            {
                if ((layer.Key.position + pixel.Key) == (translation))
                {
                    pixels.Add(pixel.Value);
                }
            }
        }

        return pixels;
    }

}