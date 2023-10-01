using NaughtyAttributes;
using SteelLotus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountExit : MonoBehaviour
{
    [SerializeField]
    private float waitingTime;

    [SerializeField, Scene]
    private string mainMenuScene;

    ScenesController scenesController;

    // Start is called before the first frame update
    void Start()
    {
        scenesController = MainGameController.Instance.GetFieldByType<ScenesController>();

        StartCoroutine(WaitForExit());
    }

    private IEnumerator WaitForExit()
    {
        yield return new WaitForSeconds(waitingTime);
        scenesController.StartTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, () => { SceneManager.LoadScene(mainMenuScene); scenesController.EndTransition(SteelLotus.Animation.AnimationTypes.AnchoreMovement, null); });
    }
}
