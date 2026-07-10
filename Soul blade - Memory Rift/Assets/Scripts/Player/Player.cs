using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private int facingDirection = 1;

    [Header("Unity Components References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput player;

    [Header("Inputs")]
    [SerializeField] private Vector2 moveInput;

    private void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        float targetSpeed = moveInput.x * walkSpeed;
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Flip()
    {
        if(moveInput.x > 0.1f)
        {
            facingDirection = 1;
        }
        else if(moveInput.x < -0.1f)
        {
            facingDirection = -1;
        }

        transform.localScale = new Vector3(facingDirection, 1f, 1f);
    }
}