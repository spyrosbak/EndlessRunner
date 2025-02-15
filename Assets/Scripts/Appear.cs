using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Appear : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI graphic;
    private float fadeTime = 3.0f;

    private void Update()
    {
        StartCoroutine(FadeOut(graphic));
    }

    IEnumerator FadeOut(TextMeshProUGUI text)
    {
        float elapsedTime = 0.0f;
        Color c = text.color;
        while (elapsedTime < fadeTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / fadeTime);
            text.color = c;
        }
    }
}