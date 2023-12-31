using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using SteelLotus.Sounds;
using SteelLotus.Core;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Image enemyImage;

    [SerializeField]
    private Image enemyHealthImage;


    private EnemyData enemyData;

    private float enemyHP;

    private TurnFightController turnFightController;

    public Image EnemyHealthImage { get => enemyHealthImage; set => enemyHealthImage = value; }

    private SoundManager soundManager;

    public void Init(EnemyData enemyData, TurnFightController turnFightController)
    {
        soundManager = MainGameController.Instance.GetFieldByType<SoundManager>();
        this.turnFightController = turnFightController;
        this.enemyData = enemyData;
        enemyHP = this.enemyData.HP;
        enemyImage.sprite = enemyData.DinosourSprite;
    }

    public void Attack()
    {
        soundManager.PlayOneShoot(soundManager.PlayerSource, soundManager.PlayerCollection.clips[0]);
        float damage = enemyData.BaseAttack + Random.Range(enemyData.RandomElements.x, enemyData.RandomElements.y);
        turnFightController.AttackAnim.PlayAnimation(SteelLotus.Dino.Evolution.SkillTypes.Attack, false, damage, turnFightController.PlayerFight.CalculatePercentageHealth(damage), turnFightController.PlayerFight.PlayerHealthImage);
        turnFightController.PlayerFight.GetHit(damage);
    }

    public void GetHit(float howMuch)
    {
        enemyHP -= howMuch;

        Debug.Log($"Enemy lost: {howMuch}hp now he has {enemyHP}");
        soundManager.PlayOneShoot(soundManager.PlayerSource, soundManager.PlayerCollection.clips[2]);
        if (enemyHP < 0)
        {
            enemyHP = 0;
        }
    }

    public float CalculatePercentageHealth(float howMuch)
    {
        float tempHealth = enemyHP;

        tempHealth -= howMuch;

        if (tempHealth < 0)
        {
            tempHealth = 0;
        }

        tempHealth = tempHealth / enemyData.HP;

        return tempHealth;
    }

    public bool CheckIfAlive()
    {
        return enemyHP > 0;
    }
}

