using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Play_music : MonoBehaviour {

    private AudioSource[] Firstmusic;
    public int ChangeMusicTiming;
    private bool Is10SecondFromStart = true;
    public TextMeshProUGUI timerText;
    private float startTime;
  
    


	// Use this for initialization
	void Awake () {
        Firstmusic = GetComponents<AudioSource>();
        //Secondmusic = GetComponent<AudioSource>();
        startTime = Time.time;

        

		
	}
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTime;
        string minutes = ((int) t/60).ToString();
        string seconds = (t % 60).ToString("f1");
        timerText.text = minutes + " : " + seconds;


        
        if (Time.time > ChangeMusicTiming && Is10SecondFromStart)
        {
            Is10SecondFromStart = false;
            

            Firstmusic[0].Stop();
            Firstmusic[1].Play();
        }





    }
}
