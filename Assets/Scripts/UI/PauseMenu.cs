using NaughtyAttributes;
using SteelLotus.Animation;
using SteelLotus.Core;
using SteelLotus.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    private GraphicRaycaster interactionBlocker;

    [SerializeField]
    private AnimatedUI escapePanelAnimation;

    [SerializeField]
    private Vector2 startingPosition;

    [SerializeField]
    private Vector2 endingPosition;

    [SerializeField]
    private AnimatedUI settingsAnimation;

    [SerializeField]
    private Vector2 startingPositionSettings;

    [SerializeField]
    private Vector2 endingPositionSettings;

    [SerializeField, Scene]
    private string loadingScene;

    [SerializeField, Scene]
    private string mainMenuScene;



    private bool gamePaused = false;

    private bool blockInteractions = false;
    private bool settingsActive = false;

    public bool GamePaused { get => gamePaused; set => gamePaused = value; }

    private ScenesController scenesController;


    private void Start()
    {
        interactionBlocker.enabled = false;
        scenesController = MainGameController.Instance.GetFieldByType<ScenesController>();
    }

    private void Update()
    {
        if (!blockInteractions && Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseMenu();
        }
    }

    public void ChangePauseMenu()
    {
        blockInteractions = true;
        escapePanelAnimation.SetActionToStartAfterAnimationEnd(() => blockInteractions = false);
        if (!gamePaused)
        {
            escapePanelAnimation.StartRectMovementAnimation(startingPosition, endingPosition, 0);
        }
        else
        {
            escapePanelAnimation.StartRectMovementAnimation(endingPosition, startingPosition, 1);
        }
        gamePaused = !gamePaused;

        if(gamePaused)
        {
            interactionBlocker.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            interactionBlocker.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        ChangePauseMenu();
    }

    public void OpenSettings()
    {
        settingsActive = !settingsActive;
        if(settingsActive)
        {
            blockInteractions = true;
            settingsAnimation.StartRectMovementAnimation(startingPositionSettings, endingPositionSettings, 0);
        }
        else
        {
            settingsAnimation.SetActionToStartAfterAnimationEnd(() => blockInteractions = false);
            settingsAnimation.StartRectMovementAnimation(endingPositionSettings, startingPositionSettings, 1);
        }
    }

    public void BackToMainScreen()
    {
        Time.timeScale = 1;
        scenesController.NextSceneToLoad = mainMenuScene;
        scenesController.WaitForInputAfterLoad = false;
        scenesController.StartTransition(AnimationTypes.AnchoreMovement, () => { SceneManager.LoadScene(loadingScene); scenesController.EndTransition(AnimationTypes.AnchoreMovement, null); });
    }

    public void Exit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

}
