using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    // Use this for initialization
    GameObject Car;
	void Start ()
    {
        Car = GameObject.Find("Car");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Car.transform.position.z > this.transform.position.z + 30f)
        {
            Destroy(this.gameObject);
        }
	}
}
