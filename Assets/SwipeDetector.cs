using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour
{
    public const float DETECT_WIPE_DISTANCE = 300f;

    private Vector2 startPointerPosition, endPointerPosition;
    public float minSwipeDistance = 50f;
    private Card card;
    private void Awake()
    {
        card = GetComponent<Card>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            startPointerPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPointerPosition = Input.mousePosition;
            float distance = Vector2.Distance(startPointerPosition, endPointerPosition);
            if (distance > DETECT_WIPE_DISTANCE)
            {
                Wipe();
            }
        }
    }

    private void Wipe()
    {
        Vector2 swipeVector = endPointerPosition - startPointerPosition;

        if (swipeVector.magnitude < minSwipeDistance) return;
        float x = swipeVector.x;
        float y = swipeVector.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                Move(Vector2Int.right);
                Debug.Log("Swipe Right");
            } else
            {
                Move(Vector2Int.left);
                Debug.Log("Swipe Left");
            }
        } else
        {
            if (y > 0)
            {
                Move(Vector2Int.down);
                Debug.Log("Swipe Up");
            } else
            {
                Move(Vector2Int.up);
                Debug.Log("Swipe Down");
            }
        }
    }

    private void Move(Vector2Int direction)
    {
        var nextPos = card.Pos + direction;
        Debug.Log(nextPos);
        if (!CardManager.Instance.IsNextToHeroCard(nextPos)) return;
        var nextCard = CardManager.Instance.cards.First(x => x.Pos == nextPos);
        if (nextCard == null) return;
        CardManager.Instance.UseCard(nextCard);
    }
}