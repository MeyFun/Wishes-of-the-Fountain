using UnityEngine;

public class WishButtonHandler : MonoBehaviour
{
    [Header("Main Groups")]
    public GameObject selectWishesUI;
    public GameObject wishesParent;

    [Header("Levels List")]
    public GameObject[] wishLevels;

    public void OnContinueClick()
    {
        int wishID = PlayerPrefs.GetInt("SavedWishID", 1);

        if (selectWishesUI != null)
        {
            selectWishesUI.SetActive(false);
        }

        if (wishesParent != null)
        {
            wishesParent.SetActive(true);
        }

        int index = wishID - 1;

        if (index >= 0 && index < wishLevels.Length)
        {
            for (int i = 0; i < wishLevels.Length; i++)
            {
                wishLevels[i].SetActive(i == index);
            }

            Debug.Log($"[Continue] Запущен уровень Wish_{wishID}");
        }
        else
        {
            Debug.LogError($"[Continue] Ошибка: Индекс {index} вне диапазона массива уровней!");
        }
    }

    public void OnWishButtonClick(int index)
    {
        foreach (var level in wishLevels) level.SetActive(false);

        if (index < wishLevels.Length)
        {
            wishLevels[index].SetActive(true);
        }
    }
}