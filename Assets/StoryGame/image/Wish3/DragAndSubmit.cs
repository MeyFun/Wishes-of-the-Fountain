using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))] // Для работы OnTriggerEnter
public class DragAndSubmit : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [Header("Settings")]
    public string correctBoyTag = "SubmitBoy"; // Тэг, который мы дадим мальчику

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // Логика перетаскивания (как во 2 задании, но без возврата)
    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    // Когда мы отпускаем мышку
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Предмет отпущен: " + gameObject.name);
        // Здесь мы больше ничего не делаем. Коллизия проверяется через Триггеры.
    }
}