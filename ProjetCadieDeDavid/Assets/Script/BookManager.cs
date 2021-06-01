using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class BookManager : MonoBehaviour
{
    int levelProgress;

    Vector2 positionListe = new Vector2(580, 400);
    public GameObject[] arrayRecette;

    GameObject listRecette;
    public Button startLevel;
    public TMP_Text levelText;
    public TMP_Text score;
    public GameObject etoileUn, etoileDeux, etoileTrois;
    public TMP_Text ticketText;

    
    public TMP_Text recette;
    public GameObject ImageRecette;
    Image inUseRecette;
    public Sprite[] newImageRecette;

    Image inUseEtoileUn, inUseEtoileDeux, inUseEtoileTrois;
    public Sprite etoileFull, etoileEmpty;
    public List<int> scoreList;

    public Image livreAnim;
    Animator anim;

    public GameObject selectionImage;
    public Sprite[] imageSelection;

    private static BookManager _instance;
    public static BookManager instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        inUseRecette = ImageRecette.GetComponent<Image>();
        ImageRecette.GetComponent<Image>().preserveAspect = true;
        inUseEtoileUn = etoileUn.GetComponent<Image>();
        inUseEtoileDeux = etoileDeux.GetComponent<Image>();
        inUseEtoileTrois = etoileTrois.GetComponent<Image>();
        anim = livreAnim.GetComponent<Animator>();
        levelProgress = GameManagerBehaviour.instance.level;
        DisplayTicket();
        this.ChangeSelect(1);
        ChangeSpriteSelection();
    }
    private void Start()
    {
        _instance = this;
    }
    public void LevelLoad()
    {
        SoundManagerBehaviour.instance.PlayButtonSound();
        if (GameManagerBehaviour.instance.levelSelect <= levelProgress)
        {
            SceneManager.LoadScene("Level " + GameManagerBehaviour.instance.levelSelect);
            GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.InGame);
        }
    }
    public void LoadMenu()
    {
        SoundManagerBehaviour.instance.PlayButtonSound();
        GameManagerBehaviour.instance.ChangeGameStateByUI(0);
    }
    public void NextLevel()
    {
        if (GameManagerBehaviour.instance.levelSelect < GameManagerBehaviour.instance.maxLevel)
        {
            GameManagerBehaviour.instance.levelSelect++;
            ChangeSelect(GameManagerBehaviour.instance.levelSelect);
        }
    }
    public void PreviousLevel()
    {
        if (GameManagerBehaviour.instance.levelSelect > 1)
        {
            GameManagerBehaviour.instance.levelSelect--;
            ChangeSelect(GameManagerBehaviour.instance.levelSelect);
        }
    }

    public void PlayButtonSound()
    {
        SoundManagerBehaviour.instance.PlayButtonSound();
    }
    public void PlayButtonBackSound()
    {
        SoundManagerBehaviour.instance.PlayButtonBackSound();
    }
    void DisplayTicket()
    {
        ticketText.text = "Ticket : " + GameManagerBehaviour.instance.ticket;
    }
    public void ChangePlayerSkin(int skin)
    {
        GameManagerBehaviour.instance.playerSkin = skin;
        ChangeSpriteSelection();
    }
    void ChangeSpriteSelection()
    {
        selectionImage.GetComponent<Image>().sprite = imageSelection[GameManagerBehaviour.instance.playerSkin];
    }
    public void ChangeSelect(int level)
    {
        if(!SoundManagerBehaviour.instance.backButtonSound.isPlaying)
        {
            SoundManagerBehaviour.instance.PlayButtonBackSound();
        }
        //listRecette = arrayRecette[level -1];
        if(listRecette != null)
        {
            Destroy(listRecette);
        }
        listRecette = Instantiate(arrayRecette[level - 1], positionListe, Quaternion.identity, GameObject.FindGameObjectWithTag("pageGauche").transform);
        recette.text = System.IO.File.ReadAllText("Assets/Recettes/Recipe" + level + ".txt");
        inUseRecette.sprite = newImageRecette[level - 1];
        GameManagerBehaviour.instance.levelSelect = level;
        levelText.text = "L"+"\n"+"E"+"\n"+"V"+"\n"+"E"+"\n"+"L"+"\n"+"\n"+ level;
        inUseEtoileUn.sprite = etoileEmpty;
        inUseEtoileDeux.sprite = etoileEmpty;
        inUseEtoileTrois.sprite = etoileEmpty;
        if (GameManagerBehaviour.instance.HighScoreList[level - 1] >= GameManagerBehaviour.instance.firstStar[level - 1])
        {
            inUseEtoileUn.sprite = etoileFull;
        }
        if (GameManagerBehaviour.instance.HighScoreList[level - 1] >= GameManagerBehaviour.instance.secondStar[level - 1])
        {
            inUseEtoileDeux.sprite = etoileFull;
        }
        if (GameManagerBehaviour.instance.HighScoreList[level - 1] >= GameManagerBehaviour.instance.thirdStar[level - 1])
        {
            inUseEtoileTrois.sprite = etoileFull;
        }
        score.text = "Score : " + GameManagerBehaviour.instance.HighScoreList[level - 1];
        if (GameManagerBehaviour.instance.levelSelect > levelProgress)
        {
            startLevel.interactable = false;
        }
        else
        {
            startLevel.interactable = true;
        }
        anim.SetTrigger("turnPage");
    }
}
