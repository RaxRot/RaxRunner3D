using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinGeneration : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private Transform pointToSpawnCoin;
    private GameObject _coin;

    private void Start()
    {
        if (Random.Range(0,100)<=50)
        {
           _coin = Instantiate(coin, pointToSpawnCoin.position, Quaternion.identity);
           _coin.transform.SetParent(transform);
        }
    }
}
