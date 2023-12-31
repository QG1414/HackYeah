using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteelLotus.Animation;
using SteelLotus.Dino.Evolution;
using System;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class AttackAnimations : MonoBehaviour
{
    [SerializeField]
    private TurnFightController turnFightController;

    [SerializeField]
    private SpriteSwitch simpleAttackAnimation;

    [SerializeField]
    private SpriteSwitch strongAttackAnimation;

    [SerializeField]
    private SpriteSwitch HealAnimation;

    [SerializeField]
    private SpriteSwitch DefenseAnimation;

    [SerializeField]
    private TMP_Text damageText;

    [SerializeField]
    private Vector2 damageEndPosition;

    [SerializeField]
    private float jumpPower;

    [SerializeField]
    private int numberOfJumps;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float reducingAnimationDuration;

    [SerializeField]
    private Vector2 playerDamagePosition;

    [SerializeField]
    private Vector2 enemyDamagePosition;

    public void PlayAnimation(SkillTypes skillType, bool strong, float damage, float percentageHealth, Image healthImage, bool ignorePass = false)
    {
        if (skillType == SkillTypes.Attack && turnFightController.PlayerTurn)
        {
            simpleAttackAnimation.transform.position = enemyDamagePosition;
        }
        else if (skillType == SkillTypes.Attack)
        {
            simpleAttackAnimation.transform.position = playerDamagePosition;
        }

        switch(skillType)
        {
            case SkillTypes.Attack:
                if (strong)
                {
                    strongAttackAnimation.StartAnimationOneTime(null);
                    ShowDamageText(strongAttackAnimation,damage, percentageHealth, healthImage, Color.red, ignorePass);
                }
                else
                {
                    simpleAttackAnimation.StartAnimationOneTime(null);
                    ShowDamageText(simpleAttackAnimation,damage, percentageHealth, healthImage, Color.red, ignorePass);
                }
                break;
            case SkillTypes.Heal:
                HealAnimation.transform.position = playerDamagePosition;
                HealAnimation.StartAnimationOneTime(null);
                ShowDamageText(HealAnimation, damage, percentageHealth, healthImage, Color.green, ignorePass);
                break;
            case SkillTypes.Defense:
                DefenseAnimation.transform.position = playerDamagePosition;
                DefenseAnimation.StartAnimationOneTime(() => turnFightController.PassMove());
                break;
        }

    }

    private void ShowDamageText(SpriteSwitch mainSwitch, float damage, float percentageHealth, Image healthImage, Color textColor, bool ignorePass = false)
    {

        TMP_Text newTextDamage = Instantiate(damageText, mainSwitch.transform);
        newTextDamage.color = textColor;

        newTextDamage.text = Mathf.RoundToInt(damage).ToString();

        Transform textTransform = newTextDamage.transform;

        textTransform.localPosition = Vector3.zero;
        textTransform.localRotation = Quaternion.identity;
        textTransform.localScale = Vector3.one;

        Vector2 endPosition = Vector2.zero;

        if (turnFightController.PlayerTurn && (mainSwitch == simpleAttackAnimation || mainSwitch == strongAttackAnimation))
        {
            endPosition = enemyDamagePosition;
        }
        else
        {
            endPosition = playerDamagePosition;
        }

        textTransform.DOJump(endPosition, jumpPower, numberOfJumps, duration).OnComplete(() => Destroy(textTransform.gameObject)).SetUpdate(true);

        ChangeHealthSlider(percentageHealth, healthImage, ignorePass);
    }

    private void ChangeHealthSlider(float percentageHealth, Image healthImage, bool ignorePass = false)
    {
        if(ignorePass)
        {
            healthImage.DOFillAmount(percentageHealth, reducingAnimationDuration).SetEase(Ease.OutSine).SetUpdate(true);
        }
        else
        {
            healthImage.DOFillAmount(percentageHealth, reducingAnimationDuration).SetEase(Ease.OutSine).OnComplete(() => turnFightController.PassMove()).SetUpdate(true);
        }
        
    }
}
