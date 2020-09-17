using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_bounce : MonoBehaviour {
   
    Transform tr;
    Rigidbody rb;
    public bool bounceEnabled;


    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
	}



    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {

            Debug.Log("collided with player");
        }

        if(bounceEnabled && other.gameObject.CompareTag("plane"))
        {
            rb.AddForce(0,1000f,0);
            //tr.position = new Vector3(0f, 50f, 0f);
            Debug.Log("got through plane");
        }
    }

}
