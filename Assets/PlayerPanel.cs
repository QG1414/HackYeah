using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField]
    private Image dinosaurImage;

    [SerializeField]
    private TMP_Text dinosaurName;

    [SerializeField]
    private TMP_Text dinosaurDescription;

    [SerializeField]
    private TMP_Text dinosaurAttack;

    [SerializeField]
    private TMP_Text dinosaurHP;

    [SerializeField]
    private Sprite emptySprite;

    private EvolutionStep currentEvolutionStep;

    private void Start()
    {
        Deinit();
    }

    public void Init(EvolutionStep currentEvolutionStep)
    {
        dinosaurImage.sprite = currentEvolutionStep.DinosourSprite;
        dinosaurName.text = currentEvolutionStep.DinosourName;
        dinosaurDescription.text = currentEvolutionStep.DinosourDescription;
        dinosaurAttack.text = "ATK: " + currentEvolutionStep.BaseAttack.ToString();
        dinosaurHP.text = "HP: " + currentEvolutionStep.HP.ToString();
    }

    public void Deinit()
    {
        if(currentEvolutionStep == null)
        {
            dinosaurImage.sprite = emptySprite;
            dinosaurName.text = "???";
            dinosaurDescription.text = "???";
            dinosaurAttack.text = "???";
            dinosaurHP.text = "???";
        }
        else
        {
            dinosaurImage.sprite = currentEvolutionStep.DinosourSprite;
            dinosaurName.text = currentEvolutionStep.DinosourName;
            dinosaurDescription.text = currentEvolutionStep.DinosourDescription;
            dinosaurAttack.text = "ATK: " + currentEvolutionStep.BaseAttack.ToString();
            dinosaurHP.text = "HP: " + currentEvolutionStep.HP.ToString();
        }
    }

    public void SaveNewState(EvolutionStep currentEvolutionStep)
    {
        this.currentEvolutionStep = currentEvolutionStep;
        dinosaurImage.sprite = currentEvolutionStep.DinosourSprite;
        dinosaurName.text = currentEvolutionStep.DinosourName;
        dinosaurDescription.text = currentEvolutionStep.DinosourDescription;
        dinosaurAttack.text = "ATK: " + currentEvolutionStep.BaseAttack.ToString();
        dinosaurHP.text = "HP: " + currentEvolutionStep.HP.ToString();
    }
}
