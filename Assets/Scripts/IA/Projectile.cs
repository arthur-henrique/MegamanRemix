using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 whereToShoot;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        whereToShoot = player.position;//  new Vector2(player.position.x, player.position.y);
        Vector3 fator = player.position - transform.position;
        whereToShoot.x = player.position.x +fator.x*3;
        whereToShoot.y = player.position.y + fator.y*3;
        
        
       
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, whereToShoot, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            DestroyProjectile();
            LevelManager.instance.LowDamage();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
