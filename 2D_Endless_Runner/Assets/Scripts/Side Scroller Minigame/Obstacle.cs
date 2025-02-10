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
        transform.Translate(Vector2.left * manager.speed * Time.deltaTime);

        if(transform.position.x < 85f)
        {
            Destroy(gameObject);
        }
    }
}