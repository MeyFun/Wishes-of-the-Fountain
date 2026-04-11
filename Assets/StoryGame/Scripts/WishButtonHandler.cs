using UnityEngine;
using UnityEngine.SceneManagement;

public class WishButtonHandler : MonoBehaviour
{
    public void OnContinueClick()
    {
        // Загружаем сохраненный ID
        int wishID = PlayerPrefs.GetInt("SavedWishID", 1);

        // Логика перехода
        switch (wishID)
        {
            case 1:
                Debug.Log("Запуск локации для желания 1");
                // SceneManager.LoadScene("Level_Forest"); 
                break;
            case 2:
                Debug.Log("Запуск локации для желания 2");
                break;
                // и так далее...
        }
    }
}