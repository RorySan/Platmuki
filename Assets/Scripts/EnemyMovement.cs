using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //config

    [SerializeField] float moveSpeed = 1f;
   

    // cached elements
    Rigidbody2D myRigidbody;
    PolygonCollider2D myBodyCollider;
    Player player;

    // states
    [SerializeField] bool isMovingRight;
    void Start()
    {       
        myBodyCollider = GetComponent<PolygonCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())           
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }        
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.KillPlayer();
    }



}
