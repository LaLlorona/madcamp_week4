using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterControll : NetworkBehaviour {

    public float speed = 5.0f;
    // Use this for initialization

    public GameObject playerSphere;
    private bool triggeringAnotherPlayer;
    private GameObject otherPlayer;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isLocalPlayer == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(0, 0, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(0, 0, -speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(speed * Time.deltaTime, 0,0);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(-speed * Time.deltaTime, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                //Instantiate(playerSphere.transform, transform.position, Quaternion.identity);
                //if we use just instantiate, server could not notice the item
                CmdSpawn();
            }

            if(triggeringAnotherPlayer && Input.GetKeyDown(KeyCode.E))
            {
                CmdKill(otherPlayer);
            }
            

        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide with portal");
    
        if (other.tag == "Player")
        {
            triggeringAnotherPlayer = true;
            
            otherPlayer = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringAnotherPlayer = false;
            otherPlayer = null;

        }
    }
    [Command]

    public void CmdKill(GameObject Other_player)
    {
        otherPlayer.GetComponent<Player_Health>().DeductHealth(20); //부딫힌 플레이어의 
        /*
        if (otherPlayer.GetComponent<Player_Health>().GetHealth() < 1)

        {
            NetworkServer.Destroy(Other_player);

        }
        */
        
    }
    

    [Command]
    public void CmdSpawn()
    {
        GameObject sphere = (GameObject)Instantiate(playerSphere, transform.position, Quaternion.identity);
        NetworkServer.SpawnWithClientAuthority(sphere, connectionToClient);
    }
}
