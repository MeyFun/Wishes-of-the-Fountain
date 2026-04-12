using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        startPosition = rectTransform.anchoredPosition; // Запоминаем место, если промахнемся мимо урны
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // Делаем прозрачным при переносе
        canvasGroup.blocksRaycasts = false; // Позволяет мышке "видеть" урну под предметом
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Двигаем предмет за мышкой
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Если при отпускании мышки под нами нет урны — возвращаем на место
        if (eventData.pointerEnter == null || eventData.pointerEnter.name != "TrashBin")
        {
            rectTransform.anchoredPosition = startPosition;
        }
    }

    // Вызывается менеджером, когда предмет попал в цель
    public void ItemCollected()
    {
        gameObject.SetActive(false); // Убираем предмет
    }
}