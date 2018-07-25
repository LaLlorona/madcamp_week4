using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainColtroller : MonoBehaviour {
    public Animator animator;

    private float h;
    private float v;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        animator.SetFloat("h",h);
        animator.SetFloat("v", v);
		
	}
}
