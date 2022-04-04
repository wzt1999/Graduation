using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    private float speed = 20;
    private float endZ = 113;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.z<endZ)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
	}
}
