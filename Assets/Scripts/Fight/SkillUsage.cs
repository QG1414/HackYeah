using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUsage : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text skillName;

    [SerializeField]
    private Image skilImage;

    private DinosourSkill DinosourSkill;

    private EvolutionStep playerEvolutionStep;
    private PlayerFightController playerFightController;

    public void Init(DinosourSkill skillToAdd, EvolutionStep playerEvolutionStep, PlayerFightController playerFightController)
    {
        this.playerFightController = playerFightController;
        DinosourSkill = skillToAdd;
        this.playerEvolutionStep = playerEvolutionStep;

        skillName.text = DinosourSkill.SkillName;
        skilImage.sprite = skillToAdd.SkillSprite;
    }

    public void UseSkill()
    {
        //TODO obra¿enia i animacje
        float damageDealt = playerEvolutionStep.BaseAttack;

        switch(DinosourSkill.SkillType)
        {
            case SkillTypes.Attack:
                playerFightController.Attack(damageDealt);
                break;
            case SkillTypes.StrongAttack:
                playerFightController.Attack(damageDealt + DinosourSkill.SkillDamage, true);
                break;
            case SkillTypes.Defense:
                playerFightController.Defense(DinosourSkill.SkillDefance);
                break;
            case SkillTypes.Heal:
                playerFightController.Heal(DinosourSkill.SkillHealing);
                break;
        }
    }
}
