using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 startPos;
    private float scrollSpeed = 50f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    private void Update()
    {
        if(rectTransform.anchoredPosition.y < 800)
        {
            rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
        else
        {
            return;
        }
        
    }
    
    public void ResetCredits()
    {
        rectTransform.anchoredPosition = startPos;
    }
}