using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class Player_Death : NetworkBehaviour {

    private Player_Health healthScript;


	// Use this for initialization
	void Start () {
        healthScript = GetComponent<Player_Health>();
        healthScript.Eventdie += DisablePlayer;

		
	}

    private void OnDisable()
    {
        healthScript.Eventdie -= DisablePlayer; // to stop memory leak
    }

    
    void DisablePlayer()
    {
        //make respawn button 
        
    }
}
