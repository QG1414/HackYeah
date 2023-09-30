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

    public void Init(DinosourSkill skillToAdd, EvolutionStep playerEvolutionStep)
    {
        DinosourSkill = skillToAdd;
        this.playerEvolutionStep = playerEvolutionStep;

        skillName.text = DinosourSkill.SkillName;
        skilImage.sprite = skillToAdd.SkillSprite;
    }

    public void UseSkill()
    {
        //TODO obra¿enia i animacje
        float damageDealt = playerEvolutionStep.BaseAttack + DinosourSkill.SkillDamage;
        Debug.Log($"wyleczono: {DinosourSkill.SkillHealing}");
        Debug.Log($"zadano: {damageDealt}");
        Debug.Log($"Obroniono: {DinosourSkill.SkillDefance}");
    }
}
