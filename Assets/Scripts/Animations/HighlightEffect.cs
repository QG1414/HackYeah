using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightEffect : MonoBehaviour
{
    [SerializeField]
    private Image imageToAnimate;

    [SerializeField]
    private Color startColor;

    [SerializeField]
    private Color endColor;

    [SerializeField]
    private Color boughtUpgradeColor;

    [SerializeField]
    private float switchingTime;

    [SerializeField]
    private bool highlightOnStart = false;

    Coroutine animationCoroutine = null;

    private Color newColor;

    private bool bought = false;

    private void Start()
    {
        if(highlightOnStart)
        {
            UnlockUnlockedColor();
        }
    }

    public void StartAnimation()
    {

        if (bought)
            return;

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
            imageToAnimate.color = startColor;
        }
        animationCoroutine = StartCoroutine(Animation());
    }

    public void StopAnimation()
    {
        if (bought)
            return;

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        imageToAnimate.color = startColor;
    }

    public void UnlockUnlockedColor()
    {
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
            animationCoroutine = null;
        }

        imageToAnimate.color = boughtUpgradeColor;
        bought = true;
    }

    private IEnumerator Animation()
    {
        while (true)
        {
            newColor = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time, switchingTime));
            imageToAnimate.color = newColor;
            yield return null;
        }
    }
}