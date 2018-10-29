using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float radiusVision;
    public GameObject ballAcid;
    public float attackSpeed = 2f ; 

    private GameObject playerFollow;
    private bool attacking;
    private Vector3 initialPosition;
	// Use this for initialization
	void Start () {

        playerFollow = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;


    }
	
	// Update is called once per frame
	void Update () {

        // Por defecto el enemigo siempre se mueve a su posicion inicial 
        Vector3 target = initialPosition;
        float distancia = Vector3.Distance(playerFollow.transform.position, transform.position);
        // si la distancia entre el player y este enemigo es menor al radiusVision, significa
        // que el palyer esta dentro de la vision del enemigo y le tira una bola de acido
        if (distancia < radiusVision)
        {

            // Genero la corutina para realizar un ataque cada determinado tiempo solo si attacking es True
            if(!attacking) StartCoroutine(CreateBallOFAcid(attackSpeed));
        }

	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusVision);
    
    }

    IEnumerator CreateBallOFAcid(float seconds)
    {
        attacking = true;
        if(ballAcid != null)
        {
            Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(ballAcid, transformUpdate, Quaternion.identity);

            yield return new WaitForSeconds(seconds);
        }
        attacking = false;
    }
}
