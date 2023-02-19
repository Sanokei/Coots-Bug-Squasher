using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CI_Coots : MonoBehaviour
{
    public List<SpriteRenderer> SpriteRenderers;
    public bool SeenControlIndicator = true;
    public float Time = 0.5f;
    // FIXME: FLAG VARIABLE BAD PRACTICE
    bool _SeenControlIndicator = false;
    void Update()
    {
        if(GameState.Instance.CurrGameState == GameStates.Playing && (!_SeenControlIndicator && SeenControlIndicator))
        {
            // gameObject.SetActive(false);
            foreach(var sr in SpriteRenderers)
                StartCoroutine(sr.FadeOut(Time));
            _SeenControlIndicator = true;
        }
    }
}
