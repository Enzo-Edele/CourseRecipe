using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

//Enzo
//Frederic gestion texte des recette & ticket

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
    public TMP_Text ticketLevelText;
    public GameObject cache;
    public GameObject livreTicket;
    public GameObject buttonLivreTicket;
    public GameObject cacheTicketG;
    public GameObject cacheTicketD;
    public Text ticketPageG;
    public Text ticketPageD;
    public TMP_Text ticketTextPageG;
    public TMP_Text ticketTextPageD;
    int page = 0;
    public TMP_Text recette;
    public GameObject ImageRecette;
    Image inUseRecette;
    public Sprite[] newImageRecette;

    Image inUseEtoileUn, inUseEtoileDeux, inUseEtoileTrois;
    public Sprite etoileFull, etoileEmpty;
    public List<int> scoreList;

    public GameObject livreAnim;
    Animator anim;

    public GameObject selectionImage;
    public Sprite[] imageSelection;
    public GameObject cadenaMamieVelo;
    public GameObject cadenaMamieScooter;

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
        _instance = this;
        inUseRecette = ImageRecette.GetComponent<Image>();
        ImageRecette.GetComponent<Image>().preserveAspect = true;
        inUseEtoileUn = etoileUn.GetComponent<Image>();
        inUseEtoileDeux = etoileDeux.GetComponent<Image>();
        inUseEtoileTrois = etoileTrois.GetComponent<Image>();
        anim = livreAnim.GetComponent<Animator>();
        levelProgress = GameManagerBehaviour.instance.level;
        DisplayTicket();
        this.ChangeSelect(levelProgress);
        ChangeSpriteSelection();
        if (GameManagerBehaviour.instance.achatMamieVelo > 0)
        {
            cadenaMamieVelo.SetActive(false);
        }
        if (GameManagerBehaviour.instance.achatMamieScooter > 0)
        {
            cadenaMamieScooter.SetActive(false);
        }
    }
    private void Start()
    {
        UIManagerBehaviour.instance.StartCoroutine(UIManagerBehaviour.instance.Transition());
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
    public void AchatMamieVelo(int amount)
    {
        if (amount < GameManagerBehaviour.instance.coin)
        {
            GameManagerBehaviour.instance.AddCoin(amount * -1);
            GameManagerBehaviour.instance.coinPerLevel = 0;
            GameManagerBehaviour.instance.achatMamieVelo++;
            cadenaMamieVelo.SetActive(false);
        }
    }
    public void AchatMamieScooter(int amount)
    {
        if (amount < GameManagerBehaviour.instance.coin)
        {
            GameManagerBehaviour.instance.AddCoin(amount * -1);
            GameManagerBehaviour.instance.coinPerLevel = 0;
            GameManagerBehaviour.instance.achatMamieScooter++;
            cadenaMamieScooter.SetActive(false);
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
        if (!SoundManagerBehaviour.instance.backButtonSound.isPlaying)
        {
            SoundManagerBehaviour.instance.PlayButtonBackSound();
        }
        StartCoroutine(turnPage(level));
    }
    public void turnPageTicketUI(int pageUI)
    {
        page += pageUI;
        StartCoroutine(turnPageTicket());
    }
    IEnumerator turnPageTicket()
    {
        buttonLivreTicket.SetActive(false);
        livreAnim.SetActive(true);
        anim.SetTrigger("turnPage");
        yield return new WaitForSeconds(1f);
        if (livreTicket.activeSelf && page < 0 || page > 2)
        {
            livreTicket.SetActive(false);
            page = 0;
        }
        else
        {
            livreTicket.SetActive(true);
            cacheTicketG.SetActive(false);
            cacheTicketD.SetActive(false);
            switch(page)
            {
                case 0:
                    ticketTextPageG.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + 15 + ".txt");
                    ticketTextPageD.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + 16 + ".txt");
                    if (GameManagerBehaviour.instance.ticket < 8)
                    {
                        cacheTicketG.SetActive(true);
                        ticketPageG.text = 8.ToString();
                    }
                    if (GameManagerBehaviour.instance.ticket < 16)
                    {
                        cacheTicketD.SetActive(true);
                        ticketPageD.text = 16.ToString();
                    }
                    break;
                case 1:
                    ticketPageG.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + 17 + ".txt");
                    ticketPageD.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + 18 + ".txt");
                    if (GameManagerBehaviour.instance.ticket < 24)
                    {
                        cacheTicketG.SetActive(true);
                        ticketPageG.text = 24.ToString();
                    }
                    if (GameManagerBehaviour.instance.ticket < 32)
                    {
                        cacheTicketD.SetActive(true);
                        ticketPageD.text = 32.ToString();
                    }
                    break;
                case 2:
                    ticketPageG.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + 19 + ".txt");
                    ticketPageD.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + 20 + ".txt");
                    if (GameManagerBehaviour.instance.ticket < 42)
                    {
                        cacheTicketG.SetActive(true);
                        ticketPageG.text = 42.ToString();
                    }
                    if (GameManagerBehaviour.instance.ticket < 50)
                    {
                        cacheTicketD.SetActive(true);
                        ticketPageD.text = 50.ToString();
                    }
                    break;
            }
        }
        yield return new WaitForSeconds(1f);
        buttonLivreTicket.SetActive(true);
        livreAnim.SetActive(false);
    }
    IEnumerator turnPage(int level)
    {
        buttonLivreTicket.SetActive(false);
        livreAnim.SetActive(true);
        anim.SetTrigger("turnPage");
        yield return new WaitForSeconds(1f);
        if (listRecette != null)
        {
            Destroy(listRecette);
        }
        inUseRecette.sprite = newImageRecette[level - 1];
        recette.text = System.IO.File.ReadAllText(Application.streamingAssetsPath + "/Recette/Recipe" + level + ".txt");
        listRecette = Instantiate(arrayRecette[level - 1], positionListe, Quaternion.identity, GameObject.FindGameObjectWithTag("pageGauche").transform);
        ticketLevelText.SetText(GameManagerBehaviour.instance.ticketMax[level - 1] - GameManagerBehaviour.instance.ticketSpawn[level - 1]+ "/" + GameManagerBehaviour.instance.ticketMax[level - 1]);
        GameManagerBehaviour.instance.levelSelect = level;
        levelText.text = "L" + "\n" + "E" + "\n" + "V" + "\n" + "E" + "\n" + "L" + "\n" + "\n" + level;
        inUseEtoileUn.sprite = etoileEmpty;
        inUseEtoileDeux.sprite = etoileEmpty;
        inUseEtoileTrois.sprite = etoileEmpty;
        cache.SetActive(true);
        if (GameManagerBehaviour.instance.HighScoreList[level - 1] >= GameManagerBehaviour.instance.firstStar[level - 1])
        {
            inUseEtoileUn.sprite = etoileFull;
            cache.SetActive(false);
            if (GameManagerBehaviour.instance.HighScoreList[level - 1] >= GameManagerBehaviour.instance.secondStar[level - 1])
            {
                inUseEtoileDeux.sprite = etoileFull;
                if (GameManagerBehaviour.instance.HighScoreList[level - 1] >= GameManagerBehaviour.instance.thirdStar[level - 1])
                {
                    inUseEtoileTrois.sprite = etoileFull;
                }
            }
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
        yield return new WaitForSeconds(1.3f);
        livreAnim.SetActive(false);
        buttonLivreTicket.SetActive(true);
    }
}
