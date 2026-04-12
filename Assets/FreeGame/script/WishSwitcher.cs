using UnityEngine;

public class WishSwitcher : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject selectWishesPanel; // Твоя книга (SelectWishes)
    public GameObject wishesParent;      // Объект Wishes, где лежат уровни

    [Header("Wish Levels")]
    public GameObject wish1;
    public GameObject wish2;
    public GameObject wish3;

    // Этот метод мы будем вызывать из кнопок-монеток
    public void LaunchWish(int index)
    {
        // 1. Выключаем меню выбора (книгу)
        if (selectWishesPanel != null) selectWishesPanel.SetActive(false);

        // 2. Включаем контейнер с уровнями
        if (wishesParent != null) wishesParent.SetActive(true);

        // 3. Выключаем все уровни перед запуском нужного (на всякий случай)
        wish1.SetActive(false);
        wish2.SetActive(false);
        wish3.SetActive(false);

        // 4. Включаем только тот, который выбрали
        switch (index)
        {
            case 1: wish1.SetActive(true); break;
            case 2: wish2.SetActive(true); break;
            case 3: wish3.SetActive(true); break;
            default: Debug.LogWarning("Неверный индекс желания!"); break;
        }

        Debug.Log("Запущено желание " + index);
    }

    // Метод для возврата в книгу (вызывай его из кнопок "Назад" в самих играх)
    public void BackToBook()
    {
        wishesParent.SetActive(false);
        selectWishesPanel.SetActive(true);
    }
}