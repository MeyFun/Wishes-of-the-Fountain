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
        selectedWishIndex = Random.Range(1, 4);

        PlayerPrefs.SetInt("SavedWishID", selectedWishIndex);
        PlayerPrefs.Save();

        string wishKey = "WishText" + selectedWishIndex;

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