using UnityEngine;
using TMPro;
using System.Collections;

public class DeliveryLevelManager : MonoBehaviour
{
    [Header("Links")]
    public RectTransform roadParent;
    public RectTransform playerCar;
    public TextMeshProUGUI countdownText;
    public RectTransform finishPoint;
    public GameObject winWindow;
    public GameObject loseWindow;

    [Header("Settings")]
    public float roadSpeed = 400f;
    public string gameKey = "Level_Drive_Completed";

    private bool isPlaying = false;
    private Vector2 roadStartPos;

    void Awake()
    {
        roadStartPos = roadParent.anchoredPosition;
    }

    void OnEnable()
    {
        ResetLevel();
    }

    public void ResetLevel()
    {
        isPlaying = false;

        roadParent.anchoredPosition = roadStartPos;
        playerCar.anchoredPosition = new Vector2(0, playerCar.anchoredPosition.y);

        winWindow.SetActive(false);
        loseWindow.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        countdownText.text = "3"; yield return new WaitForSeconds(1f);
        countdownText.text = "2"; yield return new WaitForSeconds(1f);
        countdownText.text = "1"; yield return new WaitForSeconds(1f);
        countdownText.text = "GO!"; yield return new WaitForSeconds(0.5f);
        countdownText.gameObject.SetActive(false);

        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying) return;

        roadParent.anchoredPosition += Vector2.down * roadSpeed * Time.deltaTime;

        float moveInput = 0;
        if (Input.GetKey(KeyCode.LeftArrow)) moveInput = -1;
        if (Input.GetKey(KeyCode.RightArrow)) moveInput = 1;

        if (moveInput != 0)
        {
            playerCar.GetComponent<CarController>().Move(moveInput);
        }

        if (finishPoint.position.y <= playerCar.position.y)
        {
            GameOver(true);
        }
    }

    public void GameOver(bool success)
    {
        if (!isPlaying) return;

        isPlaying = false;
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