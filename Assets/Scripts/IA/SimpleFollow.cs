using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D rdb;
    private SpriteRenderer sr;
    private bool isFlipped;
    // Start is called before the first frame update
    void Start()
    {
        rdb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 dif = target.transform.position - transform.position;
            if (dif.x < 0)
            {
                isFlipped = false;
                sr.flipX = isFlipped;
            }
            else if (dif.x > 0)
            {
                isFlipped = true;
                sr.flipX = isFlipped;
            }
            rdb.AddForce(dif*2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = null;
        }
    }
}
