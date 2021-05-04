using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagersBaheviour : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("MainMenu");
        GameManagerBehaviour.instance.ChangeGameState(GameManagerBehaviour.GameStates.MainMenu);
    }
}
