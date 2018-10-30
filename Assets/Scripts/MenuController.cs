using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public GameObject flecha, lista;

    private int indice = 0;

	// Use this for initialization
	void Start () {
        Dibujar();
	}
	
	// Update is called once per frame
	void Update () {
        bool up = Input.GetKeyDown("up");
        bool down = Input.GetKeyDown("down");

        if (up) indice++;
        if (down) indice--;

        if (indice > lista.transform.childCount - 1) indice = 0;
        else if (indice < 0) indice = lista.transform.childCount - 1;

        if (up || down) Dibujar();

        if (Input.GetKeyDown("return")) Accion();
    }

    public void Accion()
    {
        Transform opcion = lista.transform.GetChild(indice);

        if(opcion.gameObject.name == "Salir")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(opcion.gameObject.name);
        }
        
    }

    public void Dibujar()
    {
        Transform opcion = lista.transform.GetChild(indice);
        flecha.transform.position = opcion.position;
    }
}
