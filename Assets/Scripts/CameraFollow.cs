using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour {
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;
    public float smothTime;
    public RawImage background;
    public float parallaxVelocidad = 0.02f;

    private Vector2 velocity;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {

        float posX = Mathf.SmoothDamp(transform.position.x, // Suaviza la seguimiento de la camera
                                      follow.transform.position.x,
                                      ref velocity.x,
                                      smothTime); //follow.transform.position.y;

        transform.position = new Vector3(Mathf.Clamp(posX, minCamPos.x, maxCamPos.x),
            transform.position.y,
            transform.position.z);

        Parallax(posX);
    }

    void Parallax(float posX)
    {
        float finalSpeed = parallaxVelocidad * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f , 1f, 1f);
    }
}
