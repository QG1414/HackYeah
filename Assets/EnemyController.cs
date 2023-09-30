using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Image enemyImage; 


    private EnemyData enemyData;

    private float enemyHP;

    private TurnFightController turnFightController;

    public void Init(EnemyData enemyData, TurnFightController turnFightController)
    {
        this.turnFightController = turnFightController;
        this.enemyData = enemyData;
        enemyHP = this.enemyData.HP;
        enemyImage.sprite = enemyData.DinosourSprite;
    }

    public void Attack()
    {
        turnFightController.PlayerFight.GetHit(enemyData.BaseAttack + Random.Range(enemyData.RandomElements.x, enemyData.RandomElements.y));
        turnFightController.PassMove();
    }

    public void GetHit(float howMuch)
    {
        enemyHP -= howMuch;

        Debug.Log($"Enemy lost: {howMuch}hp now he has {enemyHP}");

        if(enemyHP < 0)
        {
            enemyHP = 0;
        }
    }

    public bool CheckIfAlive()
    {
        return enemyHP > 0;
    }
}

