using NaughtyAttributes;
using SteelLotus.Animation;
using SteelLotus.Core;
using SteelLotus.Sounds;
using SteelLotus.Core.SaveLoadSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SteelLotus.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button continueButton;

        [SerializeField]
        private float delayDuration = 1f;
        
        [SerializeField]
        private AnimatedMainMenu mainScreenAnimation;

        [SerializeField]
        private InfoDisplayerBlock infoDisplayerBlock;

        [SerializeField, Scene]
        private string gameScene;

        [SerializeField, Scene]
        private string loadingScene;


        private MainMenuParts currentPart = MainMenuParts.Default;

        private ScenesController scenesController;

        private SoundManager soundManager;

        private bool started = false;




        private IEnumerator Start()
        {
            if (SaveSystem.CheckIfSaveExists())
                continueButton.enabled = true;
            else
                continueButton.enabled = false;

            scenesController = MainGameController.Instance.GetFieldByType<ScenesController>();

            soundManager=MainGameController.Instance.GetFieldByType<SoundManager>();

            soundManager.PlayClip(soundManager.MusicSource, soundManager.MusicCollection.clips[0], true);

            yield return new WaitForSeconds(delayDuration);
            soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[4]);
            mainScreenAnimation.StartButtonAnimationIn();
        }

        #region PublicButtons

        public void ContinueGame()
        {
            LoadGame();
        }

        public void NewGame()
        {
            if (SaveSystem.CheckIfSaveExists())
            {
                infoDisplayerBlock.InitInfoDisplayer("Do you want to start new game ? Old progress will be deleted.", "Yes", "No", () => StartNewGame(), () => mainScreenAnimation.HoverInfoBox(false));
                infoDisplayerBlock.ShowPanel();
                mainScreenAnimation.HoverInfoBox(true);
            }
            else
            {
                StartNewGame();
            }
        }

        public void OpenSettings()
        {
            currentPart = MainMenuParts.Settings;
            soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[4]);
            mainScreenAnimation.StartButtonAnimationOut();
            mainScreenAnimation.HoverSettings(true);
        }

        public void OpenCredits()
        {
            currentPart = MainMenuParts.Credits;
            soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[4]);
            mainScreenAnimation.StartButtonAnimationOut();
            mainScreenAnimation.HoverCredits(true);
        }

        public void Exit()
        {
            infoDisplayerBlock.InitInfoDisplayer("Do you want to exit game ?", "Yes", "No", () => ExitGame(), () => mainScreenAnimation.HoverInfoBox(false));
            infoDisplayerBlock.ShowPanel();
            mainScreenAnimation.HoverInfoBox(true);
        }

        public void BackToMainScreen()
        {
            CheckLocalization();
        }
        #endregion

        #region InsideMethods

        private void CheckLocalization()
        {
            switch (currentPart)
            {
                case MainMenuParts.Settings:
                    mainScreenAnimation.HoverSettings(false);
                    break;
                case MainMenuParts.Credits:
                    mainScreenAnimation.HoverCredits(false);
                    break;
            }

            currentPart = MainMenuParts.Default;
            soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[4]);
            mainScreenAnimation.StartButtonAnimationIn();
        }

        private void ExitGame()
        {
            Application.Quit();
            //soundManager.StopAudio(soundManager.MusicSource);
            soundManager.Mute(soundManager.MusicCollection);
        }

        private void StartNewGame()
        {
            if (started)
                return;
            SaveSystem.DeleteAllSaves();
            //soundManager.StopAudio(soundManager.MusicSource);
            soundManager.Mute(soundManager.MusicCollection);
            LoadGame();
        }

        private void LoadGame()
        {
            if (started)
                return;
            started = true;
            scenesController.WaitForInputAfterLoad = true;
            scenesController.NextSceneToLoad = gameScene;
            //soundManager.StopAudio(soundManager.MusicSource);
           // soundManager.Mute(soundManager.MusicCollection);
            soundManager.MusicSource.Pause();
            scenesController.StartTransition(AnimationTypes.AnchoreMovement, () => { SceneManager.LoadScene(loadingScene); scenesController.EndTransition(AnimationTypes.AnchoreMovement, null); });

        }

        #endregion InsideMethods
    }

    public enum MainMenuParts
    {
        Default,
        Settings,
        Credits
    }
}
