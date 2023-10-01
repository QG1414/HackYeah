using NaughtyAttributes;
using SteelLotus.Animation;
using SteelLotus.Core;
using SteelLotus.Dino.Evolution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Playables.FrameData;

public class EvolutionPointsController : MonoBehaviour
{
    [SerializeField]
    private int meatCounter;

    [SerializeField]
    private int greensCounter;

    [SerializeField]
    private TMPro.TMP_Text meatText;

    [SerializeField]
    private TMPro.TMP_Text greensText;

    [SerializeField]
    private CanvasGroup interactionsBlocker;

    [SerializeField]
    private AnimatedUI upgradesPanelAnimation;

    [SerializeField]
    private Vector2 startPosition;

    [SerializeField]
    private Vector2 endposition;

    [SerializeField]
    private PlayerDinosour playerDinosour;

    [SerializeField]
    private List<HighlightEffect> herbivoreUpgrades = new List<HighlightEffect>();

    [SerializeField]
    private List<HighlightEffect> omnivoreUpgrades = new List<HighlightEffect>();

    [SerializeField]
    private List<HighlightEffect> carvivoreUpgrades = new List<HighlightEffect>();

    HighlightEffect workingAnimation;

    FightMainController mainController;


    private void Start()
    {
        mainController = MainGameController.Instance.GetFieldByType<FightMainController>();

        if (MainGameController.Instance.Path.Count != 0)
        {
            RestoreUpgrades();
        }

        if (playerDinosour.CurrentEvolutionType == EvolutionType.Base || MainGameController.Instance.Path.Count != 0)
        {
            AddPoints();
        }
    }

    public void RestoreUpgrades()
    {
        Debug.Log($"Path length: {MainGameController.Instance.Path.Count}");

        for(int i=0;i < MainGameController.Instance.Path.Count; i++)
        {
            if (MainGameController.Instance.Path[i] == 0)
            {
                carvivoreUpgrades[i].UnlockUnlockedColor();
            }
            else if (MainGameController.Instance.Path[i] == 1)
            {
                omnivoreUpgrades[i].UnlockUnlockedColor();
            }
            else
            {
                herbivoreUpgrades[i].UnlockUnlockedColor();
            }
        }
    }

    public void ShowUpgradePanel()
    {
        interactionsBlocker.blocksRaycasts = true;
        upgradesPanelAnimation.StartRectMovementAnimation(startPosition, endposition, 0);
    }

    public void HideUpgradePanel()
    {
        upgradesPanelAnimation.SetActionToStartAfterAnimationEnd(() => interactionsBlocker.blocksRaycasts = false);
        upgradesPanelAnimation.StartRectMovementAnimation(endposition, startPosition, 1);
    }

    [Button]
    public void AddPoints()
    {
        meatCounter++;
        greensCounter++;
        UpdateText();
        ShowUpgradePanel();
    }

    public void BuyUpgrade(int upgradeIndex)
    {
        int currentLevel = playerDinosour.CurrentUpgradeStep;

        if (playerDinosour.CurrentEvolutionType == EvolutionType.Base)
        {
            EvolutionType evolutionType = (EvolutionType)upgradeIndex;
            playerDinosour.ChangeToNewEvolution(evolutionType);
            meatCounter--;
            greensCounter--;
            UpdateText();
            HideUpgradePanel();
            ChooseUpgrade(evolutionType, currentLevel, true);
            return;
        }

        currentLevel++;

        if (upgradeIndex == 0)
        {
            if ((int)playerDinosour.CurrentEvolutionType > 0)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType - 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                playerDinosour.ChangeToNewEvolution(evolutionType);
                ChooseUpgrade(evolutionType, currentLevel, true);
            }
            else
            {
                playerDinosour.ChangeToNewEvolution(playerDinosour.CurrentEvolutionType);
                ChooseUpgrade(playerDinosour.CurrentEvolutionType, currentLevel, true);
            }
        }
        else if(upgradeIndex == 1)
        {
            if((int)playerDinosour.CurrentEvolutionType > 1)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType - 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                playerDinosour.ChangeToNewEvolution(evolutionType);
                ChooseUpgrade(evolutionType, currentLevel, true);
            }
            else if((int)playerDinosour.CurrentEvolutionType == 0)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType + 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;

