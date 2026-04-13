using UnityEngine;

public class BoyJudge : MonoBehaviour
{
    public FountainLevelManager levelManager;
    public string correctObjectTag = "CorrectItem";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelManager.winWindow.activeSelf || levelManager.loseWindow.activeSelf) return;

        if (collision.CompareTag(correctObjectTag))
        {
            levelManager.GameOver(true);
            AudioManager.instance.PlaySFX(AudioManager.instance.launghBoySound);
            collision.gameObject.SetActive(false);
        }
        else
        {
            levelManager.GameOver(false);
            AudioManager.instance.PlaySFX(AudioManager.instance.cryBoySound);
            collision.gameObject.SetActive(false);
        }
    }
}