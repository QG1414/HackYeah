using SteelLotus.Core;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeSkills : MonoBehaviour
{
    [SerializeField]
    private SkillUsage defaultSkill;

    [SerializeField]
    private PlayerFightController fightController;

    private FightMainController fightMainController;

    private List<SkillUsage> skills = new List<SkillUsage>();

    private void Awake()
    {
        fightMainController = MainGameController.Instance.GetFieldByType<FightMainController>();
        fightMainController.InitUI(this);
        fightMainController.StartFight();
    }

    public void CreateSkills(EvolutionStep playerEvolution)
    {
        foreach(DinosourSkill skill in playerEvolution.DinosourSkills)
        {
            SkillUsage skillUsage = Instantiate(defaultSkill, this.transform);
            skillUsage.Init(skill, playerEvolution, fightController);
            skills.Add(skillUsage);
        }
    }
}
