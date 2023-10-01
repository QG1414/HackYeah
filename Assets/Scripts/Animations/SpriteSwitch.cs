using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SteelLotus.Animation
{
    public class SpriteSwitch : MonoBehaviour
    {
        [SerializeField]
        private Image imageToChange;

        [SerializeField]
        private List<Sprite> spritesToChange = new List<Sprite>();

        [SerializeField]
        private float changeTime;

        private int currentIndex = 0;

        private Coroutine animationCoroutine;

        public void StartAnimation()
        {
            animationCoroutine = StartCoroutine(WaitForSpriteChange());
        }

        public void StartAnimationOneTime(TweenCallback tweenCallback)
        {
            imageToChange.color = new Color(imageToChange.color.r, imageToChange.color.g, imageToChange.color.b, 1);
            StartCoroutine(WaitForSpriteChangeOneTime(tweenCallback));
        }

        public void StopAnimation()
        {
            StopCoroutine(animationCoroutine);
        }

        private IEnumerator WaitForSpriteChange()
        {
            while (true)
            {
                imageToChange.sprite = spritesToChange[currentIndex];

                yield return new WaitForSecondsRealtime(changeTime);

                currentIndex++;

                if (currentIndex == spritesToChange.Count)
                    currentIndex = 0;
            }
        }

        private IEnumerator WaitForSpriteChangeOneTime(TweenCallback tweenCallback)
        {
            for (int i = 0; i < spritesToChange.Count; i++)
            {
                imageToChange.sprite = spritesToChange[i];

                yield return new WaitForSecondsRealtime(changeTime);
            }

            imageToChange.color = new Color(imageToChange.color.r, imageToChange.color.g, imageToChange.color.b, 0);

            if (tweenCallback != null)
                tweenCallback.Invoke();
        }
    }
}
