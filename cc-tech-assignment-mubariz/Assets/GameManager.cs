using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int startTime = 60; 
    private int currentTime;
    private Coroutine timerCoroutine; 

    [SerializeField ]Text timerText; 
    [SerializeField] GameObject gameOverPanel; 
    [SerializeField] EnemyBehaviour enemyBehaviour;

    private void Start()
    {
        RestartGame(); 
    }

    private IEnumerator TimerRoutine()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateTimerUI();
        }

        GameOver();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = currentTime.ToString(); 
        }
    }

    private void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }


    public void Pause()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    // Public function to restart the timer
    public void RestartGame()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }

        currentTime = startTime;
        UpdateTimerUI(); 
        gameOverPanel?.SetActive(false);
        enemyBehaviour.StopChasing();
        timerCoroutine = StartCoroutine(TimerRoutine());
        Time.timeScale = 1f;
    }
}
