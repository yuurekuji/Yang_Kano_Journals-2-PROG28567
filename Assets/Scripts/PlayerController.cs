using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public float moveSpeed;
    private Vector2 moveDir;

    public float jumpForce;
    public bool isGrounded;

    [Header("Jump Settings")]
    public float apexHeight = 3f;      // How high the jump peak is
    public float apexTime = 0.4f;      // Time to reach the peak

    public float gravity;
    private float initialJumpVelocity;
    private float jumpStartY;
    private float jumpTime;
    public bool isJumping;
    public float TerminalSpeed;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;  // Extra time allowed after leaving ground
    public float coyoteTimer;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        gravity = -2f * apexHeight / (apexTime * apexTime);
        initialJumpVelocity = 2f * apexHeight / apexTime;
    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2();

        float moveX = Input.GetAxisRaw("Horizontal");
        playerInput = new Vector2(moveX, 0).normalized;

        MovementUpdate(playerInput);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && coyoteTimer <= coyoteTime)
        {
            StartJump();
        }

        if (isJumping)
        {
            jumpTime += Time.deltaTime;
            ApplyJumpMotion();
        }

    }
    void StartJump()
    {
        isJumping = true;
        jumpTime = 0f;
        jumpStartY = transform.position.y;
    }

    void ApplyJumpMotion()
    {
        float PlayerY = 0.5f * gravity * (jumpTime * jumpTime) + initialJumpVelocity * jumpTime + jumpStartY;

        transform.position = new Vector3(transform.position.x, PlayerY);

        TerminalSpeed = gravity * jumpTime + initialJumpVelocity;

        if (TerminalSpeed < 0 && PlayerY <= jumpStartY)
        {
            isJumping = false;
            transform.position = new Vector3(transform.position.x, jumpStartY);
        }
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        rb2D.linearVelocity = new Vector2(playerInput.x * moveSpeed, playerInput.y * moveSpeed);
        
    }

    public bool IsWalking()
    {
        if (rb2D.linearVelocity.magnitude > 0.01f)
        {
            //Debug.Log("walking");
            return true;
        }
        else
        {
            //Debug.Log("isnotWalking");
            return false;
        }
       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Ground")
        {

            isGrounded = false;
            coyoteTimer = 0;
        }
    }

    public void OnCollisionExit2D(Collision2D collider)
    {

        if (collider.gameObject.tag == "Ground")
        {

            isGrounded = true;
            coyoteTimer = Time.deltaTime;
        }
    }
    public bool IsGrounded()
    {
        if (isGrounded == true)
        {
            Debug.Log("urmom");
            return false;
            
        }
        else
        { 

            return true;
        }
        
    }

    public FacingDirection GetFacingDirection()
    {
        moveDir = rb2D.linearVelocity.normalized;
        if (moveDir.x > 0)
        {
            return FacingDirection.right;
        }
        else
        {
            return FacingDirection.left;
        }
        
    }
}
