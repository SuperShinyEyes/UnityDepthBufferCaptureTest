using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuadColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = new Color(0, 255, 0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
