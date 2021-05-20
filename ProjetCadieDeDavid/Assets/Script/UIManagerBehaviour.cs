using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int currentScoreUI;

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
        scoreText.text = "Score : " + scoreUI;
        stars[0].SetActive(false);
        stars[1].SetActive(false);
        stars[2].SetActive(false);
        nextLevel.SetActive(false);
        for (int i = 0; i < LevelManagerBehaviour.Instance.articleCurrentList.Count; i++)
        {
            Vector3 pos = LevelManagerBehaviour.Instance.miniatureArtcileArray[i].transform.position;
            if (i < LevelManagerBehaviour.Instance.articleAskedArray.Length)
            {
                if (LevelManagerBehaviour.Instance.articleCurrentNumberList[i] != 0)
                {
                    StartCoroutine(AddUIScore(LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 500, true));
                    currentScoreUI = LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 500;
                    Instantiate(particlesScore, pos, Quaternion.identity, LevelManagerBehaviour.Instance.transform.Find("Canvas"));
                }
            }
            else
            {
                StartCoroutine(AddUIScore(LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 50 * -1, false));
                currentScoreUI = LevelManagerBehaviour.Instance.articleCurrentNumberList[i] * 50 * -1;
                Instantiate(particlesScore, pos, Quaternion.identity, LevelManagerBehaviour.Instance.transform.Find("Canvas"));
            }
            yield return new WaitForSeconds(1f);
        }
        if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= GameManagerBehaviour.instance.firstStar[LevelManagerBehaviour.Instance.level - 1])
        {
            GameManagerBehaviour.instance.ticketPerLevel = 0;
            GameManagerBehaviour.instance.coinPerLevel = 0;
            EndLevel();
            stars[0].SetActive(true);
            nextLevel.SetActive(true);
            if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= GameManagerBehaviour.instance.secondStar[LevelManagerBehaviour.Instance.level - 1])
            {
                yield return new WaitForSeconds(1.0f);
                stars[1].SetActive(true);
                if (LevelManagerBehaviour.Instance.levelDone && LevelManagerBehaviour.Instance.score >= GameManagerBehaviour.instance.thirdStar[LevelManagerBehaviour.Instance.level - 1])
                {
                    yield return new WaitForSeconds(1.0f);
                    stars[2].SetActive(true);
                }
            }
        }
        else
        {
            stars[0].SetActive(false);
            stars[1].SetActive(false);
            stars[2].SetActive(false);
            nextLevel.SetActive(false);
        }
        GameManagerBehaviour.instance.ResetCoinAndTicket();
    }
    IEnumerator AddUIScore(int score, bool plus)
    {
        if(score != 0)
        {
            if (plus)
            {
                for (int i = 0; i < score * 0.1f - 1; i++)
                {
                    scoreUI+= 10;
                    scoreText.text = "Score : " + scoreUI;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else
            {
                for (int i = 0; i > score; i--)
                {
                    scoreUI--;
                    scoreText.text = "Score : " + scoreUI;
                    yield return new WaitForSeconds(0.01f);
                }
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
    void EndLevel()
     {
        if(LevelManagerBehaviour.Instance.score > GameManagerBehaviour.instance.HighScoreList[SceneManager.GetActiveScene().buildIndex]-1)
        {
              GameManagerBehaviour.instance.HighScoreList[SceneManager.GetActiveScene().buildIndex - 1] = LevelManagerBehaviour.Instance.score;
        }
        if ((SceneManager.GetActiveScene().buildIndex) == GameManagerBehaviour.instance.level)
        {
            GameManagerBehaviour.instance.level = SceneManager.GetActiveScene().buildIndex + 1;
        }
    }
}
