using UnityEngine;
using System.Collections;

public static class SpriteRendererExtensions
{
    public static IEnumerator FadeOut(this SpriteRenderer spriteRenderer, float duration)
    {
        float alpha = 1f;
        float rate = 1f / duration;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * rate;
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }
    }
    public static IEnumerator FadeIn(this SpriteRenderer spriteRenderer, float duration)
    {
        float alpha = 0f;
        float rate = 1f / duration;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * rate;
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
            yield return null;
        }
    }
}