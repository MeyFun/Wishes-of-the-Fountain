using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WishSlot : MonoBehaviour
{
    [Header("Настройки ID")]
    public string wishID;
    public int wishIndex;

    [Header("Ссылки на объекты")]
    public GameObject coinImage;
    public GameObject descriptionText;
    public Button playButton;

    private WishButtonHandler wishHandler;

    void Awake()
    {
        wishHandler = Object.FindFirstObjectByType<WishButtonHandler>();
    }

    void OnEnable()
    {
        RefreshSlot();
    }

    public void RefreshSlot()
    {
        bool isCompleted = PlayerPrefs.GetInt(wishID, 0) == 1;

        if (coinImage != null) coinImage.SetActive(isCompleted);
        if (descriptionText != null) descriptionText.SetActive(isCompleted);

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
            wishHandler.selectWishesUI.SetActive(false);
            wishHandler.wishesParent.SetActive(true);

            wishHandler.OnWishButtonClick(wishIndex);
        }
    }
}