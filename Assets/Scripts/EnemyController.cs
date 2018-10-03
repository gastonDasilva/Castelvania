using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 1f;
    public float speed = 1f;
    public GameObject CoinPreFab;
    public AudioClip clipEnemyDie;

    private Rigidbody2D rb2d;
    private Animator anim;
    private AudioSource audioEnemy;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); // detecta automaticamente el rigidbody
        anim = GetComponent<Animator>();// detecta automaticamente el animator, para gestionar las animaciones
        audioEnemy = GetComponent<AudioSource>();//Detecta el audioSource
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.AddForce(Vector2.right * speed);
    }

    void FixedUpdate()
    {
        rb2d.AddForce(Vector2.right * speed);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f) // si la velocidad es o Entra al if
        {                                                           // Es decir si se choco con algu objeto
            speed = -speed;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            CambiarDireccionDependiendo(speed);
        }
    }

    void CambiarDireccionDependiendo(float s)
    {
        if (s > 0.1f && rb2d.bodyType != RigidbodyType2D.Static)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Gestiona la direccion de la animacion
        }

        if (s < -0.1f && rb2d.bodyType != RigidbodyType2D.Static)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Choco con el player");
            collision.gameObject.SendMessage("EstoyAtacando", this.gameObject);
        }
        
    }

    void EnemigoMuerto()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        rb2d.bodyType = RigidbodyType2D.Static;
        if (!(stateInfo.IsName("Enemy_Die")))
        {
            GeneratorCoins();
            audioEnemy.clip = clipEnemyDie;
            audioEnemy.Play();
        }
        anim.SetTrigger("Died");
        //audioEnemy.clip = clipEnemyDie;
        //audioEnemy.Play();
        Invoke("DestroyEnemy", 1f);
    }

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    void CreateCoin()
    {// Objeto a Instanciar, posicion actual del gameObject, variable necesaria para el instantiate

        Vector3 transformUpdate = new Vector3 (transform.position.x,transform.position.y , transform.position.z);
        Instantiate(CoinPreFab, transformUpdate, Quaternion.identity);
    }

    public void GeneratorCoins()
    {
        /// Este metodo propio de Unity realiza una invocacion cada
        /// determinado tiempo, el 0f es para la primera vez que lo invoca
        Invoke("CreateCoin", 0f);
    }
}
