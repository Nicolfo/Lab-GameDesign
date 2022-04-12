using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [Space]
    [Header("Stats")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Gravity = -9.81f;

    private Animator animator;
    private PlayerControls controls;

    //movement handlers
    private Vector3 Velocity;
    private float xMove;
    private float zMove;
    private float zLook;
    private float xLook;
    private bool jump = false;

    //shooting handlers
    bool isShooting = false;
    RangedWeaponController rangedWeaponController;

    //input handlers
    private Vector3 PlayerMovementInput;
    private Vector2 inputLeft;
    private Vector2 inputRight;

    //miscellanous handler
    private bool hasPressedInteractButton;

    private void MovePlayer()
    {
        //Vector3 moveVector = transform.TransformDirection(PlayerMovementInput);
        if (controller.isGrounded)
        {
            Velocity.y = -1f;
            if (jump)
                Velocity.y = JumpForce;
        }
        else
        {
            Velocity.y -= Gravity * -2f * Time.deltaTime;
        }

        controller.Move(((PlayerMovementInput) * Speed * Time.deltaTime));

        //controller.Move(moveVector*Speed*Time.deltaTime);
        controller.Move(Velocity * Time.deltaTime);
        if (!PlayerMovementInput.Equals(Vector3.zero))
            animator.SetBool("Walk", true);
        else
            animator.SetBool("Walk", false);

    }

    void Rotate()
    {
        
        if (!(xLook == 0 && zLook == 0))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(xLook, 0f, zLook)), 10f * Time.deltaTime);
        else if (!(xMove == 0 && zMove == 0))
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(xMove, 0f, zMove)), 10f * Time.deltaTime);

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rangedWeaponController = GetComponent<RangedWeaponController>();
        controls = new PlayerControls();

        //movement input initialization
        controls.Gameplay.Move.performed += ctx => inputLeft = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => inputLeft = Vector2.zero;
        controls.Gameplay.Look.performed += ctx => inputRight = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => inputRight = Vector2.zero;
        controls.Gameplay.Jump.performed += ctx => jump = true;
        controls.Gameplay.Jump.canceled += ctx => jump = false;

        //shooting input initialization
        controls.Gameplay.Shoot.performed += ctx => isShooting = true;
        controls.Gameplay.Shoot.canceled += ctx => isShooting = false;

        //miscellaneous input inizialization
        //controls.Gameplay.Interact.started += ctx => hasPressedInteractButton = !hasPressedInteractButton;
        controls.Gameplay.Interact.started += ctx => hasPressedInteractButton = true;
        controls.Gameplay.Interact.canceled += ctx => hasPressedInteractButton = false;


    }

    private void OnEnable() => controls.Enable();

    private void OnDisable() => controls.Disable();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        xMove = inputLeft.x;
        zMove = inputLeft.y;
        xLook = inputRight.x;
        zLook = inputRight.y;

        PlayerMovementInput = new Vector3(xMove, 0f, zMove);

        MovePlayer();
        Rotate();
        Shoot();
    }

    private void Shoot()
    {
        if (isShooting) rangedWeaponController.Shoot();
    }

    public bool HasPressedInteractButton() => hasPressedInteractButton;
}
