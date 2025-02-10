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
    [SerializeField] private GameObject bigObstacle;
    [SerializeField] private Transform spawnPosition;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;
    private GameObject goToSpawn;
    private Vector3 bgPos;
    private float score = 0;
    public float maxSpeed;
    private float speedMultiplier = 0.001f;

    public float speed;
    [NonSerialized] public bool gameOver;

    private void Awake()
    {
        bgPos = backgroundSprite.transform.position;
        goToSpawn = obstacle;
    }

    private void Start()
    {
        InvokeRepeating("GenerateObstacles", 1f, 3f);
    }

    private void Update()
    {
        if (gameOver)
        {
            gameOverPanel.SetActive(true);
            
            return;
        }  

        IncreaseSpeed();
        IncreaseScore();

        backgroundSprite.transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(backgroundSprite.transform.position.x < 80)
        {
            backgroundSprite.transform.position = bgPos;
        }
    }

    private void GenerateObstacles()
    {
        if (!gameOver)
        {
            Instantiate(goToSpawn, new Vector3(spawnPosition.position.x, spawnPosition.position.y + (goToSpawn.GetComponent<BoxCollider2D>().size.y / 2), spawnPosition.position.z), Quaternion.identity);
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
        score += Time.deltaTime * 1.15f;
        scoreText.text = score.ToString("F2");

        if(score >= 50)
        {
            goToSpawn = bigObstacle;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}