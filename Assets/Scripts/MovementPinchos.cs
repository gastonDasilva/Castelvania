using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPinchos : MonoBehaviour {

    public Transform target;
    public float speed = 3f;
    public int turno = 0;

    private bool puedeMover;
    private Vector3 start;
    private Vector3 end;
    
    // Use this for initialization
    void Start () {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.transform.position;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (puedeMover)
        {
            MoverPuerta();
            gestionarVueltaSiDebe();
        }
    }

    public void GestionarMovimientoDePinchos()
    {
        /* Activa el movimiento */
        if (target != null)
        {
            puedeMover = true;

        }

        
    }

    public void gestionarVueltaSiDebe()
    {
        if (transform.position == target.transform.position && turno ==0 )
        {
            target.position = (target.position == start) ? end : start;
            //puedeMover = false;
            turno += 1;
            Invoke("HabilitarMovimiento", 55f);
        }
    }

    public void HabilitarMovimiento()
    {
        turno = 0;
       // GestionarMovimientoDePinchos();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("ActivarMuerte");
        }
    }

    public void MoverPuerta()
    {
        /* Gestiona el movimiento de la puerta */
        float fixedSpeedDelta = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, fixedSpeedDelta);
    }
}
