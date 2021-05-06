using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerBehaviour : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selection;
    public GameObject HUD;
    public GameObject pause;
    public GameObject gameOver;

    public Text levelText;
    public Text coinText;
    public Text listHUDText;
    private static UIManagerBehaviour _instance;
    public static UIManagerBehaviour instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    public void SetMainMenuActive()
    {
        mainMenu.SetActive(true);
        selection.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
    }
    public void SetLevelSelectionActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(true);
        HUD.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
    }
    public void SetHUDActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(false);
        HUD.SetActive(true);
        pause.SetActive(false);
        gameOver.SetActive(false);
    }
    public void SetPauseActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(true);
        gameOver.SetActive(false);
    }
    public void SetGameOverActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(true);
    }
    public void SetRecipeActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
    }


    public void DisplayLevel()
    {
        levelText.text = "Level" + LevelManagerBehaviour.instance.level;
    }
    public void DisplayCoin()
    {
        coinText.text = "coin" + GameManagerBehaviour.instance.coin;
    }
    public void DisplayListHUD()
    {
        for (int i = 0; 0 < 15; i++)
        {
            listHUDText.text += "temp" + "\t" + "Nombre" + "\n";
        }
    }
}
