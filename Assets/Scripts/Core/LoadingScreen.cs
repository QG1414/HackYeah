using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;
using SteelLotus.Sounds;
using SteelLotus.Animation;

namespace SteelLotus.Core
{
    public class LoadingScreen : MonoBehaviour
    {

        [SerializeField, BoxGroup("Button Values")]
        private CanvasGroup buttonCanvasGroup;


        [SerializeField, BoxGroup("Loading Elements")]
        private CanvasGroup AnimationLogo;

        [SerializeField, BoxGroup("Loading Elements")]
        private SimpleHalfFadingEffect textFadingEffect;


        [SerializeField, BoxGroup("Animation Values")]
        private float animationTime;

        [SerializeField, BoxGroup("Animation Values")]
        private float startDelay;


        private AsyncOperation operation;

        private bool readyToChangeScene = false;

        private SoundManager soundManager;
        private AddictionalMethods addictionalMethods;
        private ScenesController scenesController;

        #region Initialization

        void Start()
        {
            scenesController = MainGameController.Instance.GetFieldByType<ScenesController>();
            soundManager = MainGameController.Instance.GetFieldByType<SoundManager>();
            addictionalMethods = MainGameController.Instance.GetFieldByType<AddictionalMethods>();

            scenesController.EndTransition(AnimationTypes.AnchoreMovement, () => StartCoroutine(StartLoadingGame()));
            SetupLoadingScreen();
        }

        private void SetupLoadingScreen()
        {
            buttonCanvasGroup.alpha = 0;
        }

        #endregion Initialization

        private void Update()
        {
            if (readyToChangeScene == true && Input.anyKeyDown)
            {
                soundManager.StopAudio(soundManager.EnviromentSource);
                StartMainGame();
                readyToChangeScene = false;
            }
        }


        #region Loading

        private IEnumerator StartLoadingGame()
        {
            operation = SceneManager.LoadSceneAsync(scenesController.NextSceneToLoad);
            operation.allowSceneActivation = false;
            while (operation.progress < 0.9f)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(startDelay);

            addictionalMethods.FadeElement(animationTime, AnimationLogo, 0f, (() => ActivateNextButton()));
        }

        private void ActivateNextButton()
        {
            if (scenesController.WaitForInputAfterLoad)
            {
                addictionalMethods.FadeElement(animationTime, buttonCanvasGroup, 1f, (() => ActivateTextAndChangePanel()));
            }
            else
            {
                soundManager.StopAudio(soundManager.EnviromentSource);
                StartMainGame();
            }
            scenesController.WaitForInputAfterLoad = true;
        }

        #endregion Loading

        #region LoadingButtons

        private void ActivateTextAndChangePanel()
        {
            textFadingEffect.StartAnimation();
            readyToChangeScene = true;
        }

        private void StartMainGame()
        {
            scenesController.StartTransition(AnimationTypes.AnchoreMovement, () => { operation.allowSceneActivation = true; scenesController.EndTransition(AnimationTypes.AnchoreMovement, null); });
        }

        #endregion LoadingButtons

    }
}
