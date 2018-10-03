using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour {

    public Text pointText;

    private int points = 0;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreasePoint()
    {
            points += 1;
            pointText.text = points.ToString();
            //SaveScore(points);
    }
}
