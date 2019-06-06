using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour {
    public static FadeManager instance;
    public SpriteRenderer black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake() {
        instance = this;
    }

    public void FadeOut(float speed = 0.01f) {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(speed));
    }

    IEnumerator FadeOutCoroutine(float speed) {
        color = black.color;

        while(color.a < 1f) {
            color.a += speed;
            black.color = color;
            yield return waitTime;
        }
    }

    public void FadeIn(float speed = 0.01f) {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(speed));
    }

    IEnumerator FadeInCoroutine(float speed) {

        color = black.color;

        while (color.a > 0f) {
            color.a -= speed;
            black.color = color;
            yield return waitTime;
        }
    }

}
