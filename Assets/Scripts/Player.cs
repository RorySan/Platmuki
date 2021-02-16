using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{

    // config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 8f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float gravity = -100f;
    

    // state
    bool isAlive = true;
    [SerializeField] bool isJumping = false;


    // cached component reference    
    Animator myAnimator;
    Rigidbody2D myRigidbody;
    PolygonCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float playerGravity;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<PolygonCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Run();
            ClimbLadder();
            Jump();
            DieOnHazards();
        }
        
    }

    private void Run()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;
        var newXPos = transform.position.x + deltaX;
                
        transform.position = new Vector2(newXPos, transform.position.y);
        
        FaceRunDirection(deltaX);
        HandleRunAnimation(deltaX);

    }

    private void Jump()
    {       
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            myRigidbody.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void DieOnHazards()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            KillPlayer();
        }
    }
    private void ClimbLadder()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climb", false);
            myRigidbody.gravityScale = playerGravity;
            myAnimator.speed = 1;
            return; 
        }
       
        myRigidbody.gravityScale = 0f;
        var deltaY = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, deltaY * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        
        HandleClimbAnimation();      

    }

   

    private void HandleRunAnimation(float deltaX)
    {
        bool isPlayerRunning = deltaX != 0;
        myAnimator.SetBool("Run", isPlayerRunning);        
    }

    private void HandleClimbAnimation()
    {        
        bool playerIsClimbing = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climb", true);
        if (!playerIsClimbing)
        {
             myAnimator.speed = 0;
        }
        else
        {
            myAnimator.speed = 1;
        }

    }

    private void FaceRunDirection(float deltaX)
    {
        if (deltaX < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            //transform.localScale = new Vector2(-1f, 1f);
        }
        else if (deltaX > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            //transform.localScale = new Vector3(1f, 1f);
        }
    }

    public void KillPlayer()
    {
        isAlive = false;        
        myAnimator.SetTrigger("Die");
        myRigidbody.AddForce(new Vector2(10f, 25f), ForceMode2D.Impulse);
        FindObjectOfType<GameSession>().PlayerDeath();
    }
}
