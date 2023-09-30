using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace SteelLotus.Dino.Evolution
{
    [CreateAssetMenu(fileName = "EvolutionSteps", menuName = "SteelLotus/EvolutionSteps")]
    public class EvolutionStep : ScriptableObject
    {
        [SerializeField]
        private Sprite dinosourSprite;

        [SerializeField]
        private float hp;

        [SerializeField]
        private float baseAttack;

        [SerializeField]
        private EvolutionType evolutionType;

        [SerializeField]
        private List<DinosourSkill> dinosourSkills = new List<DinosourSkill>();

        public Sprite DinosourSprite { get => dinosourSprite; set => dinosourSprite = value; }
        public float HP { get => hp; set => hp = value; }
        public float BaseAttack { get => baseAttack; set => baseAttack = value; }
        public EvolutionType EvolutionType { get => evolutionType; set => evolutionType = value; }
        public List<DinosourSkill> DinosourSkills { get => dinosourSkills; set => dinosourSkills = value; }

        

        public void FuseSkills(List<DinosourSkill> newSkills)
        {
            foreach (DinosourSkill skill in newSkills)
            {
                if (dinosourSkills.Find((x) => x.SkillName == skill.SkillName) != null)
                {
                    int skillIndex = dinosourSkills.FindIndex((x) => x.SkillName == skill.SkillName);
                    dinosourSkills[skillIndex].IncreaseLevel();  
                }
                else
                {
                    dinosourSkills.Add(skill);
                }
            }
        }

        public void Copy(EvolutionStep evolutionStepToCopy)
        {
            this.dinosourSprite = evolutionStepToCopy.DinosourSprite;
            this.hp = evolutionStepToCopy.hp;
            this.baseAttack = evolutionStepToCopy.baseAttack;
            this.evolutionType = evolutionStepToCopy.evolutionType;
        }

        public void ClearSkills()
        {
            this.dinosourSkills.Clear();
        }
    }

    public enum EvolutionType
    {
        Carnivore,
        Herbivore,
        Omnivore
    }
}