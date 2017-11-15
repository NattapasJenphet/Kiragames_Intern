using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccelerometerInput : MonoBehaviour
{
    public GameObject Car;
    public float speed;
    // button 
    public bool ACC_onTouch;
    public bool BRAKE_ontouch;
    public bool maxlimit;
    public bool minlimit;
    // debug text
    public Text valueSpeed;
    public Text dirdebug;

    void Start ()
	{
        Car = this.gameObject;
    }
	
	void Update ()
    {
        valueSpeed.text = ("Speed : " + speed);
        // move forward
        AccInput();
        Car.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x,GetComponent<Rigidbody>().velocity.y, 1f * speed * Time.deltaTime);
        // Car.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    }

    void AccInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        // Debug text
        dirdebug.text = ("acc " + dir.x);

        /*if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }*/

        // keyboard control editor
        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -0.2f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir.x = 0.2f;
        }

        // transform.Translate(dir.x, 0f, 0f);   
        // Car.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 1f * speed * Time.deltaTime);
        Car.GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, speed);
        Car.GetComponent<Rigidbody>().AddRelativeForce(dir.x * 10f, 0f, 0f);
        speedControl();
   
    }

    public void AccOnTouch_true() { ACC_onTouch = true; }
    public void AccOnTouch_false() { ACC_onTouch = false; }
    public void BrakeOnTouch_true() { BRAKE_ontouch = true; }
    public void BrakeOnTouch_false() { BRAKE_ontouch = false; }

    void speedControl()
    {
        if (ACC_onTouch == true && maxlimit != true)
        {
            if (speed <= 600) // 0 - 600
            {
                speed = speed + 1.5f;
                // print("G 1");
            }
            else if (speed >= 600.1 && speed <= 900) // 600 - 900
            {
                speed = speed + 0.5f;
                // print("G 2");
            }
            else if (speed >= 900.1) // 900+
            {
                speed = speed + 0.1f;
                // print("G 3");
            }
        }else if (ACC_onTouch == false)
        {
            speed = speed - 5f * 0.4f;
        }
        // max limit
        if(speed > 1200)
        {
            maxlimit = true;
            speed = 1200;
        }else if(speed < 1200)
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
            speed = speed - 10;

            if(speed < 200)
            {
                speed = 200;
            }
        }
    }
}
