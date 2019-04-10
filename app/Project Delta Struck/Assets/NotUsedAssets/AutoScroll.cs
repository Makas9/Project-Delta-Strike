using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour {
    [SerializeField, Range(1f, 5f)]
    private float scrollSpeed = 0.01f;
    private float waitingTime = 5f;
    private float timeWaited = 0f;
    private float maxScroll;
    private ScrollRect scrollRect;
    private RectTransform contenRectTransform;
    private Vector2 defaultPosition;
    private void Start()
    {
        this.scrollRect = GetComponent<ScrollRect>();
        this.contenRectTransform = this.scrollRect.content;
        this.maxScroll = this.contenRectTransform.rect.yMax;
    }
    private void Update()
    {
        Debug.Log(scrollRect.verticalNormalizedPosition);
        bool hasScrolled = scrollRect.verticalNormalizedPosition > 0.9f || scrollRect.verticalNormalizedPosition < 0.1f;
        if (hasScrolled)
        {
            timeWaited += Time.deltaTime;
            if (timeWaited >= waitingTime)
            {
                scrollSpeed = -scrollSpeed;
            }
            else return;
        }
        this.Move();
    }
    private void Move()
    {
        Vector3 contentPosition = this.contenRectTransform.position;
        float newPositionY = contentPosition.y + this.scrollSpeed;
        Vector3 newPosition = new Vector3(contentPosition.x, newPositionY, contentPosition.z);
        this.contenRectTransform.position = newPosition;
    }
}
