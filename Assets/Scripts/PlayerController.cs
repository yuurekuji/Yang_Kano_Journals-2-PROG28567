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
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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

        isGrounded = true;


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
    public void OnCollision2DStay(Collider2D collison)
    {
        Debug.Log("sus");
        if (collison.gameObject.tag == "Ground")
        {
            
            isGrounded = false;
        }
    }
    public bool IsGrounded()
    {
        if(isGrounded == true)
        {

            return true;
        }
        else
        {

            return false;
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
