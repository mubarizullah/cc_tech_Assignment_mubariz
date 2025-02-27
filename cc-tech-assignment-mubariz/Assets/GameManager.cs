using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject gameOverUI;

    [Header("Timer Settings")]
    [SerializeField] private float countdownTime = 60f;
    private bool isTimerActive = false;

    private void Start()
    {
        isTimerActive = true;
        gameOverUI.SetActive(false); 
    }

    private void Update()
    {
        if (isTimerActive && countdownTime > 0)
        {
            countdownTime -= Time.deltaTime; 
            countdownTime = Mathf.Clamp(countdownTime, 0, 60); 
            UpdateTimerUI();
        }

        if (countdownTime <= 0)
        {
            TimerEnd();
        }
    }

    public void StartCountdown()
    {
        if (!isTimerActive)
        {
            isTimerActive = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1f);
        }
        TimerEnd();
    }

    private void UpdateTimerUI()
    {
        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(countdownTime).ToString(); 
        }
    }

    private void TimerEnd()
    {
        isTimerActive = false;
        gameOverUI.SetActive(true); 
        Time.timeScale = 0; 
    }

    public void ResetTimer()
    {
        countdownTime = 60f;
        isTimerActive = false;
        gameOverUI.SetActive(false);
        Time.timeScale = 1; 
    }
}
