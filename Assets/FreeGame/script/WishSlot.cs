using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WishSlot : MonoBehaviour
{
    [Header("Настройки ID")]
    public string wishID; // Уникальное ID (например, "Level_House", "Level_Delivery")
    public int wishIndex; // Номер игры в списке Wishes (0, 1, 2...)

    [Header("Ссылки на объекты")]
    public GameObject coinImage;     // Объект монетки
    public GameObject descriptionText; // Объект с текстом
    public Button playButton;        // Кнопка (сама монетка или невидимая область)

    private WishButtonHandler wishHandler;

    void Awake()
    {
        // Ищем обработчик желаний на сцене
        wishHandler = Object.FindFirstObjectByType<WishButtonHandler>();
    }

    void OnEnable()
    {
        RefreshSlot();
    }

    public void RefreshSlot()
    {
        // Проверяем, выполнено ли задание (сохранено ли значение 1)
        bool isCompleted = PlayerPrefs.GetInt(wishID, 0) == 1;

        // Показываем/скрываем элементы
        if (coinImage != null) coinImage.SetActive(isCompleted);
        if (descriptionText != null) descriptionText.SetActive(isCompleted);

        // Если задание выполнено, кнопку можно нажать, чтобы переиграть
        if (playButton != null)
        {
            playButton.interactable = isCompleted;
            playButton.onClick.RemoveAllListeners();
            playButton.onClick.AddListener(OnSlotClick);
        }
    }

    public void OnSlotClick()
    {
        if (wishHandler != null)
        {
            // Выключаем меню выбора и включаем нужную игру
            wishHandler.selectWishesUI.SetActive(false);
            wishHandler.wishesParent.SetActive(true);

            // Включаем конкретный уровень по индексу
            wishHandler.OnWishButtonClick(wishIndex);
        }
    }
}