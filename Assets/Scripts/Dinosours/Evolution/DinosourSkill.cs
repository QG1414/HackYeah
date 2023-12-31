using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SteelLotus.Dino.Evolution
{
    [System.Serializable]
    public class DinosourSkill
    {
        [SerializeField]
        private string skillName;

        [SerializeField]
        private Sprite skillSprite;

        [SerializeField]
        private int skillLevel;

        [SerializeField]
        private float skillDamage;

        [SerializeField]
        private float skillHealing;

        [SerializeField]
        private float skillDefance;

        [SerializeField]
        private SkillTypes skillType;

        [SerializeField]
        private int skillCooldown;

        private float levelMultiplayer = 20;

        public string SkillName { get => skillName; set => skillName = value; }
        public Sprite SkillSprite { get => skillSprite; set => skillSprite = value; }
        public int SkillLevel { get => skillLevel; set => skillLevel = value; }
        public float SkillDamage { get => skillDamage; set => skillDamage = value; }
        public float SkillHealing { get => skillHealing; set => skillHealing = value; }
        public float SkillDefance { get => skillDefance; set => skillDefance = value; }
        public SkillTypes SkillType { get => skillType; set => skillType = value; }
        public int SkillCooldown { get => skillCooldown; set => skillCooldown = value; }


        public void IncreaseLevel()
        {

            if (skillLevel < 0)
                return;

            skillLevel += 1;
            SkillDamage = (levelMultiplayer * skillLevel + 100f) * SkillDamage / 100f;
            skillHealing = (levelMultiplayer * skillLevel + 100f) * skillHealing / 100f;
            skillDefance = (levelMultiplayer * skillLevel + 100f) * skillDefance / 100f;
        }
    }

    public enum SkillTypes
    {
        Attack,
        StrongAttack,
        Defense,
        Heal
    }
}
