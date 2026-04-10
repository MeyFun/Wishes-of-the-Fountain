using UnityEngine;
using TMPro;

// Автоматически добавляет компонент TextMeshProUGUI, если его нет
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    // Ключ из CSV-файла (задается в Инспекторе для каждого текста)
    [SerializeField] private string localizationKey;

    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        // Пробуем обновить текст сразу
        UpdateText();

        // Подписываемся на событие смены языка
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged += UpdateText;
        }
    }

    void OnDisable()
    {
        // Обязательно отписываемся, чтобы избежать утечек памяти
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
        }
    }

    // Сама функция обновления текста
    void UpdateText()
    {
        if (textComponent == null || LocalizationManager.Instance == null) return;

        if (string.IsNullOrEmpty(localizationKey))
        {
            Debug.LogWarning($"LocalizedText on {gameObject.name} has no key.");
            return;
        }

        // Запрашиваем перевод у Менеджера
        textComponent.text = LocalizationManager.Instance.GetTranslation(localizationKey);
    }
}   