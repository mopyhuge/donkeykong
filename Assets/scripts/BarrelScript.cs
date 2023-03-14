using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    private Rigidbody2D rig;
    public float moveSpeed;
    public float boundries;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        boundries = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "boundries")
        {


            //if we hit the top bound
            if (collision.transform.position.x > transform.position.x && rig.velocity.x > 0)
            {
                rig.velocity = new Vector2(
                    rig.velocity.x * -1,
                    rig.velocity.y
                    );
            }
            //if we hit the bottom bound
            if (collision.transform.position.x < transform.position.x && rig.velocity.x < 0)
            {
                rig.velocity = new Vector2(
                    rig.velocity.x * -1,
                    rig.velocity.y
                    );
            }
        }
    }
    
}
