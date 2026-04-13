using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class LanguageToggle : MonoBehaviour
{
    [Header("Спрайты состояний")]
    public Sprite rusActiveSprite;
    public Sprite engActiveSprite;

    private Image targetImage;
    private Toggle toggle;

    void Awake()
    {
        targetImage = GetComponent<Image>();
        toggle = GetComponent<Toggle>();

        if (targetImage == null)
        {
            Debug.LogError($"На объекте {gameObject.name} нет компонента Image!");
        }

        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void Start()
    {
        Invoke(nameof(InitialSync), 0.05f);
    }

    void InitialSync()
    {
        if (LocalizationManager.Instance != null)
        {
            bool isEnglish = LocalizationManager.Instance.GetCurrentLanguage() == LocalizationManager.LANG_ENG;

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
        if (targetImage != null)
        {
            targetImage.sprite = isEnglish ? engActiveSprite : rusActiveSprite;
        }
    }
}