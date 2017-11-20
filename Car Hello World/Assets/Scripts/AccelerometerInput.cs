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
    public bool maxLimit;
    public bool minLimit;
    // debug text
    [Space(10)]
    public Text valueSpeed;
    public Text dirDebug;
    public Text fixedValueSpeed;
    [Space(10)]
    public Vector3 currentPos;
    public Vector3 fixedPos;
    public float fixedDeltaPos;
    public float cov_km;

    void Start ()
	{
        Car = this.gameObject;
    }

    void FixedUpdate()
    {
        fixedPos = Car.transform.position;
        fixedDeltaPos = fixedDeltaPos / Time.deltaTime;
        cov_km = fixedDeltaPos * 3.6f;
        cov_km = (int)cov_km;
    }
    
    void Update ()
    {
        currentPos = Car.transform.position;
        fixedDeltaPos = Vector3.Distance(currentPos, fixedPos);     
        fixedValueSpeed.text = ("Fixed Speed : " + cov_km);

        resetPosition();       
        accInput();
        // move forward
        Car.GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
        // Car.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 1f * speed * Time.deltaTime);
        // Car.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
    }

   /* void velocityConvert()
    {
        // valueSpeed.text = ("Speed : " + GetComponent<Rigidbody>().velocity.magnitude * 3.6f);  
        float lenghtVector = GetComponent<Rigidbody>().velocity.magnitude;
        float cov_lenghtVector = lenghtVector * 3.6f;
        valueSpeed.text = ("Speed : " + (int)cov_lenghtVector);
    }*/

    void accInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        // Debug text
        dirDebug.text = ("Acc " + dir.x);

        // keyboard control editor
        if (Input.GetKey(KeyCode.A))
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(-10f * 10f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(10f * 10f, 0f, 0f);
        }

        // transform.Translate(dir.x, 0f, 0f);   
        // Car.GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 1f * speed * Time.deltaTime);
        // Car.GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, speed);
        // Car.GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, 10f);

        // turn left & right control
        Car.GetComponent<Rigidbody>().AddRelativeForce(dir.x * 10f, 0f, 0f); 
        speedControl();
   
    }

    public void AccOnTouch_true() { ACC_onTouch = true; }
    public void AccOnTouch_false() { ACC_onTouch = false; }
    public void BrakeOnTouch_true() { BRAKE_ontouch = true; }
    public void BrakeOnTouch_false() { BRAKE_ontouch = false; }

    void speedControl()
    {
        if (ACC_onTouch == true && maxLimit != true) // use button acc
        {
            if (speed <= 20) // 0 - 20
            {
                speed = speed + 0.1f;        
                // print("G 1");
            }
            else if (speed >= 20 && speed <= 52) // 20 - 100
            {
                speed = speed + 0.05f;
                // print("G 2");
            }
            else if (speed >= 52 && speed <= 57) // 20 - 100
            {
                speed = speed + 0.005f;
                // print("G 3");
            }
            else if (speed >= 57) // 100+
            {
                speed = speed + 0.001f;
                // print("G 4");
            }
        }else if (ACC_onTouch == false) // release button acc
        {
                speed = speed - 0.05f;
        }
        // max limit
        if(speed >= 60f ) 
        {
            maxLimit = true;
            speed = 60f;

        }else if(speed <= 60f )
        {
            maxLimit = false;
        }
        // min limit
        if (speed <= 5.6f)
        {
            minLimit = true;
            speed = 5.6f;
        }else if (speed >= 5.6f)
        {
            minLimit = false;
        }
        if(BRAKE_ontouch == true && minLimit != true) // use button brake
        {
            speed = speed - 0.8f;
        }
    }

    [HideInInspector]
    public float count = 0;
    [HideInInspector]
    public float n_count = 0;
    [HideInInspector]
    public bool check = false;
    [HideInInspector]
    public float oldPos;
    [HideInInspector]
    public float newPos;    

    void resetPosition()
    {
        oldPos = Car.transform.position.z;
        count = count + 1 * Time.deltaTime;
        if (count > 6)
        {    
            if (oldPos == newPos)
            {              
                n_count = n_count + 1 * Time.deltaTime;
                if(n_count > 1.5f)
                {
                    Car.transform.position = new Vector3(0f, Car.transform.position.y, Car.transform.position.z);
                    count = 0;
                    n_count = 0;
                    check = false;
                }
            }
            else if (oldPos != newPos)
            {
                count = 0;
                n_count = 0;
                check = false;
            }
        }
        if(count > 4 && check == false)
        {
            newPos = Car.transform.position.z;
            check = true;
        }
    }
}
