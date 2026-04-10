using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class BlurMainScreen : MonoBehaviour
{
    [Header("Настройка фонов (UI Images)")]
    [Tooltip("Обычный, четкий фон в группе SettingsMenu. Лежит ПОВЕРХ размытого.")]
    public Image normalBackground;

    [Tooltip("Уже размытый фон в группе SettingsMenu. Лежит ПОД обычным.")]
    public Image blurredBackground;

    [Header("Настройки анимации")]
    [Tooltip("Длительность плавного перехода в секундах")]
    public float fadeDuration = 0.4f;

    [Header("UI Слияние (Опционально)")]
    [Tooltip("Объект с кнопками/слайдерами настроек. Он появится ПОСЛЕ замыливания.")]
    public GameObject settingsControlsPanel;

    private CanvasGroup controlsCanvasGroup;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (settingsControlsPanel)
        {
            controlsCanvasGroup = settingsControlsPanel.GetComponent<CanvasGroup>();
            if (controlsCanvasGroup == null)
            {
                controlsCanvasGroup = settingsControlsPanel.AddComponent<CanvasGroup>();
            }
        }
    }
    
    void OnEnable()
    {
        SetImageAlpha(normalBackground, 1f);

        if (blurredBackground) blurredBackground.gameObject.SetActive(true);

        if (controlsCanvasGroup)
        {
            settingsControlsPanel.SetActive(true);
            controlsCanvasGroup.alpha = 0f;
        }

        StartFade(0f, true);
    }

    public void CloseSettingsAndReturn(GameObject mainMenuToEnable)
    {
        if (mainMenuToEnable == null)
        {
            Debug.LogError("Ошибка: Не указан объект MainMenu для включения в кнопке Назад!");
            gameObject.SetActive(false);
            return;
        }

        StartFade(1f, false, mainMenuToEnable);
    }


    // --- ЕДИНАЯ ЛОГИКА АНИМАЦИИ (Корутина) ---

    private void StartFade(float targetAlpha, bool isEntering, GameObject menuToEnable = null)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha, isEntering, menuToEnable));
    }

    IEnumerator FadeRoutine(float targetAlpha, bool isEntering, GameObject menuToEnable)
    {
        if (normalBackground == null)
        {
            Debug.LogError("Ошибка: Не назначен Normal Background в скрипте BlurSettingsTransition на " + gameObject.name);
            yield break;
        }

        float startAlpha = normalBackground.color.a;
        float elapsedTime = 0f;

        // Если выходим, сначала плавно скрываем элементы управления
        if (!isEntering && controlsCanvasGroup)
        {
            yield return StartCoroutine(FadeControls(0f)); // Скрываем контроллы за половину времени
        }

        // Основной цикл анимации фона (замыливание/размыливание)
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;

            // Используем SmoothStep для более "мягкой" анимации в начале и конце
            float smoothedT = Mathf.SmoothStep(0f, 1f, t);

            // Плавно интерполируем Alpha от стартовой до целевой
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, smoothedT);
            SetImageAlpha(normalBackground, newAlpha);

            yield return null;
        }

        // Финально устанавливаем точное целевое значение
        SetImageAlpha(normalBackground, targetAlpha);

        // Действия ПОСЛЕ завершения анимации фона
        if (isEntering)
        {
            // Если входили, плавно включаем элементы управления
            if (controlsCanvasGroup)
            {
                yield return StartCoroutine(FadeControls(1f)); // Показываем контроллы
            }
        }
        else
        {
            // Если выходили, выполняем переключение экранов
            if (menuToEnable) menuToEnable.SetActive(true); // Включаем Главное Меню
            gameObject.SetActive(false);                   // Выключаем Настройки
        }

        fadeCoroutine = null;
    }

    // Вспомогательная корутина для плавного появления/исчезновения панели контроллов
    IEnumerator FadeControls(float targetAlpha)
    {
        if (controlsCanvasGroup == null) yield break;

        float startAlpha = controlsCanvasGroup.alpha;
        float elapsedTime = 0f;
        float duration = fadeDuration * 0.5f; // Анимация контроллов быстрее основной

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            controlsCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }
        controlsCanvasGroup.alpha = targetAlpha;
    }

    // Вспомогательный метод для изменения Alpha канала
    private void SetImageAlpha(Image image, float alpha)
    {
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}