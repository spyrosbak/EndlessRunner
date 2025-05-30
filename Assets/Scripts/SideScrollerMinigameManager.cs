using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class SideScrollerMinigameManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput;
    private InputActionMap inputMap;
    private InputAction pauseAction;

    [SerializeField] private GameObject backgroundSprite;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Animator playerAnimator;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    private Vector3 bgPos;
    private float score = 0;
    public float maxSpeed;
    private float speedMultiplier = 0.0007f;

    public float speed;
    [NonSerialized] public bool gameOver;
    [NonSerialized] public bool playing;

    private void Awake()
    {
        bgPos = backgroundSprite.transform.position;

        inputMap = playerInput.FindActionMap("Player");
        pauseAction = inputMap.FindAction("Pause");
    }

    private void OnEnable()
    {
        pauseAction.Enable();
    }

    private void OnDisable()
    {
        pauseAction.Disable();
    }

    private void Start()
    {
        playing = true;
        InvokeRepeating("GenerateObstacles", 1f, 3f);
        SoundManager.Instance.mainTrack.Play();
    }

    private void Update()
    {
        if (gameOver)
        {
            StartCoroutine(GameOver());
            
            return;
        }

        if (playing)
        {
            IncreaseSpeed();
            IncreaseScore();

            backgroundSprite.transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (backgroundSprite.transform.position.x < -12.75f)
            {
                backgroundSprite.transform.position = bgPos;
            }

            if (pauseAction.triggered)
            {
                PauseGame();
                pausePanel.SetActive(true);
            }
        }
        else
        {
            if (pauseAction.triggered)
            {
                ResumeGame();
                pausePanel.SetActive(false);
            }
        }

    }

    private void GenerateObstacles()
    {
        if (!gameOver && playing)
        {
            Instantiate(obstacle, new Vector3(spawnPosition.position.x, spawnPosition.position.y + 0.3f, spawnPosition.position.z), Quaternion.identity);
        }
    }

    private void IncreaseSpeed()
    {
        if (speed < maxSpeed)
        {
            speed += speedMultiplier;
        }
        else
        {
            speed = maxSpeed;
        }
    }

    private void IncreaseScore()
    {
        score +=  1 * 0.025f;
        scoreText.text = score.ToString("F0");
    }

    public void PauseGame()
    {
        playing = false;
        playerAnimator.SetBool("gamePaused", true);
        SoundManager.Instance.mainTrack.Pause();
        SoundManager.Instance.buttonPauseSound.Play();
        SoundManager.Instance.pauseSound.PlayDelayed(1f);
    }

    public void ResumeGame()
    {
        playing = true;
        playerAnimator.SetBool("gamePaused", false);
        SoundManager.Instance.mainTrack.UnPause();
        SoundManager.Instance.pauseSound.Stop();
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (SoundManager.Instance.mainTrack.volume > 0)
        {
            SoundManager.Instance.mainTrack.volume -= Time.deltaTime * 0.25f;
        }

        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        SoundManager.Instance.buttonConfirmSound.Play();
        SoundManager.Instance.mainTrack.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SoundManager.Instance.buttonConfirmSound.Play();
        SoundManager.Instance.mainTrack.Stop();
        
        SceneManager.LoadScene(0);

        SoundManager.Instance.menuMusic.Play();
    }
}