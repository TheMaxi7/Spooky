using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class UiEvents : MonoBehaviour
{
    public GameObject cameraController;
    public FirstPersonController firstPersonController;
    public void ResumeGame()
    {
        UiManager.isGamePaused = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        UiManager.isGamePaused = false;
        UiManager.isGameOver = false;
        SceneManager.LoadScene("RestartScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
