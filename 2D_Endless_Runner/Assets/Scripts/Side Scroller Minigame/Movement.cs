using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput;
    private InputActionMap playerActions;
    public InputAction jumpAction;
    private Rigidbody2D rb;
    private SideScrollerMinigameManager manager;

    [SerializeField] private float jumpForce;
    private bool isGrounded;

    private void Awake()
    {
        playerActions = playerInput.FindActionMap("Player");
        jumpAction = playerActions.FindAction("Jump");

        rb = GetComponent<Rigidbody2D>();
        manager = FindFirstObjectByType<SideScrollerMinigameManager>();
    }

    private void OnEnable()
    {
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.Disable();
    }

    private void Update()
    {
        if (jumpAction.triggered)
        {
            if(isGrounded)
            {
                Jump();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(!isGrounded)
                isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            manager.gameOver = true;
            Destroy(gameObject);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        isGrounded = false;
    }
}