using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static int soulsNumber = 0;
    public static int soulsNeeded = 35;
    public static float playerHealth = 100;
    private int maxHealth=100;
    public static float playerMana = 100;
    private int maxMana = 100;
    public float manaRegen = 3.0f;
    public float healthRegen = 0.5f;
    public static bool isGamePaused = false;
    public static bool isGameOver = false;
    private float startTime;
    public static bool isGameEnded = false;
   

    public TextMeshProUGUI soulsText;
    public TextMeshProUGUI timerText;
    public Image healthBar;
    public Image manaBar;
    public Image soulsBar;
    public Image bossTimer;

    public Canvas bossTimerCanvas;
    public TextMeshProUGUI bossHealthText;
    public Canvas bossHealthBar;
    public Image bossHealth;

    public GameObject gameOverUi;
    public GameObject pauseGameUi;
    public GameObject gameUI;
    public GameObject gameWonUi;
    public TextMeshProUGUI timePlayedText;
    public GameObject cameraController; 
    public FirstPersonController firstPersonController;

    

    void Start()
    {
        startTime = Time.time;
        playerHealth = maxHealth;
        playerMana = maxMana;
        soulsText.text = "" + (soulsNumber / soulsNeeded)*100 +"%";
        timerText.text = "" + GameManager.timer;
        timePlayedText.text = "";
        soulsBar.fillAmount = 0;
        bossHealthBar.enabled = false;
        bossHealthText.enabled = false;

        isGameOver = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseGameUi.SetActive(false);
        cameraController.SetActive(true);
        Time.timeScale = 1f;

    }

    void Update()
    {
        if (playerMana < 100)
            playerMana += manaRegen * Time.deltaTime;

        if (playerHealth < 100)
            playerHealth += healthRegen * Time.deltaTime;

        if (isGameEnded)
        {
            Time.timeScale = 0f;
            cameraController.SetActive(false);
            firstPersonController.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gameUI.SetActive(false);
            gameWonUi.SetActive(true);
            float endTime = Time.time;
            float playTime = endTime - startTime;
            int playMinutes = Mathf.FloorToInt(playTime / 60);
            int playSeconds = Mathf.FloorToInt(playTime % 60);
            timePlayedText.text = string.Format("{0:00}:{1:00}", playMinutes, playSeconds);
        }

        if (playerHealth <=0 && !isGameOver)
        {
            isGameOver = true;
            Time.timeScale = 0f;
            gameUI.SetActive(false);
            gameOverUi.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cameraController.SetActive(false);
            firstPersonController.enabled = false;
        }
        
        soulsText.text = (soulsNumber * 100 / soulsNeeded).ToString()+"%";
        timerText.text = string.Format("{0:00}:{1:00}", GameManager.minutes, GameManager.seconds);
        bossTimer.fillAmount = (GameManager.minutes * 60 + GameManager.seconds) / GameManager.bossTimer;
        manaBar.fillAmount = playerMana / maxMana;
        healthBar.fillAmount = playerHealth / maxHealth;
        soulsBar.fillAmount = (float)soulsNumber / soulsNeeded;
        bossHealth.fillAmount = (float)Boss.health/Boss.startingHealth;

        if((GameManager.timer <= 0 || soulsNumber >= soulsNeeded) && bossTimerCanvas.enabled == true)
        {
            bossTimerCanvas.enabled = false;
            timerText.enabled = false;
            bossHealthBar.enabled = true;
            bossHealthText.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isGameEnded && !isGameOver)
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        gameUI.SetActive(false);
        cameraController.SetActive(false);
        firstPersonController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseGameUi.SetActive(true);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        gameUI.SetActive(true);
        Time.timeScale = 1f;
        pauseGameUi.SetActive(false);
        cameraController.SetActive(true); 
        firstPersonController.enabled = true;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void RestartGame()
    {
        gameOverUi.SetActive(false);
        gameUI.SetActive(true);
        SceneManager.LoadScene("MainScene");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
