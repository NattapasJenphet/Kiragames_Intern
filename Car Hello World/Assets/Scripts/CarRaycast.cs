using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaycast : MonoBehaviour {

    public GameObject CarObject;
    RaycastHit hit;
    public bool Action;

    void Awake()
    {
        CarObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        Action = false;
    }
    void Update()
    {
        if (Physics.Raycast(transform.position, -new Vector3(0, 0, 30f), out hit))
        {
            if (hit.collider.gameObject.name == "Car" && hit.distance < 30f)
            {
                CarObject.GetComponent<CoreGameController>().rayCountTrigger += 1;
                this.gameObject.SetActive(false);
                // print(hit.distance);                     
                print("Hit Palyer");
            }

            if (hit.collider.gameObject.tag == "Blocker" && hit.distance < 3f)
            {
                GameObject actionBrakeCar;
                actionBrakeCar = hit.collider.gameObject.transform.parent.gameObject;
                GameObject thisCar;
                thisCar = transform.parent.gameObject;
                if (actionBrakeCar.GetComponent<AutoMove>().speedValue > thisCar.GetComponent<AutoMove>().speedValue)
                {
                    actionBrakeCar.GetComponent<AutoMove>().speedValue -= 5f * Time.deltaTime;
                }
            }
        }
        Debug.DrawRay(transform.position, -new Vector3(0, 0, 30f), Color.red);
    }
}
