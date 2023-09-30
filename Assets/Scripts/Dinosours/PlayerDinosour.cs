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

    private void Start()
    {
        currentPlayerEvolutionStep.Copy(baseForm);
        currentPlayerEvolutionStep.ClearSkills();
        currentPlayerEvolutionStep.FuseSkills(baseForm.DinosourSkills);
    }

    public void ChangeToNewEvolution(EvolutionStep nextStep)
    {
        currentPlayerEvolutionStep.Copy(nextStep);
        currentPlayerEvolutionStep.FuseSkills(nextStep.DinosourSkills);
    }
}
