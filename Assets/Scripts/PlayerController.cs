using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public GameObject game;


    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
        anim = GetComponent<Animator>();// detecta automaticamente el animator, para gestionar las animaciones
    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x)); // valor absolutod de la velocidad del eje x 
        anim.SetBool("Grounded", grounded);
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) // para saltar con la flecha Arriba
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;// Para corregir la frision provista por el materials 2D
        fixedVelocity.x *= 0.75f;
        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }


        float h = Input.GetAxis("Horizontal"); // para detectar la flechas que se aprientan, -1 para <- y 1 para ->

        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        cambiarDireccion(h);

        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // Fisica del SALTO
            jump = false;
        }


        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool ataca = stateInfo.IsName("Player_Attack");
        if (Input.GetMouseButtonDown(0)&& ataca != true) // Detecta el click del boton derecho y efectua un ataque
        {
            //UpdateState("Player_Attack");
            anim.SetTrigger("Atacking");
            
        }
    }


    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            anim.Play(state);
        }
    }
    void cambiarDireccion(float h)
    {
        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Gestiona la direccion de la animacion
            if (rb2d.velocity.x < -0.1)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            if (rb2d.velocity.x > 0.1)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
        }
    }

    public void EstoyAtacando(GameObject ememy)
    {
        // devuelvo un booleano indicando si el player esta en modo Ataque
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool ataca = stateInfo.IsName("Player_Attack");
        if (ataca)
        {
            Debug.Log("Player Esta Atacando");
            ememy.SendMessage("EnemigoMuerto");
        }
        else { Debug.Log("Player NOOO Esta Atacando"); }
        
        //game.SendMessage("IncreasePoint", transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Choco con Un Enemigo");
        }
        if (collision.gameObject.tag == "Coin")
        {
            game.SendMessage("IncreasePoint");
            collision.gameObject.SendMessage("GestionarRecolectarCoin");
            Debug.Log("Player Toco Una Moneda");
        }

    }


    void OnBecameInvisible()
    {
        transform.position = new Vector3(-7f, 0f, 0f);    
    }

}
