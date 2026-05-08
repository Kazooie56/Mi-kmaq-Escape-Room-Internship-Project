using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class CollectableUI : MonoBehaviour
{
    public static CollectableUI Instance;

    public TMP_Text nameText;
    public Image iconImage;
    public TMP_Text descriptionText;
    public CanvasGroup canvasGroup;

    public float fadeDuration = 0.27f;
    public float visibleTime = 1.5f;

    private Coroutine fadeRoutine;

    // create UI and make it invisible
    void Awake()
    {
        Instance = this;
        canvasGroup.alpha = 0f;
    }

    //// code for whenever any amount is added.
    //public void AddCollectable(int amount)
    //{
    //    count += amount;
    //    //countText.text = count.ToString();

    //    // this still stops the coroutine, but it doesn't matter
    //    if (fadeRoutine != null)
    //    {
    //        StopCoroutine(fadeRoutine);
    //    }

    //    fadeRoutine = StartCoroutine(UIPopup());
    //}

    public void ShowCollectable(ItemData data)
    {
        nameText.text = "Object found: " + data.displayName;
        iconImage.sprite = data.icon;
        descriptionText.text = data.description;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(UIPopup());
    }

    IEnumerator UIPopup()
    {
        // fade in
        if (canvasGroup.alpha < 1f)                     // if it's not fully opaque
        {
            yield return Fade(canvasGroup.alpha, 1f);   // fade in until it is
        }

        float timer = visibleTime;
        while (timer > 0f)                  // wait for as long as timer / visibleTime is not at zero
        {
            timer -= Time.deltaTime;
            yield return null;              // do nothing, aka wait
        }

        // fade out
        yield return Fade(1f, 0f);
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = end;
    }
}
