using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float maxSpeed = 5f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public GameObject game;
    public AudioClip DieClip;
    public GameObject healhtbar;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool jump;
    private bool movement = true;
    private bool puedeAtacar = true;
    private AudioSource audioPlayer;
    private SpriteRenderer sprt;

    // Use this for initialization
    void Start() {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
        anim = GetComponent<Animator>();// detecta automaticamente el animator, para gestionar las animaciones
        audioPlayer = GetComponent<AudioSource>();
        sprt = GetComponent<SpriteRenderer>();
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
        if (rb2d.bodyType != RigidbodyType2D.Static)
        {
            Vector3 fixedVelocity = rb2d.velocity;// Para corregir la frision provista por el materials 2D
            fixedVelocity.x *= 0.75f;
            if (grounded)
                {
                rb2d.velocity = fixedVelocity;
                }


            float h = Input.GetAxis("Horizontal"); // para detectar la flechas que se aprientan, -1 para <- y 1 para ->
            if (!movement) h = 0; // gestiona que el personaje no pueda moverse si recibe daño

            rb2d.AddForce(Vector2.right * speed * h);

            float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

            this.cambiarDireccion(h);
            this.EfectuarSalto();
            this.EfectuarAtaque();
        }
        
    }

    public void EfectuarSalto()
    {/* Gestiona el salto del player. Cuando se apreta la flecha para arriba y no esta saltando
      * se produce el salto
      */
        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // Fisica del SALTO
            jump = false;
        }
    }

    public void EfectuarAtaque()
    {/* Realiza un Ataque. Este se produce al hacer click y este método se encarga de realizar 
      * todo el proceso 
      */
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool ataca = stateInfo.IsName("Player_Attack");
        if (Input.GetMouseButtonDown(0) && ataca != true && puedeAtacar)
        {// Detecta el click del boton derecho y efectua un ataque si está en condiciones
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

    void ActivarMuerte()
    {/* Gestiona  el proceso que hace el personaje cuando muere 
        */
        audioPlayer.clip = DieClip;
        audioPlayer.Play();
        rb2d.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Die");
        anim.SetTrigger("Default");
        Invoke("ResetScene", 2f);
        puedeAtacar = false;
        DesactivarAllColliders();
    }

    public void DesactivarAllColliders()
    {
        /* Recorre los colliders que posee el enemigo y los desactiva*/
        Collider2D[] colliders = this.gameObject.GetComponentsInChildren<Collider2D>(); // retorna todos los colliders
        for (int i = 0; i < colliders.Length; i++)
        {
            Collider2D col = colliders[i];
            col.enabled = false;
        }
    }

    void ResetScene()
    {
        game.SendMessage("RestarGame");
    }

    void cambiarDireccion(float h)
    {
        /* Se encarga de cambiar la direccion del Player dependiendo de su velocidad, si esta es negativa
         * su direccion cambia a Izquierda, por lo contrario se cambia a Derecha 
         
         */
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
            ememy.SendMessage("EnemigoMuerto");
        }
        else {
            // this.EnemyKnockBack(ememy.gameObject.transform.position.x);
        }
        
        //game.SendMessage("IncreasePoint", transform.position.y);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            healhtbar.SendMessage("RecibirDanho", 2f);
        }
        if (collision.gameObject.tag == "Coin")
        {
            game.SendMessage("IncreasePoint");
            collision.gameObject.SendMessage("GestionarRecolectarCoin");
        }

        if (collision.gameObject.tag == "DoorMovile")
        {
           // collision.gameObject.SendMessage("GestionarMovimientoDePuerta");
           collision.gameObject.GetComponentInParent<MovementDoor>().SendMessage("GestionarMovimientoDePuerta");
        }

        if (collision.gameObject.tag == "PinchosMovile")
        {
            collision.gameObject.GetComponentInParent<MovementPinchos>().SendMessage("GestionarMovimientoDePinchos");
        }

        if (collision.gameObject.tag == "GeneratorBalls")
        {
            collision.gameObject.GetComponentInParent<GeneradorDeBolas>().SendMessage("StartShow");
        }

        if (collision.gameObject.tag == "End")
        {
            game.gameObject.SendMessage("RestarGame");
        }


    }
    public void EnemyKnockBack(float enemyPosx)
    {/* Si el player es tocado por un Enemy este realiza un salto hacia atras y se le 
      * resta parte de la vida  
         */
        jump = true;
        float side = Mathf.Sign(enemyPosx - transform.position.x);
        rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse); // Fisica del SALTO para atras 
        movement = false;
        puedeAtacar = false;
        Invoke("EnableMovement", 1f);
        sprt.color = Color.red;
        //healhtbar.SendMessage("RecibirDanho", 2f);
        RegistrarDanho(2f);

    }

    public void RegistrarDanho(float cantDanho)
    {
        healhtbar.SendMessage("RecibirDanho", cantDanho);
    }

    public void EnableMovement()
    {
        movement = true;
        puedeAtacar = true;
        sprt.color = Color.white;
    }


    void OnBecameInvisible()
    {
        transform.position = new Vector3(-7f, 0f, 0f);    
    }

}
