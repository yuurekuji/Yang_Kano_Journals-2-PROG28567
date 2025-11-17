using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;
    public float moveSpeed;
    private Vector2 moveDir;
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


    }

    private void MovementUpdate(Vector2 playerInput)
    {
        rb2D.linearVelocity = new Vector2(playerInput.x * moveSpeed, playerInput.y * moveSpeed);
    }

    public bool IsWalking()
    {
        if (rb2D.linearVelocity.magnitude > 0.01f)
        {
            Debug.Log("walking");
            return true;
        }
        else
        {
            Debug.Log("isnotWalking");
            return false;
        }
       
    }
    public bool IsGrounded()
    {
        return true;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
