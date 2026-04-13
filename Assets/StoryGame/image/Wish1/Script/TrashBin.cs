using UnityEngine;
using UnityEngine.EventSystems;

public class TrashBin : MonoBehaviour, IDropHandler
{
    public LevelManager levelManager;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragAndDrop draggedItem = eventData.pointerDrag.GetComponent<DragAndDrop>();
            if (draggedItem != null)
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.putObjectSound);
                draggedItem.ItemCollected();
                levelManager.ObjectThrownInTrash();
                Debug.Log("Предмет собран!");
            }
        }
    }
}