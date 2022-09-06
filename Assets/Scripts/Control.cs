using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Variaveis
    // Componente que contém a animação
    public Animator anima;
    // Float que regula o movimento em X
    float xmov;
    // Componente que contém o RigidBody
    public Rigidbody2D rdb;
    // Booleans que controlam Jump e DoubleJump
    bool jump,doublejump;
    // Floats que controlam Cooldown the jump / Lado de pulo
    float jumptime, jumptimeside;
    // Componente que contém os efeitos de particula
    public ParticleSystem fire;


    //Funçoes
    void Start()
    {

    }
    void Update()
    {
        // Altera a variavel de acordo com o Axis do Input (Semelhante ao Input Systen)
        xmov = Input.GetAxis("Horizontal");
        // Se o Input do Jump for pressionado
        if (Input.GetButtonDown("Jump"))
        {
            // E se o jumptime for menor do que o valor
            if (jumptime < 0.1f)
            {
                // altera o boolean
                doublejump = true;
            }
        }

        if (Input.GetButton("Jump"))
        {
            jump = true;
        }
        else
        {
            jump = false;
            doublejump = false;
            jumptime = 0;
            jumptimeside = 0;
        }
        anima.SetBool("Fire", false);

        if (Input.GetButtonDown("Fire1"))
        {
            fire.Emit(1);
            anima.SetBool("Fire", true);
        }

    }
   
    void FixedUpdate()
    {
        Reverser();
        anima.SetFloat("Velocity", Mathf.Abs(xmov));
        //rdb.velocity = new Vector2(xmov * 1.3f, rdb.velocity.y);

        rdb.AddForce(new Vector2(xmov * 20/(rdb.velocity.magnitude+1), 0));

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit)
        {
            anima.SetFloat("Height", hit.distance);
            JumpRoutine(hit);
        }

        RaycastHit2D hitright;
        hitright = Physics2D.Raycast(transform.position+
            Vector3.up*0.5f, transform.right,1);

        if (hitright)
        {
            if (hitright.distance < 0.3f)
            {
                JumpRoutineSide(hitright);
            }
            Debug.DrawLine(hitright.point, transform.position 
                + Vector3.up * 0.5f);
        }

        
    }
    /// <summary>
    /// rotina de pulo parte fisica
    /// </summary>
    /// <param name="hit">coloque aqui o raycast hit para altura do chao</param>
    private void JumpRoutine(RaycastHit2D hit)
    {
        if (hit.distance < 0.1f)
        {
            jumptime = 1;
        }
      

            if (jump)
            {
                jumptime = Mathf.Lerp(jumptime, 0, Time.fixedDeltaTime * 10);
                rdb.AddForce(Vector2.up * jumptime, ForceMode2D.Impulse);
            }
        
    }

    private void JumpRoutineSide(RaycastHit2D hitside)
    {
        if (hitside.distance < 0.3f )
        {

            jumptimeside = 1;
           
        }

        if (doublejump)
        {
            PhisicalReverser();
            jumptimeside = Mathf.Lerp(jumptimeside, 0, Time.fixedDeltaTime*10);
            rdb.AddForce((hitside.normal*50 + Vector2.up*80) * jumptimeside);
        }
    }




    /// <summary>
    /// funcao pra inverter o personagem
    /// </summary>
    void Reverser()
    {
        if (xmov > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (xmov < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }
    void PhisicalReverser()
    {
        if (rdb.velocity.x > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (rdb.velocity.x < 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Damage"))
        {
            LevelManager.instance.LowDamage();
        }
    }
}
