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
    [HideInInspector]
    public Vector3 currentPos;
    [HideInInspector]
    public Vector3 lastPos;
    [Space(10)]
    public float[] gearRatio;
    public int gear;
    public Rigidbody rb;
    public Vector3 temp;

    void Start ()
	{
        Car = this.gameObject;
        rb = Car.GetComponent<Rigidbody>();
        temp = rb.centerOfMass;
        temp.x = 0f;
        temp.y = -0.8f;
        temp.z = 0f;
        rb.centerOfMass = temp;
    }

    [Space(10)]
    public GameObject wheel_BR;
    public GameObject wheell_BL;
    public WheelCollider BR;
    public WheelCollider BL;

    void FixedUpdate()
    {
        moveCar();
        Vector3 deltaPos = transform.position - lastPos;
        fixedValueSpeed.text = ("Fixed Speed : " + (int)(deltaPos.z / Time.deltaTime * 3.6f));
		lastPos = transform.position;
        
    }
    
    void Update ()
    {
        resetPosition();       
        accInput();
        // carEngine();
        // move forward
        // Car.GetComponent<Rigidbody>().velocity = Vector3.forward * speed; 
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

        // turn left & right control
        Car.GetComponent<Rigidbody>().AddRelativeForce(dir.x * 10f, 0f, 0f); 
        speedControl();
    }

    void moveCar()
    {
        BR.motorTorque =  50*Input.GetAxis("Vertical");
        BL.motorTorque =  50*Input.GetAxis("Vertical");
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
            else if (speed >= 20 && speed <= 52) // 20 - 52
            {
                speed = speed + 0.05f;
                // print("G 2");
            }
            else if (speed >= 52 && speed <= 57) // 52 - 57
            {
                speed = speed + 0.005f;
                // print("G 3");
            }
            else if (speed >= 57) // 57+
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

    void carEngine()
    {
        for (int i = 0; i < gearRatio.Length; i++)
        {
            if (gearRatio[i] > speed)
            {
                gear = i;
                break;
            }
        }
            float Max_gear = 0;
            float Min_gear = 0;
            if(gear == 0)
            {
                Min_gear = 0;
            }
            else
            {
                Min_gear = gearRatio[gear - 1];
            }
            Max_gear = gearRatio[gear];
            GetComponent<AudioSource>().pitch = ((speed - Min_gear) / (Max_gear - Min_gear)) + 0.8f;
    }
}
