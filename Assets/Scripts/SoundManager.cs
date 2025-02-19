using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Game SFX")]
    public AudioSource mainTrack;
    public AudioSource menuMusic;
    public AudioSource pauseSound;
    public AudioSource gameOverSound;
    
    [Header("Player SFX")]
    public AudioSource jumpSound;
    public AudioSource landingSound;

    [Header("UI SFX")]
    public AudioSource buttonConfirmSound;
    public AudioSource buttonCancelSound;
    public AudioSource buttonPauseSound;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}