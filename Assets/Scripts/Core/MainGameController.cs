using UnityEngine;
using NaughtyAttributes;
using System;
using System.Reflection;
using SteelLotus.Sounds;
using SteelLotus.Core.Settings;
using SteelLotus.Core.SaveLoadSystem;
using UnityEngine.Audio;

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


    }
}
