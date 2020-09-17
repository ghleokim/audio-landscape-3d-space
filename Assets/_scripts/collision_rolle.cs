using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_rolle : MonoBehaviour {
   
    Transform tr;
    Rigidbody rb;
    public bool rollEnabled;
    private bool moveEnabled;
    private float moveAmount;

    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!rollEnabled && Input.GetKeyUp(KeyCode.Space))
        {
            this.transform.parent = null;
            moveEnabled = true;
        }
        if(moveEnabled)
        {
            moveAmount = Random.Range(5.0f, 10.0f);
            this.transform.position = new Vector3(this.transform.position.x,moveAmount,this.transform.position.z);
            moveEnabled = false;
        }
    }



    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            this.transform.SetParent(other.gameObject.transform);
            rb.isKinematic = true;
            rollEnabled = false;
            Debug.Log("collided with player, attatched");
        }
        if (other.gameObject.CompareTag("audioSource"))
        {
            this.transform.SetParent(other.gameObject.transform);
            rb.isKinematic = true;
            rollEnabled = false;
            Debug.Log("collided with other ball, attatched");
        }

        /*if(rollEnabled && other.gameObject.CompareTag("plane"))
        {
            rb.AddForce(50f,-10f,50f);
            //tr.position = new Vector3(0f, 50f, 0f);
            Debug.Log("got through plane");
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        if (rollEnabled && other.gameObject.CompareTag("plane"))
        {
            rb.AddTorque(5f, -1f, 5f);
            //tr.position = new Vector3(0f, 50f, 0f);
            Debug.Log("got through plane");
        }
    }

}
