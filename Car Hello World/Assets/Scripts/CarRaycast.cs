using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaycast : MonoBehaviour {

    public GameObject CarObject;
    RaycastHit hit;

    void Awake()
    {
        CarObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    { 
        
     if(Physics.Raycast(transform.position, -new Vector3(0,0,5), out hit))
        {         
            if (hit.collider.gameObject.name == "Car" && hit.distance < 8f )
            {
                CarObject.GetComponent<AccelerometerInput>().rayCountTrigger += 1;
                this.gameObject.SetActive(false);
                // print(hit.distance);
                print("Hit Palyer");
            }
        }
        Debug.DrawRay(transform.position, -new Vector3(0,0,5), Color.red);
    }    
}
