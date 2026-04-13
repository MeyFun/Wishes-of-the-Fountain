using UnityEngine;

public class WishSwitcher : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject selectWishesPanel;
    public GameObject wishesParent;

    [Header("Wish Levels")]
    public GameObject wish1;
    public GameObject wish2;
    public GameObject wish3;

    public void LaunchWish(int index)
    {
        if (selectWishesPanel != null) selectWishesPanel.SetActive(false);

        if (wishesParent != null) wishesParent.SetActive(true);

        wish1.SetActive(false);
        wish2.SetActive(false);
        wish3.SetActive(false);

        switch (index)
        {
            case 1: wish1.SetActive(true); break;
            case 2: wish2.SetActive(true); break;
            case 3: wish3.SetActive(true); break;
            default: Debug.LogWarning("Неверный индекс желания!"); break;
        }

        Debug.Log("Запущено желание " + index);
    }
}