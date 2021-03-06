﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour {
    [SyncVar] public string playerUniqueIdentity;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer()
    {
        GetNetidentity();
        SetIdentity();
    }

    // Use this for initialization
    void Awake () {
        myTransform = transform;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(myTransform.name == "" || myTransform.name == "GameObject(Clone)") //change it to the prefab name!
        {
            SetIdentity();
        }
		
	}
    [Client]
    void GetNetidentity()
    {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerIdentity(MakeUniqueIdentity());
    }
    
    void SetIdentity()
    {
        if (!isLocalPlayer)
        {
            myTransform.name = playerUniqueIdentity;
        }
        else
        {
            myTransform.name = MakeUniqueIdentity();
        }
    }
    string MakeUniqueIdentity()
    {
        string uniqueName = "Player" + playerNetID.ToString();
        return uniqueName;
    }
    [Command]
    void CmdTellServerIdentity(string name)
    {
        playerUniqueIdentity = name;
    }

}
