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

    [SerializeField, Scene]
    private string mainGameScene;

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

    public void IncreaseEnemyIndex()
    {
        this.enemyIndex++;
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
        MainGameController.Instance.SaveData();
        scenesController.StartTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, () => { SceneManager.LoadScene(fightingScene); scenesController.EndTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, null); });
    }

    public void OnPlayerWin()
    {
        MainGameController.Instance.DoNotReset = true;
        IncreaseEnemyIndex();
        scenesController.StartTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, () => { SceneManager.LoadScene(mainGameScene); scenesController.EndTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, null); });
    }

}
