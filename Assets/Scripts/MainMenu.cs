using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);

        SoundManager.Instance.mainTrack.Play();
        SoundManager.Instance.menuMusic.Stop();
    }

    public void PlayCorfirmSound()
    {
        SoundManager.Instance.buttonConfirmSound.Play();
    }

    public void PlayCancelSound()
    {
        SoundManager.Instance.buttonCancelSound.Play();
    }
}