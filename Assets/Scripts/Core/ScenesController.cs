using DG.Tweening;
using UnityEngine;
using SteelLotus.Animation;


namespace SteelLotus.Core
{
    public class ScenesController : MonoBehaviour
    {
        [SerializeField]
        private AnimatedUI transitionAnimation;

        private string nextSceneToLoad = "";

        public string NextSceneToLoad { get => nextSceneToLoad; set => nextSceneToLoad = value; }

        private bool waitForInputAfterLoad = false;

        public bool WaitForInputAfterLoad { get => waitForInputAfterLoad; set => waitForInputAfterLoad = value; }

        public void StartTransition(AnimationTypes animationType, TweenCallback tweenCallback)
        {
            if (animationType == AnimationTypes.AnchoreMovement)
            {
                if (tweenCallback != null)
                    transitionAnimation.SetActionToStartAfterAnimationEnd(tweenCallback);

                transitionAnimation.StartRectMovementAnimation(new Vector2(1920, 0), new Vector2(0, 0), 0);
            }
            else if (animationType == AnimationTypes.CanvasFade)
            {
                if (tweenCallback != null)
                    transitionAnimation.SetActionToStartAfterAnimationEnd(tweenCallback);

                transitionAnimation.CanvasGroupFadeData.CanvasToFade.alpha = 0;
                transitionAnimation.RectMovementAnimationData[0].ObjectTransform.anchoredPosition = new Vector2(0, 0);
                transitionAnimation.StartCanvasGroupFadeAnimation(0, 1);
            }
            else if (animationType == AnimationTypes.ImageFade)
            {
                if (tweenCallback != null)
                    transitionAnimation.SetActionToStartAfterAnimationEnd(tweenCallback);
                transitionAnimation.ImageFadeAnimationData.ImageToFade.color = new Color(transitionAnimation.ImageFadeAnimationData.ImageToFade.color.r, transitionAnimation.ImageFadeAnimationData.ImageToFade.color.g, transitionAnimation.ImageFadeAnimationData.ImageToFade.color.b, 0);
                transitionAnimation.RectMovementAnimationData[0].ObjectTransform.anchoredPosition = new Vector2(0, 0);
                transitionAnimation.StartImageFadeAnimation(0, 1);
            }
        }
        public void EndTransition(AnimationTypes animationType, TweenCallback tweenCallback)
        {
            if (animationType == AnimationTypes.AnchoreMovement)
            {
                if (tweenCallback != null)
                    transitionAnimation.SetActionToStartAfterAnimationEnd(tweenCallback);

                transitionAnimation.StartRectMovementAnimation(new Vector2(0, 0), new Vector2(-1920, 0), 1);
            }
            else if (animationType == AnimationTypes.CanvasFade)
            {
                if (tweenCallback != null)
                    transitionAnimation.SetActionToStartAfterAnimationEnd(tweenCallback);

                transitionAnimation.RectMovementAnimationData[0].ObjectTransform.anchoredPosition = new Vector2(0, 0);
                transitionAnimation.CanvasGroupFadeData.CanvasToFade.alpha = 1;
                transitionAnimation.StartCanvasGroupFadeAnimation(1, 0);
            }
            else if (animationType == AnimationTypes.ImageFade)
            {
                if (tweenCallback != null)
                    transitionAnimation.SetActionToStartAfterAnimationEnd(tweenCallback);
                transitionAnimation.ImageFadeAnimationData.ImageToFade.color = new Color(transitionAnimation.ImageFadeAnimationData.ImageToFade.color.r, transitionAnimation.ImageFadeAnimationData.ImageToFade.color.g, transitionAnimation.ImageFadeAnimationData.ImageToFade.color.b, 1);
                transitionAnimation.RectMovementAnimationData[0].ObjectTransform.anchoredPosition = new Vector2(0, 0);
                transitionAnimation.StartImageFadeAnimation(1, 0);
            }
        }
    }
}
