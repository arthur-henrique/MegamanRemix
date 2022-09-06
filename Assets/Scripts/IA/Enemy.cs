using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int lives = 5;
    public Animator enemyAnimator;
    // Use this for initialization
    void Start () 
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        lives--;
        if (lives < 1)
        {
            Destroy(gameObject,1f);
        }
    }
}
