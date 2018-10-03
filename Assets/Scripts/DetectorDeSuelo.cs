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

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            player.grounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            {
             player.grounded = false;
            }
      

    }

}
