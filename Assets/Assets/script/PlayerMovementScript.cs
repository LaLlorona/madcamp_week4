using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementScript : NetworkBehaviour {
    
    public float moveSpeed;

    [SyncVar] public bool isAlive = true;

    private Vector3 moveDirection;
    public GameObject PlayerCamera;
    GameObject other;

    private void Start()
    {
        if (isLocalPlayer)
        {
            PlayerCamera.SetActive(true);
        }
        if (!isLocalPlayer)
        {
            PlayerCamera.SetActive(false);
        }
    }

    


    void Update()
    {
        if (isLocalPlayer )
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        }
        
    }

    void FixedUpdate()
    {
        if (isLocalPlayer )
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);

        }
        
    }
}
