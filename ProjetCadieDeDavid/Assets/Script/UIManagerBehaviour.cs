using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class UIManagerBehaviour : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject selection;
    public GameObject HUD;
    public GameObject pause;
    public GameObject gameOver;

    public Text levelText;
    public Text coinText;
    public TMP_Text[] listHUDText;
    public TMP_Text[] listNumberHUDText;
    public GameObject[] miniatureArtcileArray;
    public Color32 red;
    public Color32 orange;
    public Color32 green;

    public GameObject[] stars;
    public GameObject[] particles;
    public GameObject nextLevel;
    public TMP_Text scoreText;

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
        red.a = 255;
        orange.a = 255;
        green.a = 255;
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
        LevelManagerBehaviour.Instance.DisplayList();
        LevelManagerBehaviour.Instance.menuBriefing.SetActive(true);
    }
    public void SetGameOverActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(true);
        StartCoroutine(ShowStars());
    }
    public void SetRecipeActive()
    {
        mainMenu.SetActive(false);
        selection.SetActive(false);
        HUD.SetActive(false);
        pause.SetActive(false);
        gameOver.SetActive(false);
    }
    IEnumerator ShowStars()
    {
        GameOverAnimation();
        scoreText.text = "Score : " + LevelManagerBehaviour.Instance.score;
        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
        nextLevel.SetActive(false);
        if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= LevelManagerBehaviour.Instance.thirdStar)
        {
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            stars[1].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            stars[2].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            GameManagerBehaviour.instance.coinPerLevel = 0;
        }
        else if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= LevelManagerBehaviour.Instance.secondStar)
        {
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            stars[1].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            GameManagerBehaviour.instance.coinPerLevel = 0;
        }
        else if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= LevelManagerBehaviour.Instance.firstStar)
        {
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
            GameManagerBehaviour.instance.coinPerLevel = 0;
        }
        else
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
            nextLevel.SetActive(false);
            GameManagerBehaviour.instance.AddCoin(GameManagerBehaviour.instance.coinPerLevel * -1);
            GameManagerBehaviour.instance.coinPerLevel = 0;
        }
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
        for (int i = 0; i < LevelManagerBehaviour.Instance.articleCurrentList.Count; i++)
        {
            miniatureArtcileArray[i].SetActive(true);
            miniatureArtcileArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(LevelManagerBehaviour.Instance.articleCurrentList[i]);
            listHUDText[i].text = LevelManagerBehaviour.Instance.articleCurrentList[i];
            if (i >= LevelManagerBehaviour.Instance.articleAskedArray.Length)
            {
                listHUDText[i].color = red;
            }
            else
            {
                if (LevelManagerBehaviour.Instance.articleCurrentNumberList[i] == LevelManagerBehaviour.Instance.articleNumberArray[i])
                {
                    listHUDText[i].color = green;
                }
                else if (LevelManagerBehaviour.Instance.articleCurrentNumberList[i] > LevelManagerBehaviour.Instance.articleNumberArray[i])
                {
                    listHUDText[i].color = orange;
                }
            }
            listNumberHUDText[i].SetText(LevelManagerBehaviour.Instance.articleCurrentNumberList[i].ToString());
            if (i < LevelManagerBehaviour.Instance.articleNumberArray.Length)
            {
                listNumberHUDText[i].text += "/" + LevelManagerBehaviour.Instance.articleNumberArray[i].ToString();
            }
        }
    }

    void GameOverAnimation()
    {
        Instantiate(particles[1], scoreText.transform.position, Quaternion.identity);
    }
}
