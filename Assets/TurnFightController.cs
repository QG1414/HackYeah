using SteelLotus.Core;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFightController : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private PlayerFightController playerFightController;

    [SerializeField]
    private EvolutionStep playerEvolutionStep;

    [SerializeField]
    private AttackAnimations attackAnimations;

    [SerializeField]
    private GameOverScreen gameOverScreen;

    private FightMainController fightMainControler;

    private bool playerTurn = true;
    private bool fightInProgress = false;

    public PlayerFightController PlayerFight { get => playerFightController; set => playerFightController = value; }
    public EnemyController Enemy { get => enemyController; set => enemyController = value; }
    public AttackAnimations AttackAnim { get => attackAnimations; set => attackAnimations = value; }
    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }

    private void Start()
    {
        fightMainControler = MainGameController.Instance.GetFieldByType<FightMainController>();

        enemyController.Init(fightMainControler.EnemyDataProperty[fightMainControler.EnemyIndex], this);
        playerFightController.Init(playerEvolutionStep,this);
    }


    public void StartFight()
    {
        fightInProgress = true;
    }

    public void PassMove()
    {
        playerTurn = !playerTurn;

        if (!enemyController.CheckIfAlive())
        {
            fightMainControler.OnPlayerWin();
            return;
        }

        if (!playerFightController.CheckIfAlive())
        {
            playerFightController.BlockInteraction(false);
            gameOverScreen.StartScreen();
            return;
        }

        if (!playerTurn)
        {
            enemyController.Attack();
        }
        else
        {
            playerFightController.BlockInteraction(false);
        }

    }
}
