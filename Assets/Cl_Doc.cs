using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Cl_Doc : MonoBehaviour
{
    public List<SpriteRenderer> SpriteRenderers;
    public InDocBounds inDocBounds;
    public float Time = 0.5f;
    private bool _SeenControlIndicator = false;
    void Start()
    {
        Color color;
        foreach(var sr in SpriteRenderers)
        {
            color = sr.color;
            color.a = 0;
            sr.color = color;
        }
    }

    private void Update()
    {
        if(GameState.Instance.CurrGameState == GameStates.NearBookShelf && !inDocBounds.SeenControlIndicator && !_SeenControlIndicator)
        {
            // gameObject.SetActive(false);
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeIn(Time));
            _SeenControlIndicator = true;
        }
        if(!(GameState.Instance.CurrGameState == GameStates.NearBookShelf) && !inDocBounds.SeenControlIndicator && _SeenControlIndicator)
        {
            // gameObject.SetActive(false);
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeOut(Time));
            _SeenControlIndicator = false;
        }
        if(GameState.Instance.CurrGameState != GameStates.InComputer && _SeenControlIndicator && inDocBounds.SeenControlIndicator)
        {
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeOut(Time));
            Application.OpenURL("https://github.com/Sanokei/Coots-Bug-Squasher/Documentation.md");

        }
    }
}
