using UnityEngine;
using System.Collections.Generic;

public class FountainLevelManager : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public GameObject obj;
        [HideInInspector] public Vector2 startPos;
    }

    [Header("UI Elements")]
    public GameObject winWindow;
    public GameObject loseWindow;

    [Header("Objects Setup")]
    public List<ItemData> allItems; // Перетащи сюда все предметы (и нужный, и мусор)


    public string gameKey = "Level_Amulet_Completed";
    private bool gameActive = false;

    void Awake()
    {
        // Запоминаем начальные позиции всех предметов
        foreach (var item in allItems)
        {
            if (item.obj != null)
            {
                item.startPos = item.obj.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

    void OnEnable()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        gameActive = true;
        winWindow.SetActive(false);
        loseWindow.SetActive(false);

        // Возвращаем все предметы на места и включаем их
        foreach (var item in allItems)
        {
            if (item.obj != null)
            {
                item.obj.SetActive(true);
                item.obj.GetComponent<RectTransform>().anchoredPosition = item.startPos;

                // Включаем возможность взаимодействия (Raycast)
                CanvasGroup cg = item.obj.GetComponent<CanvasGroup>();
                if (cg != null) cg.blocksRaycasts = true;
            }
        }
    }

    public void GameOver(bool success)
    {
        if (!gameActive) return; // Чтобы нельзя было проиграть/выиграть дважды за раз
        gameActive = false;

        // БЛОКИРОВКА: Выключаем Raycast у всех предметов, чтобы их нельзя было больше двигать
        foreach (var item in allItems)
        {
            if (item.obj != null)
            {
                CanvasGroup cg = item.obj.GetComponent<CanvasGroup>();
                if (cg != null) cg.blocksRaycasts = false;
            }
        }

        if (success)
        {
            winWindow.SetActive(true);
            PlayerPrefs.SetInt(gameKey, 1);
            PlayerPrefs.Save();
        }
        else
        {
            loseWindow.SetActive(true);
        }
    }
}