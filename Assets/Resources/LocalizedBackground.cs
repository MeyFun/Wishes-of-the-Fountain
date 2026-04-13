using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LocalizedBackground : MonoBehaviour
{
    [Header("Спрайты для разных языков")]
    public Sprite russianSprite;
    public Sprite englishSprite;

    private Image imageComponent;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    void OnEnable()
    {
        UpdateBackground();
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged += UpdateBackground;
        }
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateBackground;
        }
    }

    void UpdateBackground()
    {
        if (imageComponent == null || LocalizationManager.Instance == null) return;

        string currentLang = LocalizationManager.Instance.GetCurrentLanguage();

        if (currentLang == LocalizationManager.LANG_RUS)
        {
            imageComponent.sprite = russianSprite;
        }
        else if (currentLang == LocalizationManager.LANG_ENG)
        {
            imageComponent.sprite = englishSprite;
        }
    }
}   