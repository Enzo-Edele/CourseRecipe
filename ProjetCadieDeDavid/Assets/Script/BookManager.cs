using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BookManager : MonoBehaviour
{
    int levelMax = 13;
    int levelProgress;
    int levelSelect = 1;

    readonly string listeRecetteUn = "Liste Recette Un";
    readonly string listeRecetteDeux = "Liste Recette Deux";
    readonly string listeRecetteTrois = "Liste Recette Trois";
    readonly string listeRecetteQuatre = "Liste Recette Quatre";
    readonly string listeRecetteCinq = "Liste Recette Cinq";
    readonly string listeRecetteSix = "Liste Recette Six";
    readonly string listeRecetteSept = "Liste Recette Sept";
    readonly string listeRecetteHuit = "Liste Recette Huit";
    readonly string listeRecetteNeuf = "Liste Recette Neuf";
    readonly string listeRecetteDix = "Liste Recette Dix";
    readonly string listeRecetteOnze = "Liste Recette Onze";
    readonly string listeRecetteDouze = "Liste Recette Douze";
    readonly string listeRecetteTreize = "Liste Recette Treize";

    public TMP_Text listRecette;

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
        levelProgress = GameManagerBehaviour.instance.level;
    }
    private void Start()
    {
        _instance = this;
    }
    public void LevelLoad()
    {
        Debug.Log("LevelPreogress : " + levelProgress);
        if (levelSelect <= levelProgress)
        {
            SceneManager.LoadScene("Level " + levelSelect);
        }
    }
    public void NextLevel()
    {
        if (levelSelect < levelMax)
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
                listRecette.text = listeRecetteUn;
                recette.text = recetteUn;
                break;
            case 2:
                listRecette.text = listeRecetteDeux;
                recette.text = recetteDeux;
                break;
            case 3:
                listRecette.text = listeRecetteTrois;
                recette.text = recetteTrois;
                break;
            case 4:
                listRecette.text = listeRecetteQuatre;
                recette.text = recetteQuatre;
                break;
            case 5:
                listRecette.text = listeRecetteCinq;
                recette.text = recetteCinq;
                break;
            case 6:
                listRecette.text = listeRecetteSix;
                recette.text = recetteSix;
                break;
            case 7:
                listRecette.text = listeRecetteSept;
                recette.text = recetteSept;
                break;
            case 8:
                listRecette.text = listeRecetteHuit;
                recette.text = recetteHuit;
                break;
            case 9:
                listRecette.text = listeRecetteNeuf;
                recette.text = recetteNeuf;
                break;
            case 10:
                listRecette.text = listeRecetteDix;
                recette.text = recetteDix;
                break;
            case 11:
                listRecette.text = listeRecetteOnze;
                recette.text = recetteOnze;
                break;
            case 12:
                listRecette.text = listeRecetteDouze;
                recette.text = recetteDouze;
                break;
            case 13:
                listRecette.text = listeRecetteTreize;
                recette.text = recetteTreize;
                break;
        }
        InUse.sprite = newImageRecette[level - 1];
        levelSelect = level;
    }
}
