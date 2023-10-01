using SteelLotus.Core;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteelLotus.Sounds;

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

    private SoundManager soundManager;

    private bool playerTurn = true;
    private bool fightInProgress = false;

    public PlayerFightController PlayerFight { get => playerFightController; set => playerFightController = value; }
    public EnemyController Enemy { get => enemyController; set => enemyController = value; }
    public AttackAnimations AttackAnim { get => attackAnimations; set => attackAnimations = value; }
    public bool PlayerTurn { get => playerTurn; set => playerTurn = value; }

    private void Start()
    {
        fightMainControler = MainGameController.Instance.GetFieldByType<FightMainController>();
        soundManager = MainGameController.Instance.GetFieldByType<SoundManager>();


        enemyController.Init(fightMainControler.EnemyDataProperty[fightMainControler.EnemyIndex], this);
        playerFightController.Init(playerEvolutionStep,this);
    }


    public void StartFight()
    {
        fightInProgress = true;
        soundManager.PlayClip(soundManager.MusicSource, soundManager.MusicCollection.clips[1], true);
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
            soundManager.StopAudio(soundManager.MusicSource);
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