                Debug.Log(evolutionType);
                playerDinosour.ChangeToNewEvolution(evolutionType);
                ChooseUpgrade(evolutionType, currentLevel, true);
            }
            else
            {
                Debug.Log(playerDinosour.CurrentEvolutionType);
                playerDinosour.ChangeToNewEvolution(playerDinosour.CurrentEvolutionType);
                ChooseUpgrade(playerDinosour.CurrentEvolutionType, currentLevel, true);
            }
        }
        else
        {
            if((int)playerDinosour.CurrentEvolutionType < 2)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType + 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                playerDinosour.ChangeToNewEvolution(evolutionType);
                ChooseUpgrade(evolutionType, currentLevel, true);
            }
            else
            {
                playerDinosour.ChangeToNewEvolution(playerDinosour.CurrentEvolutionType);
                ChooseUpgrade(playerDinosour.CurrentEvolutionType, currentLevel, true);
            }
        }


        meatCounter--;
        greensCounter--;
        UpdateText();
        HideUpgradePanel();
    }

    public bool checkIfEnaughFood(FoodTypes foodType)
    {
        switch (foodType)
        {
            case FoodTypes.Meat:
                return (meatCounter > 0 ? true : false);
            case FoodTypes.Greens:
                return (greensCounter > 0 ? true : false);
            case FoodTypes.Both:
                return (greensCounter > 0 && meatCounter > 0 ? true : false);
        }

        return false;
    }

    public void UpdateText()
    {
        meatText.text = meatCounter.ToString();
        greensText.text = greensCounter.ToString();
    }

    public void HighlightCorrectUpgrade(int upgradeIndex)
    {
        if(workingAnimation != null)
        {
            workingAnimation.StopAnimation();
            workingAnimation = null;
        }


        int currentLevel = playerDinosour.CurrentUpgradeStep;

        if (playerDinosour.CurrentEvolutionType == EvolutionType.Base)
        {
            EvolutionType evolutionType = (EvolutionType)upgradeIndex;
            ChooseUpgrade(evolutionType, currentLevel);
            return;
        }

        currentLevel++;

        if (upgradeIndex == 0)
        {
            if ((int)playerDinosour.CurrentEvolutionType > 0)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType - 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                ChooseUpgrade(evolutionType, currentLevel);
            }
            else
            {
                ChooseUpgrade(playerDinosour.CurrentEvolutionType, currentLevel);
            }
        }
        else if (upgradeIndex == 1)
        {
            if ((int)playerDinosour.CurrentEvolutionType > 1)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType - 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                ChooseUpgrade(evolutionType, currentLevel);
            }
            else if ((int)playerDinosour.CurrentEvolutionType == 0)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType + 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                ChooseUpgrade(evolutionType, currentLevel);
            }
            else
            {
                ChooseUpgrade(playerDinosour.CurrentEvolutionType, currentLevel);
            }
        }
        else
        {
            if ((int)playerDinosour.CurrentEvolutionType < 2)
            {
                int newIndex = (int)playerDinosour.CurrentEvolutionType + 1;
                EvolutionType evolutionType = (EvolutionType)newIndex;
                ChooseUpgrade(evolutionType, currentLevel);
            }
            else
            {
                ChooseUpgrade(playerDinosour.CurrentEvolutionType, currentLevel);
            }
        }
    }

    private void ChooseUpgrade(EvolutionType evolutionType, int level, bool turnOn = false)
    {
        switch (evolutionType)
        {
            case EvolutionType.Carnivore:
                if(turnOn)
                {
                    MainGameController.Instance.Path.Add(0);
                    carvivoreUpgrades[level].UnlockUnlockedColor();
                }
                else
                {
                    carvivoreUpgrades[level].StartAnimation();
                    workingAnimation = carvivoreUpgrades[level];
                }
                break;
            case EvolutionType.Herbivore:
                if (turnOn)
                {
                    MainGameController.Instance.Path.Add(2);
                    herbivoreUpgrades[level].UnlockUnlockedColor();
                }
                else
                {
                    herbivoreUpgrades[level].StartAnimation();
                    workingAnimation = herbivoreUpgrades[level];
                }
                break;
            case EvolutionType.Omnivore:
                if (turnOn)
                {
                    MainGameController.Instance.Path.Add(1);
                    omnivoreUpgrades[level].UnlockUnlockedColor();
                }
                else
                {
                    omnivoreUpgrades[level].StartAnimation();
                    workingAnimation = omnivoreUpgrades[level];
                }
                break;
        }
    }

    public void StopHighlight()
    {
        if(workingAnimation != null)
        {
            workingAnimation.StopAnimation();
            workingAnimation = null;
        }
    }

    public void StartFight()
    {
        mainController.LoadFightScene();
    }

}

public enum FoodTypes
{
    Meat,
    Greens,
    Both
}
