using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSize;
    [SerializeField] private float scale;

    private Rigidbody body;
    private Animator animator;
    public bool isGrounded { get; set; }
    public bool isMoving {get; set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        isGrounded=true;
    }

    
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(isGrounded && isMoving){
                    animator.SetBool("isWalkingBackwards", true);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isJumping", false);
                }
                
                body.velocity = new Vector2(direction * speed, body.velocity.y);
            }
        if (Input.GetKey(KeyCode.RightArrow))
            {   
                if(isGrounded && isMoving){
                    animator.SetBool("isWalking", true);
                    animator.SetBool("isWalkingBackwards", false);
                    animator.SetBool("isJumping", false);
                }
                
                body.velocity = new Vector2(direction * speed, body.velocity.y);
            }
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
            {
                Jump();
            }
        
        if (direction != 0)
        {
            isMoving = true;
        }
        else 
        {
            isMoving = false;
            animator.SetBool("isWalkingBackwards", false);
            animator.SetBool("isWalking", false);
        }
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else 
        {
            animator.SetBool("isJumping", true);
        }

        if(Input.GetKeyDown(KeyCode.P)){
            animator.SetTrigger("punch");
        }

        if(Input.GetKeyDown(KeyCode.K)){
            animator.SetTrigger("kick");
        }
        
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSize);
        animator.SetBool("isJumping", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("isWalkingBackwards", false);
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;

    }
     public float getSpeed()
    {
        return speed;
    }

    public float getJumpSize()
    {
        return jumpSize;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void setJumpSize(float newJumpSize)
    {
        jumpSize = newJumpSize;
    }
}
