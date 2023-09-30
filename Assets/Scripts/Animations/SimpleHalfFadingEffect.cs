using NaughtyAttributes;
using SteelLotus.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SteelLotus.Animation
{
    public class SimpleHalfFadingEffect : MonoBehaviour
    {

        [SerializeField]
        private AnimationType animationType;

        [SerializeField, ShowIf(nameof(animationType), AnimationType.CanvasGroup)]
        private CanvasGroup canvasGroupToAnimate;

        [SerializeField, ShowIf(nameof(animationType), AnimationType.Image)]
        private Image imageToAnimate;

        [SerializeField, ShowIf(nameof(animationType), AnimationType.Text)]
        private TMP_Text textToAnimate;

        [SerializeField, Range(0, 1)]
        private float startValue;

        [SerializeField, Range(0, 1)]
        private float endValue;

        [SerializeField]
        private float animationTime;

        private AddictionalMethods addictionalMethods;

        public void StartAnimation()
        {
            addictionalMethods = MainGameController.Instance.GetFieldByType<AddictionalMethods>();

            AnimateIn();
        }

        private void AnimateIn()
        {
            switch (animationType)
            {
                case AnimationType.Image:
                    addictionalMethods.FadeElement(animationTime, imageToAnimate, startValue, endValue, (() => AnimateOut()));
                    break;
                case AnimationType.Text:
                    addictionalMethods.FadeElement(animationTime, textToAnimate, startValue, endValue, (() => AnimateOut()));
                    break;
                case AnimationType.CanvasGroup:
                    addictionalMethods.FadeElement(animationTime, canvasGroupToAnimate, startValue, endValue, (() => AnimateOut()));
                    break;
            }

        }

        private void AnimateOut()
        {
            switch (animationType)
            {
                case AnimationType.Image:
                    addictionalMethods.FadeElement(animationTime, imageToAnimate, endValue, startValue, (() => AnimateIn()));
                    break;
                case AnimationType.Text:
                    addictionalMethods.FadeElement(animationTime, textToAnimate, endValue, startValue, (() => AnimateIn()));
                    break;
                case AnimationType.CanvasGroup:
                    addictionalMethods.FadeElement(animationTime, canvasGroupToAnimate, endValue, startValue, (() => AnimateIn()));
                    break;
            }
        }

        private enum AnimationType
        {
            Image,
            CanvasGroup,
            Text
        }
    }
}
