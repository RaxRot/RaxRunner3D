using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private void Update()
    {
        ControlObjectMovement();
    }

    private void ControlObjectMovement()
    {
        
        transform.position -= new Vector3(0f, 0f, GameManager.Instance.GetWorldSpeed() * Time.deltaTime);
        
        
        if (transform.position.z<PathDestriction.zPosition)
        {
            Destroy(gameObject);
        }
    }
}
