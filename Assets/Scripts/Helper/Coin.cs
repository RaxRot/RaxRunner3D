using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinImpactFx;
    
    public static Action OnCoinCollected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
           OnCoinCollected?.Invoke();

           AudioManager.Instance.CollectCoin();
           Instantiate(coinImpactFx, transform.position, Quaternion.identity);
           
           Destroy(gameObject);
        }
    }
}
