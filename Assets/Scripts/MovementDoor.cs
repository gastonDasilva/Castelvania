using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDoor : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public int turno = 0;

    private bool cerrar;
    private Vector3 start;
    private Vector3 end;
    

    // Use this for initialization
    void Start()
    {
        if (target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cerrar)
        {
            MoverPuerta();
            gestionarVueltaSiDebe();
        }
    }

    public void GestionarMovimientoDePuerta()
    {
        /* Activa el movimiento */
        if(target != null)
        {
            cerrar = true;
            
        }

        if (transform.position == target.transform.position && turno ==0)
        {
            //cerrar = false;
            target.position = (target.position == start) ? end : start;
            //puedeMover = false;
            turno += 1;
            //Invoke("HabilitarMovimiento", 60f);
        }
    }

    public void CloseOrOpenDoor()
    {
        turno = 0;
    }

    public void gestionarVueltaSiDebe()
    {
        if (transform.position == target.transform.position && turno == 0)
        {
            target.position = (target.position == start) ? end : start;
            //puedeMover = false;
            turno += 1;
            Invoke("HabilitarMovimiento", 60f);
        }
    }

    public void MoverPuerta()
    {
        /* Gestiona el movimiento de la puerta */
        float fixedSpeedDelta = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, fixedSpeedDelta);
    }
}
