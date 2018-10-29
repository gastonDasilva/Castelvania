using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour {

    public Text pointText;
    private AudioSource musicPlayer;

    private int points = 0;
    // Use this for initialization
    void Start () {
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.Play();
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

    public void RestarGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
