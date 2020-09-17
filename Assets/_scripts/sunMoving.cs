using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunMoving : MonoBehaviour {

    Transform sunTr;
    public float speed = 1.0f;

	// Use this for initialization
	void Start () {
        sunTr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {

        float x = speed * Time.time * 100f % 360f;//90f * Mathf.Sin(Time.time * 2 * Mathf.PI) % 180f;
        sunTr.rotation = Quaternion.Euler(x, 0, 0);

    }
}
