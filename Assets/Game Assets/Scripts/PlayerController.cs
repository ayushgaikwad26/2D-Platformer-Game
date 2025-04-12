using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCollider; // Reference to your existing BoxCollider2D

    private Vector2 originalColliderSize;
    private float crouchHeight = 0.8f; // You can tweak this value as needed

    private void Awake()
    {
        Debug.Log("Player controller awake");
    }

    private void Start()
    {
        // Store original size of collider for restoring later
        originalColliderSize = boxCollider.size;
    }

    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Set walking/running animation
        animator.SetFloat("Speed", Mathf.Abs(speed));

        // Flip player based on direction
        Vector3 scale = transform.localScale;
        if (speed < 0)
        {
            scale.x = -1 * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        // Crouch logic
        bool Crouch = Input.GetKey(KeyCode.LeftControl);
        animator.SetBool("Crouch", Crouch);

        if (Crouch)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, crouchHeight);
        }
        else
        {
            boxCollider.size = originalColliderSize;
        }

        // Jump logic
        bool Jump = vertical > 0;
        animator.SetBool("Jump", Jump);
    }
}
