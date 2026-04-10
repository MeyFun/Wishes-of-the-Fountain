using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))] // Добавляем CanvasGroup для плавного появления контроллов
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
        // Получаем или добавляем CanvasGroup на панель контроллов для плавного появления
        if (settingsControlsPanel)
        {
            controlsCanvasGroup = settingsControlsPanel.GetComponent<CanvasGroup>();
            if (controlsCanvasGroup == null)
            {
                controlsCanvasGroup = settingsControlsPanel.AddComponent<CanvasGroup>();
            }
        }
    }

    // --- ЛОГИКА ВКЛЮЧЕНИЯ (Вход в Настройки) ---

    // Этот метод вызывается Unity автоматически при SetActive(true) на SettingsMenu
    void OnEnable()
    {
        // Подготовка к анимации входа:
        // 1. Обычный фон полностью виден (Alpha = 1)
        SetImageAlpha(normalBackground, 1f);

        // 2. Размытый фон активен под ним
        if (blurredBackground) blurredBackground.gameObject.SetActive(true);

        // 3. Скрываем элементы управления настройками (через Alpha), чтобы они плавно появились позже
        if (controlsCanvasGroup)
        {
            settingsControlsPanel.SetActive(true); // Включаем сам объект
            controlsCanvasGroup.alpha = 0f;       // Но делаем прозрачным
        }

        // 4. Запускаем корутину плавного исчезновения обычного фона
        StartFade(0f, true); // targetAlpha = 0 (прозрачный), isEntering = true
    }


    // --- ЛОГИКА ВЫХОДА (Возврат в Главное Меню) ---

    // **ЭТУ ФУНКЦИЮ ПОВЕСИТЬ НА КНОПКУ "НАЗАД" В НАСТРОЙКАХ**
    // В поле GameObject перетащите объект MainMenu из иерархии.
    public void CloseSettingsAndReturn(GameObject mainMenuToEnable)
    {
        if (mainMenuToEnable == null)
        {
            Debug.LogError("Ошибка: Не указан объект MainMenu для включения в кнопке Назад!");
            // Если забыли указать меню, просто резко закрываем настройки, чтобы не "зависло"
            gameObject.SetActive(false);
            return;
        }

        // Запускаем корутину плавного появления обычного фона обратно
        StartFade(1f, false, mainMenuToEnable); // targetAlpha = 1 (видимый), isEntering = false
    }


    // --- ЕДИНАЯ ЛОГИКА АНИМАЦИИ (Корутина) ---

    private void StartFade(float targetAlpha, bool isEntering, GameObject menuToEnable = null)
    {
        // Если старая анимация еще идет, останавливаем её
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        // Запускаем новую анимацию
        fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha, isEntering, menuToEnable));
    }

    IEnumerator FadeRoutine(float targetAlpha, bool isEntering, GameObject menuToEnable)
    {
        // Проверка на ошибки
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