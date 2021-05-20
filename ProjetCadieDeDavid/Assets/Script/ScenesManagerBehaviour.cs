using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManagerBehaviour : MonoBehaviour
{
    private static ScenesManagerBehaviour _instance;
    public static ScenesManagerBehaviour instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
    public void LoadCredit()
    {
        SceneManager.LoadScene("Crédit");
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadRecipeScene()
    {
        SceneManager.LoadScene("Recipe");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(!LevelManagerBehaviour.Instance.levelDone)
        {
            GameManagerBehaviour.instance.ResetCoinAndTicket();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        SaveSysteme.Save(GameManagerBehaviour.instance);
    }
}
