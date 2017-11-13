using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccelerometerInput : MonoBehaviour
{
    public GameObject Car;
    public float speed = 50f;
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
        Car.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x,GetComponent<Rigidbody>().velocity.y, 1f * speed * Time.deltaTime);
        // Car.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    }

    void AccInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;

        // print("Input acc: " + dir.x);
        if (dir.x > 0.4f && dir.x * -1 < 0)
        {
            dir.x = dir.x / 3f;
            print("fast right");
        }
        else if (dir.x < 0.4f && dir.x * -1 < 0)
        {
            dir.x = dir.x / 5;
            print("normal right");
        }
        if (dir.x < -0.4f && dir.x * -1 > 0)
        {
            dir.x = dir.x / 3f;
            print("fast left");
        }
        else if (dir.x > -0.4f && dir.x * -1 > 0)
        {
            dir.x = dir.x / 5;
            print("normal left");
        }

        /*if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }*/

        if (speed != 0 && speed > 100)
        {
            transform.Translate(dir.x, 0f, 0f);
        }
        speedControl();
    }

    public void AccOnTouch_true() { ACC_onTouch = true; }
    public void AccOnTouch_false() { ACC_onTouch = false; }
    public void BrakeOnTouch_true() { BRAKE_ontouch = true; }
    public void BrakeOnTouch_false() { BRAKE_ontouch = false; }

    void speedControl()
    {  
        if(ACC_onTouch == true && maxlimit != true)
        {
            speed = speed + 15f;       
        }else if(ACC_onTouch == false)
        {
            speed = speed - 5f;
        }
        // max limit
        if(speed > 800)
        {
            maxlimit = true;
            speed = 800;
        }else if(speed < 800)
        {
            maxlimit = false;
        }
        // min limit
        if (speed < 200)
        {
            speed = 200;
        }
        if(BRAKE_ontouch == true)
        {
            speed = speed - 20f;

            if(speed < 200)
            {
                speed = 200;
            }
        }
    }
}
