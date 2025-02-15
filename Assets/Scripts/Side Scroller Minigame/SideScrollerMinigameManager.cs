using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SideScrollerMinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject backgroundSprite;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Animator playerAnimator;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
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
    }

    private void Start()
    {
        InvokeRepeating("GenerateObstacles", 1f, 3f);
        playing = true;
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
    }

    public void ResumeGame()
    {
        playing = true;
        playerAnimator.SetBool("gamePaused", false);
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);

        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}