using UnityEngine;
using System.Collections.Generic;
using System.IO;
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    public const string LANG_RUS = "Russian";
    public const string LANG_ENG = "English";

    [SerializeField] private string currentLanguage = LANG_RUS;

    private Dictionary<string, Dictionary<string, string>> localizationData;

    private bool isReady = false;

    public delegate void LanguageChangedHandler();
    public event LanguageChangedHandler OnLanguageChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLocalizationData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Загрузка данных из CSV-файла в Resources
    void LoadLocalizationData()
    {
        localizationData = new Dictionary<string, Dictionary<string, string>>();

        TextAsset csvFile = Resources.Load<TextAsset>("Localization");
        if (csvFile == null)
        {
            Debug.LogError("Localization file не найден в Resources/Localization.csv");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        string[] headers = lines[0].Trim().Split(',');
        List<string> languagesInFile = new List<string>();
        for (int i = 1; i < headers.Length; i++)
        {
            languagesInFile.Add(headers[i]);
        }

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] row = lines[i].Trim().Split(',');
            string key = row[0];

            Dictionary<string, string> translations = new Dictionary<string, string>();
            for (int j = 1; j < row.Length; j++)
            {
                if (j < headers.Length)
                {
                    translations.Add(headers[j], row[j]);
                }
            }

            if (!localizationData.ContainsKey(key))
            {
                localizationData.Add(key, translations);
            }
            else
            {
                Debug.LogWarning($"Duplicate localization key found: {key}");
            }
        }

        isReady = true;
    }

    public string GetTranslation(string key)
    {
        if (!isReady || localizationData == null) return $"[WAITING_{key}]";

        if (localizationData.ContainsKey(key))
        {
            Dictionary<string, string> translations = localizationData[key];
            if (translations.ContainsKey(currentLanguage))
            {
                return translations[currentLanguage];
            }
            else
            {
                Debug.LogWarning($"Ключа перевода '{key}' не найден в {currentLanguage}. Включаем русский");
                if (translations.ContainsKey(LANG_RUS)) return translations[LANG_RUS];
                return $"[MISSING_{key}_{currentLanguage}]";
            }
        }

        return $"[KEY_NOT_FOUND_{key}]";
    }

    public void ChangeLanguage(string newLanguage)
    {
        if (newLanguage != LANG_RUS && newLanguage != LANG_ENG)
        {
            Debug.LogError($"Unsupported language: {newLanguage}");
            return;
        }

        if (currentLanguage != newLanguage)
        {
            currentLanguage = newLanguage;
            Debug.Log($"Language changed to: {currentLanguage}");

            // Оповещаем все подписанные объекты TMP_Text, чтобы они обновились
            OnLanguageChanged?.Invoke();
        }
    }

    public string GetCurrentLanguage() => currentLanguage;
}