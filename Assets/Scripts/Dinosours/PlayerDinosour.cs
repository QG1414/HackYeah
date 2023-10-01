using NaughtyAttributes;
using SteelLotus.Core;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Playables.FrameData;

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

    public EvolutionStep CurrentPlayerEvolutionStep { get => currentPlayerEvolutionStep; }



    private void Start()
    {
        MainGameController.Instance.Init(this);

        if(MainGameController.Instance.DoNotReset)
        {
            currentUpgradeStep = MainGameController.Instance.Path.Count - 1;
            MainGameController.Instance.DoNotReset = false;
            currentEvolutionType = MainGameController.Instance.CurrentEvolutionType;
        }
        else
        {
            currentPlayerEvolutionStep.Copy(baseForm);
            currentPlayerEvolutionStep.ClearSkills();
            currentPlayerEvolutionStep.FuseSkills(baseForm.DinosourSkills);
        }
    }

    public void ChangeToNewEvolution(EvolutionType evolutionType)
    {
        if(currentEvolutionType != EvolutionType.Base)
        {
            currentUpgradeStep++;
        }

        currentEvolutionType = evolutionType;

        Debug.Log(currentUpgradeStep);
        Debug.Log(currentEvolutionType);

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

    public EvolutionStep GetNextStep(EvolutionType evolutionType)
    {
        int tempCurrentUpgrade = currentUpgradeStep;
        EvolutionType tempCurrentEvolutionType;

        if (currentEvolutionType != EvolutionType.Base)
        {
            tempCurrentUpgrade++;
        }

        tempCurrentEvolutionType = evolutionType;

        EvolutionStep tempStep = new EvolutionStep();

        Debug.Log(currentUpgradeStep);
        Debug.Log(currentEvolutionType);

        EvolutionStep newStep = null;
        switch (evolutionType)
        {
            case EvolutionType.Carnivore:
                newStep = carnivoreSteps[tempCurrentUpgrade];
                break;
            case EvolutionType.Omnivore:
                newStep = omnivoreSteps[tempCurrentUpgrade];
                break;
            case EvolutionType.Herbivore:
                newStep = herbivoreSteps[tempCurrentUpgrade];
                break;
        }

        tempStep.Copy(newStep);
        tempStep.FuseSkills(newStep.DinosourSkills);

        return tempStep;

    }
}
