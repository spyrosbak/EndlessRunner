using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private SideScrollerMinigameManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<SideScrollerMinigameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * (manager.speed + 0.5f) * Time.deltaTime);

        if(transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}