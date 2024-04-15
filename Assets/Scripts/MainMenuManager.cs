using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public AudioSource menuMusic;
    public GameObject commands;
    public GameObject menu;
    private void Start()
    {
        menuMusic.Play();
    }
    public void QuitGame()
    {
        Application.Quit();

    }

    public void Commands()
    {
        commands.SetActive(true);
        menu.SetActive(false);
    }

    public void Back()
    {
        commands.SetActive(false);
        menu.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
