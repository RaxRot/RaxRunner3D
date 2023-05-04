using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDestriction : MonoBehaviour
{
    [HideInInspector]public static float zPosition;

    private void Start()
    {
        zPosition = transform.position.z;
    }
}
