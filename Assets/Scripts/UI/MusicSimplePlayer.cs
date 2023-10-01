using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteelLotus.Sounds;
using SteelLotus.Core;

public class MusicSimplePlayer : MonoBehaviour
{
    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = MainGameController.Instance.GetFieldByType<SoundManager>(); 
    }

    public void PlaySound(AudioClip clip)
    {
        soundManager.PlayOneShoot(soundManager.UISource, clip);
    }
    public void Mute()
    {
        soundManager.StopAudio(soundManager.UISource);
    }
}
