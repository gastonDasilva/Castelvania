using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAcid : MonoBehaviour {


    public float speed;

    private GameObject player;
    private Rigidbody2D r2bd;
    private Vector3 target, dir; 

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        r2bd =GetComponent<Rigidbody2D>();

        if(player != null)
        {
            target = player.transform.position;
            dir = (target - transform.position).normalized;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(target!= Vector3.zero)
        {
            r2bd.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.SendMessage("RegistrarDanho", 8f);
            Destroy(this.gameObject);    
        }

        if (collision.gameObject.tag == "PlayerAttack")
        {
            collision.gameObject.GetComponentInParent<PlayerController>().SendMessage("EstoyAtacando", this.gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void EnemigoMuerto()
    {
        Destroy(this.gameObject);
    }
}
