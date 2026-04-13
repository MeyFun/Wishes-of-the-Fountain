using UnityEngine;
using System.Collections;

public class FadeInSwitchWish : MonoBehaviour
{
    [Header("Groups")]
    public GameObject wishMenuUI;
    public GameObject mainMenuUI;
    public GameObject startAnimNenu;
    public bool isStart;
    public float fadeTime = 1.0f;

    public void ReturnToMainMenu()
    {
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