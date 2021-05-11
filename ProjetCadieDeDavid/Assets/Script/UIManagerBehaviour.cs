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
    public Text listNumberHUDText;
    public GameObject[] miniatureArtcileArray;

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
    private void Start()
    {
        for(int i = 0; i < miniatureArtcileArray.Length; i++)
        {
            miniatureArtcileArray[i].GetComponent<Image>().preserveAspect = true;
            miniatureArtcileArray[i].SetActive(false);
        }
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
        levelText.text = "Level : " + LevelManagerBehaviour.Instance.level;
    }
    public void DisplayCoin()
    {
        coinText.text = "Coin : " + GameManagerBehaviour.instance.coin;
    }
    public void DisplayListHUD()
    {
        listHUDText.text = "";
        listNumberHUDText.text = "";
        for (int i = 0; i < LevelManagerBehaviour.Instance.articleCurrentList.Count; i++)
        {
            miniatureArtcileArray[i].SetActive(true);
            miniatureArtcileArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(LevelManagerBehaviour.Instance.articleCurrentList[i]);
            listHUDText.text += LevelManagerBehaviour.Instance.articleCurrentList[i] + "\n";
            listNumberHUDText.text += LevelManagerBehaviour.Instance.articleCurrentNumberList[i];
            if (i < LevelManagerBehaviour.Instance.articleNumberArray.Length)
            {
                listNumberHUDText.text += "/" + LevelManagerBehaviour.Instance.articleNumberArray[i] + "\n";
            }
            else
            {
                listNumberHUDText.text += "\n";
            }
        }
    }
}
