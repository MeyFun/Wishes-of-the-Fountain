using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    public LevelManager levelManager;

    public void OnDrop(PointerEventData eventData)
    {
        // Проверяем, что в нас бросили именно предмет
        if (eventData.pointerDrag != null)
        {
            DragAndDrop draggedItem = eventData.pointerDrag.GetComponent<DragAndDrop>();
            if (draggedItem != null)
            {
                draggedItem.ItemCollected(); // Прячем предмет
                levelManager.ObjectThrownInTrash(); // Сообщаем менеджеру
                Debug.Log("Предмет собран!");
            }
        }
    }
}