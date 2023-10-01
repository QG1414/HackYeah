using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUsage : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text skillName;

    [SerializeField]
    private Image skilImage;

    [SerializeField]
    private TMP_Text skillCooldown;

    [SerializeField]
    private CanvasGroup blocker;

    private DinosourSkill DinosourSkill;

    private EvolutionStep playerEvolutionStep;
    private PlayerFightController playerFightController;

    private int skillCooldoown = 0;

    public void Init(DinosourSkill skillToAdd, EvolutionStep playerEvolutionStep, PlayerFightController playerFightController)
    {
        this.playerFightController = playerFightController;
        this.playerFightController.reduceCooldown += ReduceCooldown;

        DinosourSkill = skillToAdd;
        this.playerEvolutionStep = playerEvolutionStep;

        skillName.text = DinosourSkill.SkillName;
        skilImage.sprite = skillToAdd.SkillSprite;
    }

    public void UseSkill()
    {
        if(skillCooldoown > 0)
        {
            return;
        }

        //TODO obra¿enia i animacje
        float damageDealt = playerEvolutionStep.BaseAttack;

        switch(DinosourSkill.SkillType)
        {
            case SkillTypes.Attack:
                playerFightController.Attack(damageDealt);
                break;
            case SkillTypes.StrongAttack:
                playerFightController.Attack(damageDealt + DinosourSkill.SkillDamage, true);
                skillCooldoown = DinosourSkill.SkillCooldown;
                blocker.alpha = 1;
                skillCooldown.text = "Turns: " + skillCooldoown.ToString();
                break;
            case SkillTypes.Defense:
                playerFightController.Defense(DinosourSkill.SkillDefance);
                skillCooldoown = DinosourSkill.SkillCooldown;
                blocker.alpha = 1;
                skillCooldown.text = "Turns: " + skillCooldoown.ToString();
                break;
            case SkillTypes.Heal:
                playerFightController.Heal(DinosourSkill.SkillHealing);
                skillCooldoown = DinosourSkill.SkillCooldown;
                blocker.alpha = 1;
                skillCooldown.text = "Turns: " + skillCooldoown.ToString();
                break;
        }
        
    }

    public void ReduceCooldown()
    {
        skillCooldoown--;

        skillCooldown.text = "Turns: " + skillCooldoown.ToString();

        if (skillCooldoown == 0)
        {
            blocker.alpha = 0;
        }
    }

    private void OnDestroy()
    {
        this.playerFightController.reduceCooldown -= ReduceCooldown;
    }
}
