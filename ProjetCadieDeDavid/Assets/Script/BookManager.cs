using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BookManager : MonoBehaviour
{
    int levelProgress;

    public GameObject listeRecetteUn;
    public GameObject listeRecetteDeux;
    public GameObject listeRecetteTrois;
    public GameObject listeRecetteQuatre;
    public GameObject listeRecetteCinq;
    public GameObject listeRecetteSix;
    public GameObject listeRecetteSept;
    public GameObject listeRecetteHuit;
    public GameObject listeRecetteNeuf;
    public GameObject listeRecetteDix;
    public GameObject listeRecetteOnze;
    public GameObject listeRecetteDouze;
    public GameObject listeRecetteTreize;

    public GameObject listRecette;
    public TMP_Text levelText;
    public TMP_Text score;
    public GameObject etoileUn, etoileDeux, etoileTrois;

    readonly string recetteUn = "Recette Un";
    readonly string recetteDeux = "Recette Deux";
    readonly string recetteTrois = "Recette Trois";
    readonly string recetteQuatre = "Recette Quatre";
    readonly string recetteCinq = "Recette Cinq";
    readonly string recetteSix = "Recette Six";
    readonly string recetteSept = "Recette Sept";
    readonly string recetteHuit = "Recette Huit";
    readonly string recetteNeuf = "Recette Neuf";
    readonly string recetteDix = "Recette Dix";
    readonly string recetteOnze = "Recette Onze";
    readonly string recetteDouze = "Recette Douze";
    readonly string recetteTreize = "Recette Treize";
    
    public TMP_Text recette;
    public GameObject ImageRecette;
    Image inUseRecette;
    public Sprite[] newImageRecette;

    Image inUseEtoileUn, inUseEtoileDeux, inUseEtoileTrois;
    public Sprite etoileFull, etoileEmpty;
    public List<int> scoreList;

    public Image livreAnim;
    Animator anim;

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
        this.ChangeSelect(1);
    }
    private void Start()
    {
        _instance = this;
        GameObject book = GameObject.Find("Book");
        this.transform.SetParent(book.transform);
    }
    public void LevelLoad()
    {
        if (GameManagerBehaviour.instance.levelSelect <= levelProgress)
        {
            SceneManager.LoadScene("Level " + GameManagerBehaviour.instance.levelSelect);
            GameManagerBehaviour.instance.ChangeGameStateByUI(2);
        }
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
    
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    public void ChangeSelect(int level)
    {
        switch(level)
        {
            case 1:
                listRecette = listeRecetteUn;
                recette.text = recetteUn;
                break;
            case 2:
                listRecette = listeRecetteDeux;
                recette.text = recetteDeux;
                break;
            case 3:
                listRecette = listeRecetteTrois;
                recette.text = recetteTrois;
                break;
            case 4:
                listRecette = listeRecetteQuatre;
                recette.text = recetteQuatre;
                break;
            case 5:
                listRecette = listeRecetteCinq;
                recette.text = recetteCinq;
                break;
            case 6:
                listRecette = listeRecetteSix;
                recette.text = recetteSix;
                break;
            case 7:
                listRecette = listeRecetteSept;
                recette.text = recetteSept;
                break;
            case 8:
                listRecette = listeRecetteHuit;
                recette.text = recetteHuit;
                break;
            case 9:
                listRecette = listeRecetteNeuf;
                recette.text = recetteNeuf;
                break;
            case 10:
                listRecette = listeRecetteDix;
                recette.text = recetteDix;
                break;
            case 11:
                listRecette = listeRecetteOnze;
                recette.text = recetteOnze;
                break;
            case 12:
                listRecette = listeRecetteDouze;
                recette.text = recetteDouze;
                break;
            case 13:
                listRecette = listeRecetteTreize;
                recette.text = recetteTreize;
                break;
        }
        anim.SetTrigger("turnPage");
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
    }
}
