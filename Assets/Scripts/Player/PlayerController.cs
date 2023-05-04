using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   private PlayerInput _playerInput;

   private Rigidbody _rigidbody;
   private Animator _animator;

   [Header("Movement")]
   [SerializeField] private Transform[] laneTransforms;
   private int _currentLaneIndex;
   private Vector3 _distination;
   [SerializeField] private float moveSpeed = 20;

   [Header("Jump")]
   [SerializeField] private float jumpHeight = 1.5f;
   [SerializeField] private Transform groundCheckPoint;
   [SerializeField] [Range(0, 1)] private float groundCheckRadius = 0.2f;
   [SerializeField] private LayerMask whatIsGround;

   private bool _isPlayerALive;
   

   private void OnEnable()
   {
      if (_playerInput==null)
      {
         _playerInput = new PlayerInput();
      }
      _playerInput.Enable();
   }

   private void OnDisable()
   {
      _playerInput.Disable();
   }

   private void Awake()
   {
      _animator = GetComponent<Animator>();
      _rigidbody = GetComponent<Rigidbody>();
   }

   private void Start()
   {
      _playerInput.GamePlayActions.Move.performed += MovePerformed;
      _playerInput.GamePlayActions.Jump.performed += JumpPerformed;
      
      FindStartPosition();

      _isPlayerALive = true;
   }

   private void JumpPerformed(InputAction.CallbackContext obj)
   {
      if (IsOnGround())
      {
         float jumpUpSpeed = Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude);
         _rigidbody.AddForce(Vector3.up*jumpUpSpeed,ForceMode.VelocityChange);
         AudioManager.Instance.PlayJump();
      }
      
   }

   private void FindStartPosition()
   {
      for (int i = 0; i < laneTransforms.Length; i++)
      {
         if (laneTransforms[i].position==transform.position)
         {
            _currentLaneIndex = i;
            _distination = laneTransforms[i].position;
         }
      }
   }

   private void MovePerformed(InputAction.CallbackContext obj)
   {
      if (IsOnGround())
      {
         float inputValue = obj.ReadValue<float>();
         if (inputValue>0)
         {
            MoveRight();
         }
         else
         {
            MoveLeft();
         }
      }
   }

   private void MoveLeft()
   {
      _currentLaneIndex--;
      if (_currentLaneIndex<=0)
      {
         _currentLaneIndex = 0;
      }
      
      _distination = laneTransforms[_currentLaneIndex].position;
      AudioManager.Instance.PlaySwipe();
   }

   private void MoveRight()
   {
      _currentLaneIndex++;
      if (_currentLaneIndex>=laneTransforms.Length)
      {
         _currentLaneIndex = laneTransforms.Length - 1;
      }
      
      _distination = laneTransforms[_currentLaneIndex].position;
      AudioManager.Instance.PlaySwipe();
   }

   private void Update()
   {
      if (_isPlayerALive)
      {
         float xPos = Mathf.Lerp(transform.position.x, _distination.x, moveSpeed * Time.deltaTime);
         transform.position = Vector3.Lerp(transform.position,
            new Vector3(xPos, transform.position.y, transform.position.z), 10f * Time.deltaTime);
      
         AnimatePlayer();
      }

      _isPlayerALive = GameManager.Instance.IsPlayerAlive();

   }

   private bool IsOnGround()
   {
      return Physics.CheckSphere(groundCheckPoint.position,groundCheckRadius,whatIsGround);
   }

   private void AnimatePlayer()
   {
      _animator.SetBool(TagManager.PLAYER_IS_ON_GROUND_ANIM_PARAMETR,IsOnGround());
      _animator.SetFloat(TagManager.PLAYER_Y_VELOCITY_PARAMETR,_rigidbody.velocity.y);
      _animator.SetBool(TagManager.IS_GAME_STARTED,GameManager.Instance.IsGameStarted());
   }
}
