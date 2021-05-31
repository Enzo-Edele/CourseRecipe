using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerBehaviour : MonoBehaviour
{
    public AudioSource mainMenuTheme;
    public AudioSource bookMenuTheme;
    public AudioSource gameOverTheme;
    public AudioSource buttonSound;
    public AudioSource backButtonSound;

    private static SoundManagerBehaviour _instance;
    public static SoundManagerBehaviour instance
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
    public void PlayMainMenuTheme()
    {
        StopAllSound();
        mainMenuTheme.Play();
    }
    public void PlayBookMenuTheme()
    {
        StopAllSound();
        bookMenuTheme.Play();
    }
    public void PlayGameOverTheme()
    {
        StopAllSound();
        gameOverTheme.Play();
    }
    public void PlayButtonSound()
    {
        buttonSound.Play();
    }
    public void PlayButtonBackSound()
    {
        backButtonSound.Play();
    }

    public void StopAllSound()
    {
        mainMenuTheme.Stop();
        bookMenuTheme.Stop();
        gameOverTheme.Stop();
    }
    public void UnPauseInGameTheme()
    {
        LevelManagerBehaviour.Instance.inGameTheme.UnPause();
    }
}
