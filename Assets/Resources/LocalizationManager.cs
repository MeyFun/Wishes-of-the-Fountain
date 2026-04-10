using UnityEngine;
using System.Collections.Generic;
using System.IO;

// Убедитесь, что у вас установлен TextMeshPro, если используете TMP_Text
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    // Singletone-паттерн
    public static LocalizationManager Instance { get; private set; }

    // Константы для языков (чтобы избежать опечаток)
    public const string LANG_RUS = "Russian";
    public const string LANG_ENG = "English";

    // Текущий язык по умолчанию
    [SerializeField] private string currentLanguage = LANG_RUS;

    // Словарь: Ключ -> (Язык -> Перевод)
    // dictionary["menu_music"]["English"] = "Music"
    private Dictionary<string, Dictionary<string, string>> localizationData;

    private bool isReady = false;

    // Событие, на которое подпишутся все TMP_Text объекты в игре
    public delegate void LanguageChangedHandler();
    public event LanguageChangedHandler OnLanguageChanged;

    void Awake()
    {
        // Настройка Singletone
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Объект не уничтожается при смене сцен
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

        // Загружаем файл как TextAsset ( Localization.csv в папке Assets/Resources)
        TextAsset csvFile = Resources.Load<TextAsset>("Localization");
        if (csvFile == null)
        {
            Debug.LogError("Localization file not found in Resources/Localization.csv");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        // Читаем первую строку, чтобы узнать названия языков
        string[] headers = lines[0].Trim().Split(',');
        List<string> languagesInFile = new List<string>();
        for (int i = 1; i < headers.Length; i++) // Пропускаем Key (index 0)
        {
            languagesInFile.Add(headers[i]);
        }

        // Читаем данные
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] row = lines[i].Trim().Split(',');
            string key = row[0];

            Dictionary<string, string> translations = new Dictionary<string, string>();
            for (int j = 1; j < row.Length; j++)
            {
                // Если колонка в CSV существует для этого языка
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
        Debug.Log("Localization Manager loaded " + localizationData.Count + " keys.");
    }

    // Основная функция: Получение перевода по ключу
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
                // Если перевода на текущий язык нет, пробуем English (или Key)
                Debug.LogWarning($"Translation for key '{key}' not found in {currentLanguage}. Trying defaults.");
                if (translations.ContainsKey(LANG_ENG)) return translations[LANG_ENG];
                return $"[MISSING_{key}_{currentLanguage}]";
            }
        }

        return $"[KEY_NOT_FOUND_{key}]"; // Визуальная подсказка в игре, если ключа нет
    }

    // Функция для смены языка (например, из Настроек)
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