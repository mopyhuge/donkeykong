using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float climbSpeed;
    public float moveSpeed;
    public Rigidbody2D rig;
    public float limit;
    private Vector3 startPos;
    public bool grounded;
    private float yInput;
    public float yForce;
    public Vector3 startVel;
    public Quaternion startRot;
    public bool onLadder;
    private GameObject curPlat;
    public Collider2D playerCollider;
   
    
   


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        grounded = true;
        startVel = Vector3.zero;
        startRot = transform.rotation;
        onLadder = false;
    }

    private void FixedUpdate()
    {
        yInput = Input.GetAxis("Jump");
        if (isGrounded() && yInput > 0)
        {
            rig.AddForce(Vector2.up * yInput * yForce, ForceMode2D.Impulse);
            
        }
        if (onLadder && Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            rig.velocity = new Vector2(Input.GetAxis("Jump") * moveSpeed, rig.velocity.y);
            onLadder = true;
            rig.gravityScale = 0;
        }
        if (onLadder && Input.GetAxis("Vertical") != 0 )
        {
            rig.velocity = new Vector2(0, Input.GetAxis("Vertical") * climbSpeed);
            grounded = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rig.velocity.y);
        if (transform.position.x > limit)
        {
            transform.position = new Vector3(limit, transform.position.y, transform.position.x);
        }
        if (transform.position.x < -limit)
        {
            transform.position = new Vector3(-limit, transform.position.y, transform.position.x);
        }
    }

    public bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x,transform.position.y - .2f,transform.position.z),Vector3.down,.01f);
        if (hit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
        if (collision.gameObject.CompareTag("Barrel"))
        {
            resetPlayer();
        }
        if (collision.gameObject.CompareTag("platform"))
        {
            curPlat = collision.gameObject;
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            curPlat = null;
        }
    }

    private IEnumerator disableCollision()
    {
        Collider2D platCollider = curPlat.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(playerCollider, platCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platCollider, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ladder")
        {
            onLadder = true;
            rig.gravityScale = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ladder")
        {
            onLadder = false;
            rig.gravityScale = 1;
        }
            
    }

    public void resetPlayer()
    {
        rig.isKinematic = true;
        grounded = true;
        transform.position = startPos;
        transform.rotation = startRot;
        rig.velocity = startVel;
        rig.isKinematic = false;
        onLadder = false;
    }
}
