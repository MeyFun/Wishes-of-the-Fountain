using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class GarbageItem
    {
        public GameObject gameObject;
        public Vector2 startPosition;
    }

    [Header("Settings")]
    public float initialTime = 60f;
    public string gameKey = "Level_Trash_Completed";

    [Header("Garbage List")]
    public List<GarbageItem> garbageItems = new List<GarbageItem>();

    [Header("UI Elements")]
    public TextMeshProUGUI timerText;
    public GameObject winWindow;
    public GameObject loseWindow;

    private float timeLeft;
    private int currentItems;
    private bool gameActive = false;

    [ContextMenu("Запомнить текущие позиции мусора")]
    public void SaveCurrentPositions()
    {
        foreach (var item in garbageItems)
        {
            if (item.gameObject != null)
            {
                item.startPosition = item.gameObject.GetComponent<RectTransform>().anchoredPosition;
            }
        }
        Debug.Log("Позиции мусора сохранены в списке!");
    }

    void OnEnable()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        timeLeft = initialTime;
        currentItems = garbageItems.Count;
        gameActive = true;

        if (winWindow != null) winWindow.SetActive(false);
        if (loseWindow != null) loseWindow.SetActive(false);

        foreach (var item in garbageItems)
        {
            if (item.gameObject != null)
            {
                RectTransform rt = item.gameObject.GetComponent<RectTransform>();
                rt.anchoredPosition = item.startPosition;

                item.gameObject.SetActive(true);

                CanvasGroup cg = item.gameObject.GetComponent<CanvasGroup>();
                if (cg != null)
                {
                    cg.alpha = 1f;
                    cg.blocksRaycasts = true;
                }
            }
        }

        if (timerText != null) timerText.color = Color.white;
    }

    void Update()
    {
        if (!gameActive) return;
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else GameOver(false);
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(timeLeft);
        string language = LocalizationManager.Instance.GetCurrentLanguage();
        if (language == "Russian") timerText.text = $"Осталось: {seconds} сек.";
        else timerText.text = $"Remaining: {seconds} sec.";
        if (timeLeft < 10) timerText.color = Color.red;
    }

    public void ObjectThrownInTrash()
    {
        if (!gameActive) return;
        currentItems--;
        if (currentItems <= 0) GameOver(true);
    }

    void GameOver(bool success)
    {
        gameActive = false;
        if (success)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.winSound);
            winWindow.SetActive(true);
            PlayerPrefs.SetInt(gameKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.loseSound);
            loseWindow.SetActive(true);
        }
    }
}