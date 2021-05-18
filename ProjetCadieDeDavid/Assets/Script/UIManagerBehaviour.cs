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
    public GameObject canvas;

    public Text levelText;
    public Text coinText;
    public TMP_Text[] listHUDText;
    public TMP_Text[] listNumberHUDText;
    public GameObject[] miniatureArtcileArray;
    public Color32 red;
    public Color32 orange;
    public Color32 green;

    public GameObject[] stars;
    public GameObject particlesScore;
    public GameObject nextLevel;
    public TMP_Text scoreText;
    int scoreUI;

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
        scoreUI = 0;
        LevelManagerBehaviour.Instance.DisplayList();
        LevelManagerBehaviour.Instance.menuBriefing.SetActive(true);
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
        scoreText.text = "Score : 0";
        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
        nextLevel.SetActive(false);
        for (int i = 0; i < LevelManagerBehaviour.Instance.articleCurrentList.Count; i++)
        {
            Vector3 pos = LevelManagerBehaviour.Instance.miniatureArtcileArray[i].transform.position;
            Instantiate(particlesScore, pos, Quaternion.identity, LevelManagerBehaviour.Instance.transform.Find("Canvas"));
            if(i<LevelManagerBehaviour.Instance.articleAskedArray.Length)
            {
                StartCoroutine(AddUIScore(LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 50, true));
            }
            else
            {
                StartCoroutine(AddUIScore(LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 50, false));
            }
            yield return new WaitForSeconds(LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 50 * 0.01f);
        }
        if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= LevelManagerBehaviour.Instance.thirdStar)
        {
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            stars[1].SetActive(true);
            yield return new WaitForSeconds(1.0f);
            stars[2].SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }
        else if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= LevelManagerBehaviour.Instance.secondStar)
        {
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            stars[1].SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }
        else if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= LevelManagerBehaviour.Instance.firstStar)
        {
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
        }
        else
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
            nextLevel.SetActive(false);
            GameManagerBehaviour.instance.AddCoin(GameManagerBehaviour.instance.coinPerLevel * -1);
        }
        GameManagerBehaviour.instance.coinPerLevel = 0;
    }
    IEnumerator AddUIScore(int score, bool plus)
    {
        if(plus)
        {
            for(int i = 0; i <= score; i++)
            {
                scoreText.text = "Score : " + scoreUI++;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            for (int i = 0; i <= score; i++)
            {
                scoreText.text = "Score : " + scoreUI--;
                yield return new WaitForSeconds(0.01f);
            }
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
                listNumberHUDText[i].color = red;
            }
            else
            {
                if (LevelManagerBehaviour.Instance.articleCurrentNumberList[i] == LevelManagerBehaviour.Instance.articleNumberArray[i])
                {
                    listHUDText[i].fontStyle = FontStyles.Strikethrough;
                    listNumberHUDText[i].color = green;
                }
            }
            listNumberHUDText[i].SetText(LevelManagerBehaviour.Instance.articleCurrentNumberList[i].ToString());
            if (i < LevelManagerBehaviour.Instance.articleNumberArray.Length)
            {
                listNumberHUDText[i].text += "/" + LevelManagerBehaviour.Instance.articleNumberArray[i].ToString();
            }
        }
    }

}
