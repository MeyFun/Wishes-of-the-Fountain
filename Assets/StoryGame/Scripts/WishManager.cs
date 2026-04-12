using UnityEngine;
using TMPro;

public class WishManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI wishTextDisplay;

    private int selectedWishIndex;

    void OnEnable()
    {
        GenerateRandomWish();
    }

    void GenerateRandomWish()
    {
        // 1. Выбираем случайное число от 1 до 3
        selectedWishIndex = Random.Range(1, 4);

        // 2. Сохраняем ID желания, чтобы потом знать, какой уровень загружать
        PlayerPrefs.SetInt("SavedWishID", selectedWishIndex);
        PlayerPrefs.Save();

        // 3. Формируем ключ для твоего менеджера
        string wishKey = "WishText" + selectedWishIndex;

        // 4. Получаем перевод через твой LocalizationManager
        if (LocalizationManager.Instance != null)
        {
            string translatedText = LocalizationManager.Instance.GetTranslation(wishKey);
            wishTextDisplay.text = translatedText;
            Debug.Log($"[WishManager] Выбрано: {wishKey}. Текст: {translatedText}");
        }
        else
        {
            wishTextDisplay.text = "Error: LocalizationManager not found!";
            Debug.LogError("LocalizationManager.Instance is null! Убедись, что объект с этим скриптом есть на сцене.");
        }
    }
}