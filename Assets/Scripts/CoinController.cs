using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {
   // public GameObject game;
    public AudioClip clipCoinRecolected;

    private AudioSource audioCoin;
    private Animator anim;
    // Use this for initialization
    void Start () {
        audioCoin = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Player Toco Una Moneda");
        }

    }

    void GestionarRecolectarCoin()
    {
        audioCoin.clip = clipCoinRecolected;
        audioCoin.Play();
        //game.SendMessage("IncreasePoint");
        anim.Play("Coin_Default");
        Invoke("DestroyCoin", 0.6f);

    }

    void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}
