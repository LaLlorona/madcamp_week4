using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ChangeSize : NetworkBehaviour {

    


    public float speed;
    Vector3 temp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
            temp = transform.localScale;
            temp.x += Time.deltaTime * speed;
            temp.y += Time.deltaTime * speed;
            temp.z += Time.deltaTime * speed;

            transform.localScale = temp;

                
		
	}
}
