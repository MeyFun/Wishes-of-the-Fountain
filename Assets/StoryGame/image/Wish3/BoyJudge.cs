using UnityEngine;

public class BoyJudge : MonoBehaviour
{
    public FountainLevelManager levelManager;
    public string correctObjectTag = "CorrectItem";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, не выключен ли скрипт (признак конца игры)
        if (levelManager.winWindow.activeSelf || levelManager.loseWindow.activeSelf) return;

        if (collision.CompareTag(correctObjectTag))
        {
            levelManager.GameOver(true);
            collision.gameObject.SetActive(false); // Удаляем предмет, как ты просил
        }
        else
        {
            levelManager.GameOver(false);
            collision.gameObject.SetActive(false); // Удаляем предмет при ошибке
        }
    }
}