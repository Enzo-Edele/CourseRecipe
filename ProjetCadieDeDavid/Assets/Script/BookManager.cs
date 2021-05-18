using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BookManager : MonoBehaviour
{
    int levelProgress;
    public int levelSelect;

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
    public TMP_Text score;

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
    Image InUse;

    public Sprite[] newImageRecette;

    public List<int> scoreList;

    private BookManager _instance;
    public BookManager instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        InUse = ImageRecette.GetComponent<Image>();
        ImageRecette.GetComponent<Image>().preserveAspect = true;
        levelProgress = GameManagerBehaviour.instance.level;
        levelSelect = 1;
    }
    private void Start()
    {
        _instance = this;
    }
    public void LevelLoad()
    {
        if (levelSelect <= levelProgress)
        {
            SceneManager.LoadScene("Level " + levelSelect);
            GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.InGame);
        }
    }
    public void NextLevel()
    {
        if (levelSelect < GameManagerBehaviour.instance.maxLevel)
        {
            levelSelect++;
        }
        ChangeSelect(levelSelect);
    }
    public void PreviousLevel()
    {
        if (levelSelect > 1)
        {
            levelSelect--;
        }
        ChangeSelect(levelSelect);
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
        InUse.sprite = newImageRecette[level - 1];
        levelSelect = level;
        score.text = "Score : " + GameManagerBehaviour.instance.scoreList[level - 1];
    }
}
