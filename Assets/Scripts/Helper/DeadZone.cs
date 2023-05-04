using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public static Action OnPlayerDied;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
           OnPlayerDied?.Invoke();
        }
    }
}
