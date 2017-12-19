using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal enum GearType
{
        Four_Gears,
        Five_Gears,
        Six_Gears
}

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
    public Text Geartext;
    [HideInInspector]
    public Vector3 currentPos;
    [HideInInspector]
    public Vector3 lastPos;
    [Space(10)]
    public float[] gearRatio;
    public int gear;
    [SerializeField] private GearType m_GearType = GearType.Four_Gears;
    public Rigidbody rb;
    public AudioSource[] soundEffect;

    void Start ()
	{       
        rb = Car.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.25f, 0f);
        ApplyGear();   
    }

    [Space(10)]
    public WheelCollider BR;
    public WheelCollider BL;
    public GameObject[] WheelRotate;
    public float engineTorque; // = 25f;

    void FixedUpdate()
    {
        moveCar();       
        Vector3 deltaPos = transform.position - lastPos;
        speed = (int)(deltaPos.z / Time.deltaTime * 3.6f);       
        fixedValueSpeed.text = ("Fixed Speed : " + speed);
        lastPos = transform.position;
    }
    
    void Update ()
    {
        ApplyGear();
        accInput();
        brakeCar();
        resetPosition();
        carEngineSound();
        TouchCount();
        WheelRotate[0].transform.Rotate(BL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRotate[1].transform.Rotate(BR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRotate[2].transform.Rotate(BL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRotate[3].transform.Rotate(BR.rpm / 60 * 360 * Time.deltaTime, 0, 0);       
      
        // print(BR.motorTorque);
    }
  
    void accInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        // Debug text
        dirDebug.text = ("Acc " + dir.x);
        Geartext.text = ("Gear " + (gear + 1));
        // keyboard control editor
        if (Input.GetKey(KeyCode.A))
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(-20f * 10f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(20f * 10f, 0f, 0f);
        }

        // turn left & right control
        if (speed != 0 && speed >= 10 )
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(dir.x * 500f, 0f, 0f);
        }
        // speedControl();
    }

    public float m_time;
    public float TopSpeed = 150;
    public float currentMotorturque;
 
    void moveCar()
    {
        if (ACC_onTouch == true)    
        //if (ACC_onTouch == true)
        {
            BR.motorTorque = currentMotorturque;
            BL.motorTorque = currentMotorturque;            
            //BR.motorTorque += (engineTorque / gearRatio[gear] * DeleyForce) / (m_time * 2);
            //BL.motorTorque += (engineTorque / gearRatio[gear] * DeleyForce) / (m_time * 2);                        
        }
        else if (ACC_onTouch == false || speed > TopSpeed)
        {
            BR.motorTorque = 0;
            BL.motorTorque = 0;
        }
    }
    void brakeCar()
    {
        if (BRAKE_ontouch == true)
        {
            BR.brakeTorque += 10f;
            BL.brakeTorque += 10f;           
        }
        else if (BRAKE_ontouch == false)
        {
            BR.brakeTorque = 0;
            BL.brakeTorque = 0;
        }
    }

    void ApplyGear()
    {
        switch (m_GearType)
        {
            case GearType.Four_Gears :
                gearRatio = new float[4];
                
                gearRatio[0] = 30;
                gearRatio[1] = 60;
                gearRatio[2] = 90;
                gearRatio[3] = 120;
                break;
            case GearType.Five_Gears :             
                gearRatio = new float[5];

                gearRatio[0] = 30;
                gearRatio[1] = 60;
                gearRatio[2] = 90;
                gearRatio[3] = 120;
                gearRatio[4] = 150;
                break;
            case GearType.Six_Gears :
                gearRatio = new float[6];

                gearRatio[0] = 30;
                gearRatio[1] = 60;
                gearRatio[2] = 90;
                gearRatio[3] = 120;
                gearRatio[4] = 150;
                gearRatio[5] = 180;
                break;
        }
    }

    [HideInInspector]
    public float DeleyForce;
    public void AccOnTouch_true()
    {
        ACC_onTouch = true;      
    }
    public void AccOnTouch_false()
    {
        ACC_onTouch = false;
        DeleyForce = 0f;
    }
    public void BrakeOnTouch_true()
    {
        BRAKE_ontouch = true;
        if (speed != 0)
        {
            soundEffect[1].Play();
        }
    }
    public void BrakeOnTouch_false() { BRAKE_ontouch = false; }  
    void TouchCount()
    {
        if(ACC_onTouch == true && DeleyForce <= 1f)
        {
            DeleyForce = DeleyForce + 0.5f * Time.deltaTime;
        }
    }

    /*
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
            //speed = speed - 0.05f;

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
           // speed = speed - 0.8f;
           BR.brakeTorque = 10;
           BL.brakeTorque = 10;
           print("Break");
        }
    }
    */   

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
    void carEngineSound()
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
        soundEffect[0].pitch = ((speed - Min_gear) / (Max_gear - Min_gear)) + 0.7f;
    }

    /* void velocityConvert()
   {
       // valueSpeed.text = ("Speed : " + GetComponent<Rigidbody>().velocity.magnitude * 3.6f);  
       float lenghtVector = GetComponent<Rigidbody>().velocity.magnitude;
       float cov_lenghtVector = lenghtVector * 3.6f;
       valueSpeed.text = ("Speed : " + (int)cov_lenghtVector);
   }*/
}
