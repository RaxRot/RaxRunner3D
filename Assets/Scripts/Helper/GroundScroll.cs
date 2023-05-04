using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    private Material _material;
    [SerializeField] private float scrollSpeed = 0.15f;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Vector2 offset = new Vector2(scrollSpeed * Time.deltaTime, 0f);
        _material.mainTextureOffset += offset;
    }
}
