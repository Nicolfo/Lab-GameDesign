using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Gravity = -9.81f;
    private Animator animator;
  
    private Vector3 Velocity;
    private float xMove;
    private float zMove;
    private float zLook;
    private float xLook;
   
     private Vector3 PlayerMovementInput;
 
    private void movePlayer(){
        //Vector3 moveVector = transform.TransformDirection(PlayerMovementInput);
        if(controller.isGrounded){
            Velocity.y=-1f;
            if(Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown("joystick button 0") )
                Velocity.y=JumpForce;
        }
        else{
             Velocity.y -= Gravity*-2f*Time.deltaTime;
        }
       
        controller.Move(((PlayerMovementInput) * Speed * Time.deltaTime));
        
        //controller.Move(moveVector*Speed*Time.deltaTime);
        controller.Move(Velocity*Time.deltaTime);
        if(! PlayerMovementInput.Equals(Vector3.zero))
        animator.SetBool("Walk",true);
        else
        animator.SetBool("Walk",false);

    }
   
   void rotate()
    {
    Debug.Log("x:"+xLook+" y:"+zLook);
     /*if (xLook > 0)
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 10f * Time.deltaTime);
     else if (xLook < 0)
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 10f * Time.deltaTime);
     if (zLook > 0)
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 10f * Time.deltaTime);
     else if (zLook < 0)
         transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 10f * Time.deltaTime);*/
    if(!(xLook==0 && zLook==0))
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(xLook,0f,zLook)), 10f * Time.deltaTime);
    else if(!(xMove==0 && zMove==0))
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(xMove,0f,zMove)), 10f * Time.deltaTime);
    
    }
    private void Awake(){
        animator=GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xMove=Input.GetAxis("Horizontal");
        zMove=Input.GetAxis("Vertical");
         zLook=Input.GetAxis("JoyY");
        xLook=Input.GetAxis("JoyX");
        PlayerMovementInput=new Vector3(xMove,0f,zMove);
        
        movePlayer();
        rotate();
    }
}
