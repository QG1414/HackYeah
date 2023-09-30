using DG.Tweening;
using NaughtyAttributes;
using SteelLotus.Animation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SteelLotus.Animation
{
    public class AnimatedMainMenu : MonoBehaviour
    {
        [SerializeField]
        private GraphicRaycaster interactionsBlocker;



        [SerializeField, BoxGroup("Title Animation")]
        private AnimatedUI titleAnimation;

        [SerializeField, BoxGroup("Title Animation")]
        private Vector2 titleStartPosition;

        [SerializeField, BoxGroup("Title Animation")]
        private Vector2 titleEndPosition;



        [ReorderableList, SerializeField, BoxGroup("Buttons Animation")]
        private List<AnimatedUI> buttonsAnimations = new List<AnimatedUI>();

        [SerializeField, BoxGroup("Buttons Animation")]
        private Vector3 startingPosition;

        [SerializeField, BoxGroup("Buttons Animation")]
        private Vector3 endingPosition;

        [SerializeField, BoxGroup("Buttons Animation")]
        private float heightAddition;



        [SerializeField, BoxGroup("Settings Animation")]
        private AnimatedUI settingsAnimation;

        [SerializeField, BoxGroup("Settings Animation")]
        private Vector2 settingsStartPosition;

        [SerializeField, BoxGroup("Settings Animation")]
        private Vector2 settingsEndPosition;



        [SerializeField, BoxGroup("Credits Animation")]
        private AnimatedUI creditsAnimation;

        [SerializeField, BoxGroup("Credits Animation")]
        private Vector2 creditsStartPosition;

        [SerializeField, BoxGroup("Credits Animation")]
        private Vector2 creditsEndPosition;



        [SerializeField]
        private AnimatedUI InfoBoxAnimation;



        private int direction = 1;

        private Vector2 tempEndingPosition;

        private Queue<AnimatedUI> animationOrder = new Queue<AnimatedUI>();



        #region Title&ButtonsAnimation

        public void StartButtonAnimationIn()
        {
            interactionsBlocker.enabled = false;

            direction = 1;
            tempEndingPosition = endingPosition;
            animationOrder.Clear();

            foreach (var button in buttonsAnimations)
            {
                animationOrder.Enqueue(button);
            }

            NextStepAnimation(startingPosition, tempEndingPosition, direction, 0);
        }

        private void NextStepAnimation(Vector2 startingPosition, Vector2 endingPosition, int direction, int animatorNumber)
        {
            if (animationOrder.Count == 0)
            {
                titleAnimation.SetActionToStartAfterAnimationEnd(() => interactionsBlocker.enabled = true);
                if (animatorNumber == 0)
                {
                    titleAnimation.StartRectMovementAnimation(titleStartPosition, titleEndPosition, animatorNumber);
                }
                else
                    titleAnimation.StartRectMovementAnimation(titleEndPosition, titleStartPosition, animatorNumber);

                return;
            }

            AnimatedUI currentButton = animationOrder.Dequeue();

            Vector2 newStartingPosition = new Vector2(startingPosition.x, startingPosition.y + heightAddition);
            Vector2 newEndingPosition = new Vector2(endingPosition.x, endingPosition.y + heightAddition);

            currentButton.SetActionToStartAfterAnimationEnd(() => NextStepAnimation(newStartingPosition, newEndingPosition, direction *= -1, animatorNumber));

            currentButton.StartRectMovementAnimationX(startingPosition.x * direction, endingPosition.x * direction, animatorNumber);
        }

        public void StartButtonAnimationOut()
        {
            interactionsBlocker.enabled = false;

            direction = -1;
            tempEndingPosition = endingPosition;
            animationOrder.Clear();

            foreach (var button in buttonsAnimations)
            {
                animationOrder.Enqueue(button);
            }

            NextStepAnimation(tempEndingPosition, startingPosition, direction, 1);
        }

        #endregion Title&ButtonsAnimation


        #region SettingsAnimation

        public void HoverSettings(bool open)
        {
            interactionsBlocker.enabled = false;
            settingsAnimation.SetActionToStartAfterAnimationEnd(() => interactionsBlocker.enabled = true);

            if (open)
            {
                settingsAnimation.StartRectMovementAnimation(settingsStartPosition, settingsEndPosition, 0);
            }
            else
            {
                settingsAnimation.StartRectMovementAnimation(settingsEndPosition, settingsStartPosition, 1);
            }
        }

        #endregion SettingsAnimation


        #region CreditsAnimation

        public void HoverCredits(bool open)
        {
            interactionsBlocker.enabled = false;
            creditsAnimation.SetActionToStartAfterAnimationEnd(() => interactionsBlocker.enabled = true);

            if (open)
            {
                creditsAnimation.StartRectMovementAnimation(creditsStartPosition, creditsEndPosition, 0);
            }
            else
            {
                creditsAnimation.StartRectMovementAnimation(creditsEndPosition, creditsStartPosition, 1);
            }
        }

        #endregion CreditsAnimation


        #region InfoBoxAnimation

        public void HoverInfoBox(bool open)
        {
            interactionsBlocker.enabled = false;
            InfoBoxAnimation.SetActionToStartAfterAnimationEnd(() => interactionsBlocker.enabled = true);

            if (open)
            {
                InfoBoxAnimation.StartRectMovementAnimation(creditsStartPosition, creditsEndPosition, 0);
            }
            else
            {
                InfoBoxAnimation.StartRectMovementAnimation(creditsEndPosition, creditsStartPosition, 1);
            }
        }

        #endregion InfoBoxAnimation
    }
}
