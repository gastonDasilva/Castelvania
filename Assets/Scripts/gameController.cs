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
        pointText.text =  GetMaxScore().ToString();
        points = GetMaxScore();
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
            SaveScore(points);
    }

    public void RestarGame()
    {
        SceneManager.LoadScene("Nuevo");
    }

    public void MenuGame()
    {
        SceneManager.LoadScene("End");
    }

    public void SaveScore(int currentPoint)
    {// guarda el score que el jugador logro
        PlayerPrefs.SetInt("Max Points", currentPoint);
    }

    public int GetMaxScore()
    { // devuelve el score maximo que el jugador pudo lograr
        return PlayerPrefs.GetInt("Max Points", 0);
    }
}
