using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizedText : MonoBehaviour
{
    [SerializeField] private string localizationKey;

    private TextMeshProUGUI textComponent;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        UpdateText();

        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged += UpdateText;
        }
    }

    void OnDisable()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
        }
    }

    void UpdateText()
    {
        if (textComponent == null || LocalizationManager.Instance == null) return;

        if (string.IsNullOrEmpty(localizationKey))
        {
            Debug.LogWarning($"LocalizedText on {gameObject.name} has no key.");
            return;
        }

        textComponent.text = LocalizationManager.Instance.GetTranslation(localizationKey);
    }
}   