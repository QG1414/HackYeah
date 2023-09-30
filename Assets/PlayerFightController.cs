using SteelLotus.Animation;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private EvolutionStep playerState;

    private float playerHP;

    TurnFightController turnFightController;

    private bool inDefenseMode = false;
    private float demageReduction;

    public void Init(EvolutionStep playerState, TurnFightController turnFightController)
    {
        this.turnFightController = turnFightController;
        this.playerState = playerState;
        playerHP = this.playerState.HP;
        playerImage.sprite = playerState.DinosourSprite;
    }

    public void Attack(float howMuch)
    {
        if (howMuch == 0)
            return;

        turnFightController.Enemy.GetHit(howMuch);
    }

    public void Heal(float howMuch)
    {
        if (howMuch == 0)
            return;

        if (playerHP + howMuch > playerState.HP)
        {
            playerHP = playerState.HP;
        }

        playerHP += howMuch;
    }

    public void Defense(float howMuch)
    {
        if (howMuch == 0)
            return;

        inDefenseMode = true;
        demageReduction = howMuch;
    }

    public void GetHit(float howMuch)
    {

        if(inDefenseMode)
        {
            inDefenseMode = false;
            float demageSent = howMuch - howMuch * (demageReduction / 100f);
            howMuch = howMuch * (demageReduction / 100f);
            turnFightController.Enemy.GetHit(demageSent);
        }

        playerHP -= howMuch;

        Debug.Log($"Player lost: {howMuch}hp now he has {playerHP}");

        if (playerHP < 0)
        {
            playerHP = 0;
        }
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
