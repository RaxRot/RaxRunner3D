using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerraFollow : MonoBehaviour
{
   private Transform _target;

   [SerializeField] private Vector3 offset =new Vector3(0,2,-3);
   [SerializeField] private float rotationX = 20;
   
   [SerializeField] private float smoothTime = 0.3f;
   private Vector3 velocity = Vector3.zero;

   private void Start()
   {
      _target = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
   }

   private void LateUpdate()
   {
      Vector3 targetPosition = _target.position + offset;
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
      transform.rotation=Quaternion.Euler(rotationX,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z);
   }
}
