using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private float normalWorldSpeed = 4f;
    [SerializeField] private float minWorldSpeed = 2;
    private float _currentWorldSpeed;
    [SerializeField] private float timeBetweenChangeWorldSpeed = 3f;

    private int _playerScore;
    private int _highScore;
    private int _playerCurrentHealth;
    [SerializeField] private float playerHealth = 3;

    private bool _isGameStarted;
    private bool _isPlayerAlive;

    private void OnEnable()
    {
        CollisionDetection.OnObstacleToughed += DamagePlayer;
        Coin.OnCoinCollected += CollectCoin;
        DeadZone.OnPlayerDied += PlayerDie;
    }

    private void OnDisable()
    {
        CollisionDetection.OnObstacleToughed -= DamagePlayer;
        Coin.OnCoinCollected -= CollectCoin;
        DeadZone.OnPlayerDied -= PlayerDie;
    }

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        
        _playerCurrentHealth = 3;
        _currentWorldSpeed = normalWorldSpeed;
        _isPlayerAlive = true;
    }

    private void Start()
    {
        UIManager.Instance.SetHealth(_playerCurrentHealth/playerHealth);
        UIManager.Instance.SetScore(_playerScore);
    }

    private void Update()
    {
        if (!_isGameStarted)
        {
            _currentWorldSpeed = 0;
        }
        
        if (Keyboard.current.anyKey.wasPressedThisFrame &&!_isGameStarted)
        {
            _isGameStarted = true;
            _currentWorldSpeed = normalWorldSpeed;
        }
    }

    public float GetWorldSpeed()
    {
        return _currentWorldSpeed;
    }

    private void DamagePlayer()
    {
        _playerCurrentHealth--;
        if (_playerCurrentHealth<=0)
        {
            _playerCurrentHealth = 0;
            
            PlayerDie();
        }
        else
        {
            ChangeWorldSpeed();
            UIManager.Instance.InjurePlayer();
            AudioManager.Instance.PlayInjure();
        }
        
        UIManager.Instance.SetHealth(_playerCurrentHealth/playerHealth);
        
    }

    private void CollectCoin()
    {
        _playerScore++;
        UIManager.Instance.SetScore(_playerScore);
    }

    private void ChangeWorldSpeed()
    {
        StartCoroutine(_ChangeWorldSpeedCo());
    }

    private IEnumerator _ChangeWorldSpeedCo()
    {
        _currentWorldSpeed = minWorldSpeed;
        yield return new WaitForSeconds(timeBetweenChangeWorldSpeed);
        _currentWorldSpeed = normalWorldSpeed;
    }

    private void PlayerDie()
    {
        _playerCurrentHealth = 0;
        UIManager.Instance.SetHealth(_playerCurrentHealth/playerHealth);
        UIManager.Instance.ShowGameOver();
        _currentWorldSpeed = 0;
        
        AudioManager.Instance.PlayGameOver();
        _isPlayerAlive = false;

        if (PlayerPrefs.HasKey(TagManager.SCORE))
        {
            if (_playerScore>PlayerPrefs.GetInt(TagManager.SCORE))
            {
                _highScore = _playerScore;
                PlayerPrefs.SetInt(TagManager.SCORE,_highScore);
            }
            else
            {
                _highScore = PlayerPrefs.GetInt(TagManager.SCORE);
            }
           
        }
        else
        {
            _highScore = _playerScore;
            PlayerPrefs.SetInt(TagManager.SCORE,_highScore);
        }
        
        UIManager.Instance.ShowFinalScore(_playerScore);
        UIManager.Instance.ShowHighScore(_highScore);
        
    }

    public bool IsGameStarted()
    {
        return _isGameStarted;
    }

    public bool IsPlayerAlive()
    {
        return _isPlayerAlive;
    }
}
