using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    public Renderer rend;
    public Material lightMaterial;
    public Material darkMaterial;

    const float FADE_TIME = 2f;

    void OnEnable() {
        StartCoroutine(FadingIn());
    }

    IEnumerator FadingIn() {
        for (float colorIndex = 1f; colorIndex >= 0; colorIndex -= (Time.deltaTime / FADE_TIME)) {
            rend.material.Lerp(lightMaterial, darkMaterial, colorIndex);
            yield return null;
        }
    }
}
