using NaughtyAttributes;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDinosour : MonoBehaviour
{
    [SerializeField]
    private EvolutionStep baseForm;

    [SerializeField]
    private EvolutionStep currentPlayerEvolutionStep;

    [SerializeField]
    private List<EvolutionStep> herbivoreSteps = new List<EvolutionStep>();

    [SerializeField]
    private List<EvolutionStep> carnivoreSteps = new List<EvolutionStep>();

    [SerializeField]
    private List<EvolutionStep> omnivoreSteps = new List<EvolutionStep>();

    private EvolutionType currentEvolutionType = EvolutionType.Base;

    private int currentUpgradeStep = 0;

    public EvolutionType CurrentEvolutionType { get => currentEvolutionType; set => currentEvolutionType = value; }

    public int CurrentUpgradeStep { get => currentUpgradeStep; set => currentUpgradeStep = value; }



    private void Start()
    {
        currentPlayerEvolutionStep.Copy(baseForm);
        currentPlayerEvolutionStep.ClearSkills();
        currentPlayerEvolutionStep.FuseSkills(baseForm.DinosourSkills);
    }

    public void ChangeToNewEvolution(EvolutionType evolutionType)
    {
        if(currentEvolutionType != EvolutionType.Base)
        {
            currentUpgradeStep++;
        }

        currentEvolutionType = evolutionType;

        Debug.Log(currentUpgradeStep);

        EvolutionStep newStep = null;
        switch (evolutionType)
        {
            case EvolutionType.Carnivore:
                newStep = carnivoreSteps[currentUpgradeStep];
                break;
            case EvolutionType.Omnivore:
                newStep = omnivoreSteps[currentUpgradeStep];
                break;
            case EvolutionType.Herbivore:
                newStep = herbivoreSteps[currentUpgradeStep];
                break;
        }


        currentPlayerEvolutionStep.Copy(newStep);
        currentPlayerEvolutionStep.FuseSkills(newStep.DinosourSkills);
    }
}
