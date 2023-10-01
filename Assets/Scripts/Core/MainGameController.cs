using UnityEngine;
using NaughtyAttributes;
using System;
using System.Reflection;
using SteelLotus.Sounds;
using SteelLotus.Core.Settings;
using SteelLotus.Core.SaveLoadSystem;
using UnityEngine.Audio;
using System.Collections.Generic;
using SteelLotus.Dino.Evolution;

namespace SteelLotus.Core
{
    public class MainGameController : Singleton<MainGameController>
    {

        [SerializeField]
        private AudioMixer mainMixer;

        [BoxGroup("Core"),SerializeField]
        private DataManager dataManager;

        [BoxGroup("Core"), SerializeField]
        private SoundManager soundManager;

        [BoxGroup("Core"), SerializeField]
        private ScenesController scenesController;

        [BoxGroup("Core"), SerializeField]
        private AddictionalMethods addictionalMethods;

        [SerializeField]
        private FightMainController fightMainController;

        private SettingsController settingsController;

        private PlayerDinosour playerDinosour;


        private bool doNotReset = false;
        private List<int> path = new List<int>();
        private EvolutionType currentEvolutionType;

        public bool DoNotReset { get { return doNotReset; } set { doNotReset = value; } }
        public List<int> Path { get { return path; } set { path = value; } }
        public EvolutionType CurrentEvolutionType { get => currentEvolutionType; set => currentEvolutionType = value; }

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);

            dataManager.Init();

            SaveSystem.Init();

            settingsController = new SettingsController();
            settingsController.Init(mainMixer);
            fightMainController.Init(scenesController);
        }


        public T GetFieldByType<T>() where T : class
        {
            Type myType = this.GetType();
            FieldInfo[] allFields = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in allFields)
            {
                if(field.FieldType == typeof(T))
                {
                    return field.GetValue(this) as T;
                }
            }

            return null;
        }

        public T GetFIeldByName<T>(string name) where T : class
        {
            Type myType = this.GetType();
            FieldInfo[] allFields = myType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in allFields)
            {
                if (field.Name == name)
                {
                    return field.GetValue(this) as T;
                }
            }

            return null;
        }

        public void SaveData()
        {
            currentEvolutionType = playerDinosour.CurrentEvolutionType;
        }

        public void Init(PlayerDinosour player)
        {
            playerDinosour = player;
        }

        public void ResetData()
        {
            doNotReset = false;
            path.Clear();
            currentEvolutionType = EvolutionType.Base;
        }

    }
}
