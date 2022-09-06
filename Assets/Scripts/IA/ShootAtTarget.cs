using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTarget : MonoBehaviour
{

    private float timeBtwShots;
    public float startTimeBtwShots;
    public Transform shootFrom;
    public GameObject target;
    public GameObject shots;
    

    // Start is called before the first frame update
    void Start()
    {
     timeBtwShots = startTimeBtwShots;   
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if(timeBtwShots <= 0)
            {
                Instantiate(shots, shootFrom.position, Quaternion.identity);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            target = null;
        }
    }
}
