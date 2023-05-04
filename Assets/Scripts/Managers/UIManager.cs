using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance;
   
   [SerializeField] private TMP_Text scoreText;
   private int _score;

   [SerializeField] private Image healthImage;

   [SerializeField] private GameObject injurePanel;
   [SerializeField] private GameObject pausePanel;
   private bool _isPaused;
   private Keyboard keyboard;

   [SerializeField] private GameObject gameOverPanel;
   [SerializeField] private TMP_Text finalScore;
   [SerializeField] private TMP_Text finalHighScore;

   [SerializeField] private TMP_Text startText;

   private void Awake()
   {
      if (Instance==null)
      {
         Instance = this;
      }
   }

   private void Start()
   {
      healthImage.gameObject.SetActive(true);
      injurePanel.SetActive(false);
      pausePanel.SetActive(_isPaused);
      keyboard = Keyboard.current;
      
      gameOverPanel.SetActive(false);
   }

   private void Update()
   {
      if (keyboard.escapeKey.wasPressedThisFrame)
      {
         _isPaused = !_isPaused;
         pausePanel.SetActive(_isPaused);
         if (_isPaused)
         {
            Time.timeScale = 0f;
         }
         else
         {
            Time.timeScale = 1f;
         }
      }
      
      startText.gameObject.SetActive(!GameManager.Instance.IsGameStarted());
   }

   public void SetScore(int score)
   {
      scoreText.text = "Score: " + score;
   }

   public void SetHealth(float health)
   {
      print(health);
      healthImage.fillAmount = health;
   }

   public void InjurePlayer()
   {
      StartCoroutine(_InjurePlayerCo());
   }

   private IEnumerator _InjurePlayerCo()
   {
      bool isInjure =true;
      
      for (int i = 0; i < 10; i++)
      {
         injurePanel.SetActive(isInjure);
         yield return new WaitForSeconds(0.2f);
         isInjure = !isInjure;
      }

      isInjure = false;
      injurePanel.SetActive(isInjure);
   }

   public void ResumeGame()
   {
      Time.timeScale = 1f;
      pausePanel.SetActive(false);
   }

   public void MainMenu()
   {
      Time.timeScale = 1f;
      pausePanel.SetActive(false);
      //
   }

   public void RestartGame()
   {
      Time.timeScale = 1f;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   public void ShowGameOver()
   {
      gameOverPanel.SetActive(true);
   }

   public void ShowFinalScore(int score)
   {
      finalScore.text = "Score:" + score;
   }

   public void ShowHighScore(int score)
   {
      finalHighScore.text = "High Score:" + score;
   }

}
