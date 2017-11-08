using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccelerometerInput : MonoBehaviour
{
    public GameObject Car;
    public float speed = 0f;
    public string OnRoad;
    // button 
    public bool ACC_onTouch;
    public bool BRAKE_ontouch;
    public bool maxlimit;
    public bool minlimit;

    public Text valueSpeed;
    void Start ()
	{
        Car = this.gameObject;
    }
	
	void Update ()
    {
        valueSpeed.text = ("Speed : " + speed);
        //move forward
        AccInput();
        Car.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    }

    void AccInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.x = dir.x / 2;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        transform.Translate(dir.x, 0, 0);
        speedControl();
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.name == "Road")
        {
            OnRoad = (collision.collider.gameObject.transform.parent.name);
        }
    }

    public void AccOnTouch_true() { ACC_onTouch = true; }
    public void AccOnTouch_false() { ACC_onTouch = false; }
    public void BrakeOnTouch_true() { BRAKE_ontouch = true; }
    public void BrakeOnTouch_false() { BRAKE_ontouch = false; }

    void speedControl()
    {  
        if(ACC_onTouch == true && maxlimit != true)
        {
            speed = speed + 0.2f;       
        }else if(ACC_onTouch == false)
        {
            speed = speed - 0.2f;
        }
        // max limit
        if(speed > 50)
        {
            maxlimit = true;
            speed = 50;
        }else if(speed < 50)
        {
            maxlimit = false;
        }
        // min limit
        if (speed < 0)
        {
            speed = 0;
        }
        if(BRAKE_ontouch == true)
        {
            speed = speed - 0.4f;
            if(speed < 0)
            {
                speed = 0;
            }
        }
    }
}
