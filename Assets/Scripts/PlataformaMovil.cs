﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour {

    public Transform target;
    public float speed = 1f;

    private Vector3 start;
    private Vector3 end;
	// Use this for initialization
	void Start () {
		if(target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (target != null)
        {
            float fixedSpeedDelta = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, fixedSpeedDelta);
        }

        if (transform.position == target.transform.position)
        {
            target.position = (target.position == start) ? end : start;
        }
    }
}
