using SteelLotus.Animation;
using SteelLotus.Dino.Evolution;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SteelLotus.Sounds;
using SteelLotus.Core;

public class PlayerFightController : MonoBehaviour
{
    [SerializeField]
    private Image playerImage;

    [SerializeField]
    private AnimatedUI skillsAnimation;

    [SerializeField]
    private Vector2 skillStartPosition;

    [SerializeField]
    private Vector2 skillEndPosition;

    [SerializeField]
    private GraphicRaycaster graphicRaycaster;

    [SerializeField]
    private Image playerHealthImage;


    private EvolutionStep playerState;

    private SoundManager soundManager;

    private float playerHP;

    TurnFightController turnFightController;

    private bool inDefenseMode = false;
    private float demageReduction;

    public Image PlayerHealthImage { get => playerHealthImage; set => playerHealthImage = value; }

    public void Init(EvolutionStep playerState, TurnFightController turnFightController)
    {
        soundManager = MainGameController.Instance.GetFieldByType<SoundManager>();
        this.turnFightController = turnFightController;
        this.playerState = playerState;
        playerHP = this.playerState.HP;
        playerImage.sprite = playerState.DinosourSprite;
    }

    public void Attack(float howMuch, bool strong = false)
    {
        if (howMuch == 0)
            return;

        BlockInteraction(true);
        turnFightController.AttackAnim.PlayAnimation(SteelLotus.Dino.Evolution.SkillTypes.Attack, strong, howMuch, turnFightController.Enemy.CalculatePercentageHealth(howMuch), turnFightController.Enemy.EnemyHealthImage);
        soundManager.PlayClip(soundManager.PlayerSource, soundManager.PlayerCollection.clips[1], false);
        turnFightController.Enemy.GetHit(howMuch);
    }

    public void Heal(float howMuch)
    {
        if (howMuch == 0)
            return;

        BlockInteraction(true);

        if (playerHP + howMuch > playerState.HP)
        {
            playerHP = playerState.HP;
        }

        playerHP += howMuch;

        turnFightController.AttackAnim.PlayAnimation(SteelLotus.Dino.Evolution.SkillTypes.Heal, false, howMuch, (playerHP / playerState.HP) , PlayerHealthImage);
    }

    public void Defense(float howMuch)
    {
        if (howMuch == 0)
            return;

        BlockInteraction(true);

        inDefenseMode = true;
        demageReduction = howMuch;
        soundManager.PlayClip(soundManager.PlayerSource, soundManager.PlayerCollection.clips[0], false);
        turnFightController.AttackAnim.PlayAnimation(SteelLotus.Dino.Evolution.SkillTypes.Defense, false, howMuch, 0, PlayerHealthImage);
    
    }

    public void GetHit(float howMuch)
    {

        if(inDefenseMode)
        {
            inDefenseMode = false;
            float demageSent = howMuch - howMuch * (demageReduction / 100f);
            howMuch = howMuch * (demageReduction / 100f);
            turnFightController.AttackAnim.PlayAnimation(SteelLotus.Dino.Evolution.SkillTypes.Attack, false, demageSent, turnFightController.Enemy.CalculatePercentageHealth(demageSent), turnFightController.Enemy.EnemyHealthImage, true);
            soundManager.PlayClip(soundManager.PlayerSource, soundManager.PlayerCollection.clips[2], false);
            turnFightController.Enemy.GetHit(demageSent);
        }

        playerHP -= howMuch;

        if (playerHP < 0)
        {
            playerHP = 0;
        }
    }

    public float CalculatePercentageHealth(float howMuch)
    {

        float tempHealth = playerHP;

        if (inDefenseMode)
        {
            float demageSent = howMuch - howMuch * (demageReduction / 100f);
            howMuch = howMuch * (demageReduction / 100f);
        }

        tempHealth -= howMuch;

        if (tempHealth < 0)
        {
            tempHealth = 0;
        }

        tempHealth = tempHealth / playerState.HP;

        return tempHealth;
    }

    public bool CheckIfAlive()
    {
        return playerHP > 0;
    }

    public void BlockInteraction(bool block)
    {
        if (block)
        {
            graphicRaycaster.enabled = false;
            skillsAnimation.StartRectMovementAnimation(skillEndPosition, skillStartPosition, 1);
        }
        else
        {
            skillsAnimation.SetActionToStartAfterAnimationEnd(() => graphicRaycaster.enabled = true);
            skillsAnimation.StartRectMovementAnimation(skillStartPosition, skillEndPosition, 0);
        }
    }

    public void Pass()
    {
        turnFightController.PassMove();
    }
}
