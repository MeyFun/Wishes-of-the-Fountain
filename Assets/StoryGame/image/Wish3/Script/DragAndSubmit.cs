using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class DragAndSubmit : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    [Header("Settings")]
    public string correctBoyTag = "SubmitBoy";

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.grabObjectSound);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas != null)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.putObjectSound);
        Debug.Log("Предмет отпущен: " + gameObject.name);
    }
}