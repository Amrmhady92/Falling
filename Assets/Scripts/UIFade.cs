﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public Image fadePlane;

    public float fadeDuration;
    public Color startColor;
    Color fadedColor;

    private void Start()
    {
        fadedColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        StartCoroutine(FadeOut(fadeDuration));
    }

    IEnumerator FadeOut(float duration) {
        float percent = 1;
        while (percent > 0)
        {
            percent -= duration * Time.deltaTime;
            fadePlane.color = Color.Lerp(fadedColor, startColor,  percent);
            yield return null;
        }
    
    }

    public IEnumerator FadeIn(float duration)
    {
        print("start");
        float percent = 0;
        while (percent < 1)
        {

            print(percent);
            percent += duration * Time.deltaTime;
            fadePlane.color = Color.Lerp(fadedColor, startColor, percent);
            yield return null;
        }

    }
}