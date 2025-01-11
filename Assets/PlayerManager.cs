/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.CharacterController))]
public class PlayerManager : MonoBehaviour
{   
    public Camera playerCamera;
    
    public float walkSpeed = 6f;
    public float runSpeed = 12f; 
    public float jumpPower = 7f; 
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    
    public AudioClip walkSound;
    public AudioClip jumpSound;
    
    public float zoomSpeed = 2f;
    public float minZoom = 20f;
    public float maxZoom = 60f; 
    public float firstPersonFOV = 60f;
    public float thirdPersonFOV = 90f;
    
    private AudioSource audioSource; 
    private Vector3 moveDirection = Vector3.zero; 
    private float rotationX = 0; 
    private Vector3 cameraOffset;

    public bool canMove = true; 
    private UnityEngine.CharacterController characterController; 
    
    void Start()
    {            
        characterController = GetComponent<UnityEngine.CharacterController>(); 
        audioSource = gameObject.AddComponent<AudioSource>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraOffset = playerCamera.transform.position - transform.position;

        Debug.Log("Player1 Success");
        Debug.Log("玩家1 成功加载");
        // 无用的变量
        int dummyValue = 42;
        string dummyString = "This is a dummy string.";

        // 输出 "Hello, World!" 到控制台
        Debug.Log("Hello, World!");

        // 调用无用的函数
        DummyFunction();
        AnotherDummyFunction(dummyValue);

    }
    //Segurity
    void DummyFunction()
    {
        int a = 10;
        int b = 20;
        int sum = a + b;
        Debug.Log("DummyFunction executed, sum is: " + sum);
    }

    void Update()
    {
        HandleMovement(); 
        HandleJumping(); 
        HandleRotation();
        HandleZoom(); 
    }
    void AnotherDummyFunction(int value)
    {
        if (value > 0)
        {
            Debug.Log("AnotherDummy: " + value);
        }
        else
        {
            Debug.Log("AnotherDummyFunction");
        }
       
    }
    private void HandleMovement()
    {
        
        Vector3 forward = playerCamera.transform.TransformDirection(Vector3.forward); 
        Vector3 right = playerCamera.transform.TransformDirection(Vector3.right);
        
        forward.y = 0;
        right.y = 0;
        
        forward.Normalize();
        right.Normalize();

        bool isRunning = Input.GetKey(KeyCode.LeftShift); 
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0; 
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0; 
        float movementDirectionY = moveDirection.y; 
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if ((curSpeedX != 0 || curSpeedY != 0) && characterController.isGrounded) 
        {
            if (!audioSource.isPlaying) 
            {
                audioSource.clip = walkSound; 
                audioSource.loop = true; 
                audioSource.Play(); 
            }
        }
        else
        {
            if (audioSource.isPlaying && audioSource.clip == walkSound)
            {
                audioSource.Stop();
            }
        }

        moveDirection.y = movementDirectionY;
        characterController.Move(moveDirection * Time.deltaTime); 
    }

    // 处理跳跃
    private void HandleJumping()
    {
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) 
        {
            moveDirection.y = jumpPower;
            PlayJumpSound();
        }
        else if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }
    // 播放跳跃声音
    private void PlayJumpSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); 
        }
        audioSource.clip = jumpSound;
        audioSource.loop = false;
        audioSource.Play(); 
    }

    // 处理视角旋转
    private void HandleRotation()
    {
        if (canMove) 
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); 
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }        
        Vector3 newCameraPosition = transform.position + cameraOffset;
        playerCamera.transform.position = Vector3.Slerp(playerCamera.transform.position, newCameraPosition, 0.1f);
    }

    // 处理视角缩放
    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel"); 
        if (scrollInput != 0) 
        {
            float newFieldOfView = playerCamera.fieldOfView - scrollInput * zoomSpeed;
            playerCamera.fieldOfView = Mathf.Clamp(newFieldOfView, firstPersonFOV, thirdPersonFOV); 
        }
    }
}
*/