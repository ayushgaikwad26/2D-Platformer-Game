using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCol;
    public float speed;
    public float jump;
    private Rigidbody2D rb2d;

    // Store original collider values for standing
    private Vector2 boxColInitSize = new Vector2(0.6131499f, 2.082082f);
    private Vector2 boxColInitOffset = new Vector2(0.02508068f, 0.9767957f);

    // Crouch values
    private Vector2 crouchSize = new Vector2(0.6131499f, 1.0f);
    private Vector2 crouchOffset = new Vector2(0.02508068f, 0.55f);

    private void Awake()
    {
        Debug.Log("Player controller awake");
        rb2d=gameObject.GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Jump");
        MoveCharacter(horizontal,vertical);
        PlayMovementAnimation(horizontal,vertical);
    
if (vertical > 0)
{
    animator.SetBool("Jump", true);
}
else
{
    animator.SetBool("Jump", false);
}
    }


       private void MoveCharacter(float horizontal,float vertical){
        //move character horizontally
        Vector3 position=transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
        //move character vertically
        if(vertical>0){
           rb2d.AddForce(new Vector2(0f,jump),ForceMode2D.Force);
        }
       } 
       
       private void PlayMovementAnimation(float horizontal,float vertical){
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 scale = transform.localScale;

        if (horizontal < 0)
        {
            scale.x = -1 * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
        HandleCrouch();
       }
    
     
    

    private void HandleCrouch()
    {
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);
        animator.SetBool("Crouch", isCrouching);

        if (isCrouching)
        {
            boxCol.size = crouchSize;
            boxCol.offset = crouchOffset;
        }
        else
        {
            boxCol.size = boxColInitSize;
            boxCol.offset = boxColInitOffset;
        }
    }
}
