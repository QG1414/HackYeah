using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using SteelLotus.Sounds;
using SteelLotus.Core;
using SteelLotus.Animation;

namespace SteelLotus.Intro
{
    public class SteelLotusIntroController : MonoBehaviour
    {

        [SerializeField]
        VideoPlayer player;

        [SerializeField, Scene]
        private string loadingScene;

        [SerializeField, Scene]
        private string afterLoadingScene;

        private bool delayInActive = false;
        private bool videoFinished = false;

        SoundManager soundManager;
        ScenesController scenesController;

        private void Start()
        {
            soundManager = MainGameController.Instance.GetFieldByType<SoundManager>();
            scenesController = MainGameController.Instance.GetFieldByType<ScenesController>();

            StartCoroutine(WaitWithDelay());

            soundManager.PlayOneShoot(soundManager.EnviromentSource, soundManager.EnviromentCollection.clips[0], 1f);
        }

        private void Update()
        {
            if (!videoFinished && delayInActive && !player.isPlaying)
            {
                VideoFinished();
            }
        }

        private IEnumerator WaitWithDelay()
        {
            yield return new WaitForSeconds(0.5f);
            delayInActive = true;
        }

        private void VideoFinished()
        {
            videoFinished = true;
            scenesController.NextSceneToLoad = afterLoadingScene;
            scenesController.WaitForInputAfterLoad = false;
            scenesController.StartTransition(AnimationTypes.AnchoreMovement, () => SceneManager.LoadScene(loadingScene));
            soundManager.PlayClip(soundManager.EnviromentSource, soundManager.EnviromentCollection.clips[1], true);

        }
    }
}
