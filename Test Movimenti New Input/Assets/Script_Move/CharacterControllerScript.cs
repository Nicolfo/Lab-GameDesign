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

    //just for testing purposes. This will be deleted
    [SerializeField] Transform rangedWeapon2SetAsChild;

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
        //Debug.Log("x:"+xLook+" y:"+zLook);
        /*if (xLook > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 10f * Time.deltaTime);
        else if (xLook < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 10f * Time.deltaTime);
        if (zLook > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 10f * Time.deltaTime);
        else if (zLook < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 10f * Time.deltaTime);*/
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

        //just a test
        rangedWeapon2SetAsChild.SetParent(transform);
        //rangedWeapon2SetAsChild.Translate(transform.position, Space.World);
    }

    private void OnEnable() => controls.Enable();

    private void OnDisable() => controls.Disable();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("x " + inputLeft.x + " y " + inputLeft.y);
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
}
