using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeSuelo : MonoBehaviour {
    private PlayerController player;
    private Rigidbody2D rb2d;

    // Use this for initialization
    void Start () {
        player = GetComponentInParent<PlayerController>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PinchosHorizontal")
        {
            Debug.Log("Player Se Pincho");
            player.SendMessage("ActivarMuerte");
        }
    }
        
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.grounded = true;
        }
        if (collision.gameObject.tag == "Plataform")
        {
            player.transform.parent = collision.transform;
            player.grounded = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataform")
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
            player.transform.parent = collision.transform;
            player.grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            {
             player.grounded = false;
            }

        if (collision.gameObject.tag == "Plataform")
        {
            player.transform.parent = null;
            player.grounded = false;
        }

    }

}
