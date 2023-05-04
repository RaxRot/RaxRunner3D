using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public static Action OnObstacleToughed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            OnObstacleToughed?.Invoke();
            
            Destroy(gameObject);
        }
    }
}
