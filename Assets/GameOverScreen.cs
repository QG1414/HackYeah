using NaughtyAttributes;
using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField, Scene]
    private string mainMenuScreen;

    [SerializeField, Scene]
    private string loadingScreen;

    [SerializeField, Scene]
    private string gameScene;

    ScenesController scenesController;

    private void Start()
    {
        scenesController = MainGameController.Instance.GetFieldByType<ScenesController>();

        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }


    public void StartScreen()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void RestartLevel()
    {
        MainGameController.Instance.ResetData();
        scenesController.StartTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, () =>
        {
            SceneManager.LoadScene(gameScene);
            scenesController.EndTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, null);
        });
    }

    public void BackToMenu()
    {
        MainGameController.Instance.ResetData();

        scenesController.NextSceneToLoad = mainMenuScreen;

        scenesController.StartTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, () =>
        {
            SceneManager.LoadScene(loadingScreen);
            scenesController.EndTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, null);
        });
    }
}
