using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public float speedValue;
    void Start()
    {
        speedValue = 4;
    }

    void Update ()
    {      
        destorySelf();
        speedControl(speedValue);

    }

    void speedControl(float speedInput)
    {
        speedInput = speedValue;
        //transform.Translate(new Vector3(0, 0, 1f * speedInput * Time.deltaTime));
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.forward * speedInput * Time.deltaTime);
    }

    void BehoviorsOfCar() //****
    {


    }

    void destorySelf()
    {
        if (transform.position.y < -19f && transform.position.y > -20f)
        {
            Destroy(this.gameObject);
        }
    }
}
