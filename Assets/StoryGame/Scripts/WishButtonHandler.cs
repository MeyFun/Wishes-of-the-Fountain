using UnityEngine;

public class WishButtonHandler : MonoBehaviour
{
    [Header("Main Groups")]
    public GameObject selectWishesUI; // Группа со свитком/описанием желания
    public GameObject wishesParent;   // Родительская группа Wishes

    [Header("Levels List")]
    // Перетащи сюда свои объекты Wish1, Wish2... в строгом порядке
    // Индекс 0 = Wish1, Индекс 1 = Wish2 и т.д.
    public GameObject[] wishLevels;

    public void OnContinueClick()
    {
        // 1. Получаем ID (от 1 до 5), который сохранили в WishManager
        int wishID = PlayerPrefs.GetInt("SavedWishID", 1);

        // 2. Выключаем экран выбора желания
        if (selectWishesUI != null)
        {
            selectWishesUI.SetActive(false);
        }

        // 3. Включаем общую группу Wishes (если она была выключена)
        if (wishesParent != null)
        {
            wishesParent.SetActive(true);
        }

        // 4. Логика включения конкретного уровня
        // Вычитаем 1, так как в массиве нумерация с нуля
        int index = wishID - 1;

        if (index >= 0 && index < wishLevels.Length)
        {
            // Выключаем на всякий случай все уровни, кроме нужного
            for (int i = 0; i < wishLevels.Length; i++)
            {
                wishLevels[i].SetActive(i == index);
            }

            Debug.Log($"[Continue] Запущен уровень Wish_{wishID}");
        }
        else
        {
            Debug.LogError($"[Continue] Ошибка: Индекс {index} вне диапазона массива уровней!");
        }
    }
}