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

    [SerializeField]
    private List<EnemyData> enemyData = new List<EnemyData>();

    private InitializeSkills initializeSkills;

    private ScenesController scenesController;

    private int enemyIndex = 0;

    public int EnemyIndex { get => enemyIndex; set => enemyIndex = value; }
    public List<EnemyData> EnemyDataProperty { get => enemyData; set => enemyData = value; }

    private int currentFightWithEnemy = 0;

    public void Init(ScenesController scenesController)
    {
        this.scenesController = scenesController;
        this.enemyIndex = currentFightWithEnemy;
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
