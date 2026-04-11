using UnityEngine;
using System.Collections;

public class FadeInSwitchWish : MonoBehaviour
{
    [Header("Groups")]
    public GameObject wishMenuUI;      // Группа "Экран с желанием"
    public GameObject mainMenuUI;      // Группа "Главное меню"
    public GameObject startAnimNenu;
    public bool isStart;
    public float fadeTime = 1.0f;      // Длительность анимации (1 секунда)

    public void ReturnToMainMenu()
    {
        // Проверка: если корутина уже запущена, не нажимать второй раз
        StopAllCoroutines();
        StartCoroutine(SwitchToMenuRoutine());
    }

    IEnumerator SwitchToMenuRoutine()
    {
        yield return new WaitForSeconds(fadeTime);

        if (wishMenuUI != null) wishMenuUI.SetActive(false);
        if (isStart) if (startAnimNenu != null) startAnimNenu.SetActive(true);
        if (mainMenuUI != null) mainMenuUI.SetActive(true);

        yield return new WaitForSeconds(fadeTime);
    }
}