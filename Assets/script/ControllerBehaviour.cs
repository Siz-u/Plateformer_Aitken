using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
public class ControllerBehaviour : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float maxspeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ground;
    // Start is called before the first frame update
  
    private Rigidbody2D myRB;
    private Vector2 stickDirection;
    private bool isOnGround = false;

    private void OnEnable()
    {
        var controls = new Controls();
        controls.Enable();
        controls.Main.Move.performed += MoveOnperformed; 
        controls.Main.Move.canceled += MoveOncanceled;
        controls.Main.Jump.performed += JumpOnperformed; 
    }

    private void JumpOnperformed(InputAction.CallbackContext obj)
    {
        if (isOnGround)
        {
        myRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isOnGround= false;
        }
        Debug.Log ("saut");
    }
    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        stickDirection = obj.ReadValue<Vector2>();
        Debug.Log(stickDirection);
    }

    private void MoveOncanceled(InputAction.CallbackContext obj)
    {
        stickDirection = Vector2.zero;
    }

    void Start ()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var direction = new Vector2
        {
            x = stickDirection.x,

            y = 0

        };
        if (myRB.velocity.sqrMagnitude < maxspeed)
        {
            myRB.AddForce(stickDirection * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (ground == (ground | (1 << other.gameObject.layer)))
        {
            isOnGround = true; 
            Debug.Log ("saut 2");
        }
    }
}
