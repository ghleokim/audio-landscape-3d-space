using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material : MonoBehaviour {

    public float angle = 0.0f;
    public float speed = 1.0f;
    Renderer rend;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {

        float offsetX = speed * Mathf.Cos(angle);
        float offsetY = speed * Mathf.Sin(angle);


        //x = x + speed * cosA
        //y = y + speed * sinA
		
	}
}
