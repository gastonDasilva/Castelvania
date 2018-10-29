using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDeMovimiento : MonoBehaviour {

    public Transform from, to;
    // Use this for initialization

    void OnDrawGizmosSelected()
    {
        if(from != null && to != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, to.position);
            Gizmos.DrawSphere(from.position, 0.15f);
            Gizmos.DrawSphere(to.position, 0.15f);
        }
    }
}
