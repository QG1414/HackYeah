using NaughtyAttributes;
using SteelLotus.Core;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightMainController : MonoBehaviour
{
    [SerializeField]
    private EvolutionStep playerEvolutionStep;

    [SerializeField, Scene]
    private string fightingScene;

    [SerializeField, Scene]
    private string loadingScene;

    private InitializeSkills initializeSkills;

    private ScenesController scenesController;

    public void Init(ScenesController scenesController)
    {
        this.scenesController = scenesController;
    }


    public void InitUI(InitializeSkills initializeSkills)
    {
        this.initializeSkills = initializeSkills;
    }

    public void StartFight()
    {
        initializeSkills.CreateSkills(playerEvolutionStep);
    }

    public void LoadFightScene()
    {
        scenesController.StartTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, () => { SceneManager.LoadScene(fightingScene); scenesController.EndTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, null); });
    }

}
