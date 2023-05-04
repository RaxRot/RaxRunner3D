using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_Text logoGameText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject exitButton;

    [SerializeField] private GameObject howToPlayPanel;

    private void Start()
    {
        howToPlayPanel.SetActive(false);
    }

    public void HowToPlay()
    {
        logoGameText.gameObject.SetActive(false);
        playButton.SetActive(false);
        exitButton.SetActive(false);
        
        howToPlayPanel.SetActive(true);
    }

    public void Back()
    {
        logoGameText.gameObject.SetActive(true);
        playButton.SetActive(true);
        exitButton.SetActive(true);
        
        howToPlayPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameLevel");
    }
}
