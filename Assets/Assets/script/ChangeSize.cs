using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ChangeSize : NetworkBehaviour {

    


    public float speed;
    Vector3 temp;
    public bool start;
    private float startTime = -15f/120f;
    public int ChangeMusicTiming;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        temp = transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTime;
        Debug.Log(temp.x);

        if ((t > ChangeMusicTiming) &&temp.x>5)
        {
            temp = transform.localScale;
            temp.x += Time.deltaTime * speed;   
            temp.y += Time.deltaTime * speed;
            temp.z += Time.deltaTime * speed;
            

            transform.localScale = temp;
        }


        

                
		
	}
}
