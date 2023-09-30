using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SteelLotus.Sounds;

namespace SteelLotus.Core.Settings
{
    public class SettingsManagement : MonoBehaviour
    {

        [SerializeField, BoxGroup("Setting Buttons")]
        private Slider masterVolumeSlider;

        [SerializeField, BoxGroup("Setting Buttons")]
        private Slider musicVolumeSlider;

        [SerializeField, BoxGroup("Setting Buttons")]
        private Slider effectsVolumeSlider;

        [SerializeField, BoxGroup("Setting Buttons")]
        private TMP_Dropdown resolutionDropdown;

        [SerializeField, BoxGroup("Setting Buttons")]
        private Toggle fullscreenToggle;

        private SettingsController settingsController;
        private SoundManager soundManager;

        void Start()
        {
            settingsController = MainGameController.Instance.GetFieldByType<SettingsController>();
            soundManager = MainGameController.Instance.GetFieldByType<SoundManager>();
            SetupSettings();
        }


        #region SettingsPublicMethods

        public void SaveSettings()
        {
            settingsController.SaveSettings();
            //soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[0], 1f);
        }

        public void VolumeChanged(Slider sliderChanged)
        {
            SoundTypes soundType = ChooseSoundType(sliderChanged);

            settingsController.ChangeVolumeOfSounds(soundType, sliderChanged.value);
        }

        public void SetNewResolution()
        {
            //soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[1], 0f);
            string newResolution = resolutionDropdown.options[resolutionDropdown.value].text;

            settingsController.ChangeGameResolution(newResolution);
        }

        public void SetFullscreen()
        {
            //soundManager.PlayOneShoot(soundManager.UISource, soundManager.UICollection.clips[1], 0f);
            settingsController.SetFullScreen(fullscreenToggle.isOn);
        }

        #endregion SettingsPublicMethods


        #region SettingsAdditionalMethods

        private void SetupSettings()
        {
            SettingsController settingsControllertemp = settingsController;

            settingsControllertemp.SetupResolutions(resolutionDropdown);

            masterVolumeSlider.value = settingsControllertemp.MasterVolumeValue;
            VolumeChanged(masterVolumeSlider);

            musicVolumeSlider.value = settingsControllertemp.MusicVolumeValue;
            VolumeChanged(musicVolumeSlider);

            effectsVolumeSlider.value = settingsControllertemp.EffectsVolumeValue;
            VolumeChanged(effectsVolumeSlider);

            fullscreenToggle.isOn = settingsControllertemp.FullscreenActive;

            for (int i = 0; i < resolutionDropdown.options.Count; i++)
            {
                if (resolutionDropdown.options[i].text == settingsControllertemp.ScreenResolution)
                {
                    resolutionDropdown.value = i;
                    break;
                }
            }
        }

        private SoundTypes ChooseSoundType(Slider slider)
        {
            if (slider == masterVolumeSlider)
                return SoundTypes.Master;
            else if (slider == musicVolumeSlider)
                return SoundTypes.Music;
            else
                return SoundTypes.Effects;
        }

        #endregion SettingsAdditionalMethods

    }
}
