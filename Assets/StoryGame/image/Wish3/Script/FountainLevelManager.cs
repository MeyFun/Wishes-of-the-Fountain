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
    public List<ItemData> allItems;


    public string gameKey = "Level_Amulet_Completed";
    private bool gameActive = false;

    void Awake()
    {
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

        foreach (var item in allItems)
        {
            if (item.obj != null)
            {
                item.obj.SetActive(true);
                item.obj.GetComponent<RectTransform>().anchoredPosition = item.startPos;

                CanvasGroup cg = item.obj.GetComponent<CanvasGroup>();
                if (cg != null) cg.blocksRaycasts = true;
            }
        }
    }

    public void GameOver(bool success)
    {
        if (!gameActive) return;
        gameActive = false;

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