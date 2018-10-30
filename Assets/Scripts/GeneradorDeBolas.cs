using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeBolas : MonoBehaviour {

    public GameObject ballAcid;
    public GameObject coins;
    public GameObject door;
    public float timeOfCreation = 0.75f;

    private bool generatorMultipleEnabled;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (generatorMultipleEnabled) CreateBallOFAcid();

    }

    public void CreateBallOFAcid()
    {
            Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(ballAcid, transformUpdate, Quaternion.identity);
    }

    public void CreateCoin()
    {
        Vector3 transformUpdate = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(coins, transformUpdate, Quaternion.identity);
    }

    public void StartGenerator()
    {
        /// Este metodo propio de Unity realiza una invocacion cada
        /// determinado tiempo, el 0f es para la primera vez que lo invoca
        /// despues el metodo CreateBallOFAcid sera invocado cada vez que pase
        /// los determinados segundos provistos por el timeOfCreation
        InvokeRepeating("CreateBallOFAcid", 0f, timeOfCreation);
    }

    public void StartGeneratorMultipleBalls()
    {
        generatorMultipleEnabled = true;
    }

    public void cancelGeneratorMultipleBalls()
    {
        generatorMultipleEnabled = false;
    }

    public void RafagaDeBolas()
    {
        Invoke("StartGeneratorMultipleBalls",0f);
        Invoke("cancelGeneratorMultipleBalls", 5f);

    }

    public void DropCoins()
    {
        CreateCoin();
    }

    public void OpenDoor()
    {
        door.gameObject.SendMessage("GestionarMovimientoDePuerta");
    }

    public void CloseDoor()
    {
        door.gameObject.SendMessage("CloseOrOpenDoor");
    }

    public void StartShow()
    {
        Invoke("StartGenerator", 7f);
        Invoke("CancelGenerator", 30f);
        Invoke("RafagaDeBolas", 38f);
        //Invoke("DropCoins", 45f);
        Invoke("OpenDoor", 50f);
        Invoke("CloseDoor", 55f);
        BoxCollider2D col = this.gameObject.GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    public void CancelGenerator()
    {   // Cancela el invoke previamente invocado :P
        CancelInvoke("CreateBallOFAcid");      
    }
}
