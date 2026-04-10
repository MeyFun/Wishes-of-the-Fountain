using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class LanguageToggle : MonoBehaviour
{
    [Header("Спрайты состояний")]
    public Sprite rusActiveSprite; // Плашка, где светится РУС
    public Sprite engActiveSprite; // Плашка, где светится ENG

    private Image targetImage;
    private Toggle toggle;

    void Awake()
    {
        // 1. Сначала находим компоненты
        targetImage = GetComponent<Image>();
        toggle = GetComponent<Toggle>();

        // Проверка: если вдруг забыли добавить Image
        if (targetImage == null)
        {
            Debug.LogError($"На объекте {gameObject.name} нет компонента Image!");
        }

        // Подписываемся на событие
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void Start()
    {
        // Даем менеджеру локализации долю секунды на инициализацию
        Invoke(nameof(InitialSync), 0.05f);
    }

    void InitialSync()
    {
        if (LocalizationManager.Instance != null)
        {
            bool isEnglish = LocalizationManager.Instance.GetCurrentLanguage() == LocalizationManager.LANG_ENG;

            // Отключаем на мгновение слушатель, чтобы не вызывать ChangeLanguage при запуске
            toggle.onValueChanged.RemoveListener(OnToggleChanged);
            toggle.isOn = isEnglish;
            toggle.onValueChanged.AddListener(OnToggleChanged);

            UpdateVisual(isEnglish);
        }
        else
        {
            Debug.LogWarning("LocalizationManager не найден на сцене! Проверьте, есть ли он.");
        }
    }

    public void OnToggleChanged(bool isEnglish)
    {
        UpdateVisual(isEnglish);

        if (LocalizationManager.Instance != null)
        {
            string newLang = isEnglish ? LocalizationManager.LANG_ENG : LocalizationManager.LANG_RUS;
            LocalizationManager.Instance.ChangeLanguage(newLang);
        }
    }

    void UpdateVisual(bool isEnglish)
    {
        // Проверка на null перед использованием (защита от ошибки в логе)
        if (targetImage != null)
        {
            targetImage.sprite = isEnglish ? engActiveSprite : rusActiveSprite;
        }
    }
}