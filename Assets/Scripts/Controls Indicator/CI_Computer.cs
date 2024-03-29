using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CI_Computer : MonoBehaviour
{
    public List<SpriteRenderer> SpriteRenderers;
    public InComputerBounds inComputerBounds;
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
        if(GameState.Instance.CurrGameState == GameStates.NearComputer && !inComputerBounds.SeenControlIndicator && !_SeenControlIndicator)
        {
            // gameObject.SetActive(false);
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeIn(Time));
            _SeenControlIndicator = true;
        }
        if(!(GameState.Instance.CurrGameState == GameStates.NearComputer) && !inComputerBounds.SeenControlIndicator && _SeenControlIndicator)
        {
            // gameObject.SetActive(false);
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeOut(Time));
            _SeenControlIndicator = false;
        }
        if(GameState.Instance.CurrGameState != GameStates.InComputer && _SeenControlIndicator && inComputerBounds.SeenControlIndicator)
        {
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeOut(Time));
            // GameState.Instance.CurrGameState = GameStates.InComputer;

            SceneManager.LoadScene("Scene_1");
        }
    }
}
