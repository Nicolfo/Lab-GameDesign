using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tryMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float Speed;
    private Animator animator;
    void Start()
    {

    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMovementInput.Normalize();
        transform.Translate(PlayerMovementInput * Speed * Time.deltaTime, Space.World);
        if (PlayerMovementInput != Vector3.zero){
            transform.forward = PlayerMovementInput;
            animator.SetBool("Walk", true);

        }
        else
            animator.SetBool("Walk", false);
    }
}
