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
    private Animator animCar;
    public float speed;
    // button 
    public GameObject pauseCanvas;
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
    public Text timeMonitorText;
    public float timeMonitor;

    [HideInInspector]
    public Vector3 currentPosition;
    [HideInInspector]
    public Vector3 lastPosition;

    [Space(10)]
    public float[] gearRatio;
    public int gear;

    [SerializeField] private GearType m_GearType = GearType.Four_Gears;
    public Rigidbody rigibodyCar;
    public AudioSource[] soundEffect;
    public float pitchRandom;

    void Start()
    {
        StartCoroutine(SoundSetting(false, 0.5f));
        rigibodyCar = Car.GetComponent<Rigidbody>();
        animCar = Car.GetComponent<Animator>();
        rigibodyCar.centerOfMass = new Vector3(0f, -0.25f, 0f);
        n_lastPosition = transform.position;
        ApplyGear();
    }

    [Space(10)]
    public WheelCollider BR;
    public WheelCollider BL;
    public GameObject[] WheelRotate;
    public float engineTorque; // = 25f;
    public float topSpeed = 180f;
    public float DeleyForce;

    void FixedUpdate()
    {
        MoveCar();
        Vector3 deltaPos = transform.position - lastPosition;
        speed = (int)(deltaPos.z / Time.deltaTime * 3.6f);
        fixedValueSpeed.text = ("SPEED :  " + speed);
        lastPosition = transform.position;
    }

    void Update()
    {
        pitchRandom = Random.Range(-0.2f, 0.2f);
        timeMonitor += Time.deltaTime;
        float twoDecimal = Mathf.Round(timeMonitor * 100f);
        timeMonitorText.text = ("TIME " + twoDecimal / 100f);

        ApplyGear();
        SteerInput();
        BrakeCar();
        CarEngineSound();
        TouchCount();
        DistanceCalculate();

        // 4 Wheels Rotate 
        WheelRotate[0].transform.Rotate(BL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRotate[1].transform.Rotate(BR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRotate[2].transform.Rotate(BL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRotate[3].transform.Rotate(BR.rpm / 60 * 360 * Time.deltaTime, 0, 0);

        if (animCar.GetBool("Crash") == true)
        {
            StartCoroutine(SoundSetting(true, 0.8f));
        }
    }

    void SteerInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        if (dir.x > 0.2)
        {
            dir.x = 0.2f;
            animCar.SetInteger("Animation", 2);
        }
        else if (dir.x < -0.2)
        {
            dir.x = -0.2f;
            animCar.SetInteger("Animation", 1);
        }
        else if (dir.x < 0.1 && dir.x > -0.1)
        {
            animCar.SetInteger("Animation", 0);
        }
        // Debug text
        float distanceMonitor = Mathf.Round(distanceTravelled);
        dirDebug.text = ("DISTANCE : " + distanceMonitor / 100);
        Geartext.text = ("GEAR :  " + (gear + 1)); // Cut
        // keyboard control editor
        if (Input.GetKey(KeyCode.A))
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(-10f * 10f, 0f, 0f);
            animCar.SetInteger("Animation", 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(10f * 10f, 0f, 0f);
            animCar.SetInteger("Animation", 2);
        }
        // turn left & right control
        if (speed != 0 && speed >= 10)
        {
            Car.GetComponent<Rigidbody>().AddRelativeForce(dir.x * 500f, 0f, 0f);
        }
    }

    void MoveCar()
    {
        if (ACC_onTouch == true && speed < topSpeed && (BR.motorTorque + BL.motorTorque) < topSpeed)
        //if (ACC_onTouch == true)
        {
            //BR.motorTorque = (engineTorque / gearRatio[gear] * DeleyForce) / (m_time * 2);
            //BL.motorTorque = (engineTorque / gearRatio[gear] * DeleyForce) / (m_time * 2);
            BR.motorTorque += 0.2f;
            BL.motorTorque += 0.2f;
        }
        else if (ACC_onTouch == false)
        {
            BR.motorTorque = 10;
            BL.motorTorque = 10;
        }
    }

    void BrakeCar()
    {
        if (BRAKE_ontouch == true)
        {
            BR.brakeTorque += 5f;
            BL.brakeTorque += 5f;

            if (BR.brakeTorque > 5)
            {
                BR.brakeTorque = 5;
                BL.brakeTorque = 5;
            }
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
            case GearType.Four_Gears:
                gearRatio = new float[4];

                gearRatio[0] = 30;
                gearRatio[1] = 60;
                gearRatio[2] = 90;
                gearRatio[3] = 120;
                break;
            case GearType.Five_Gears:
                gearRatio = new float[5];

                gearRatio[0] = 30;
                gearRatio[1] = 60;
                gearRatio[2] = 90;
                gearRatio[3] = 120;
                gearRatio[4] = 150;
                break;
            case GearType.Six_Gears:
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
    public void BrakeOnTouch_false()
    {
        BRAKE_ontouch = false;
    }

    public void RestartButton()
    {
        GameManager.instance.gameOver();
    }

    public void PauseButton()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            for (int i = 0; i < soundEffect.Length; i++)
            {
                soundEffect[i].enabled = false;
            }
            pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        if (Input.GetMouseButtonDown(0) && Time.timeScale == 0)
        {
            for (int i = 0; i < soundEffect.Length; i++)
            {
                soundEffect[i].enabled = true;
            }
            pauseCanvas.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    void TouchCount()
    {
        if (ACC_onTouch == true && DeleyForce <= 1f)
        {
            DeleyForce = DeleyForce + 0.5f * Time.deltaTime;
        }
    }

    public IEnumerator SoundSetting(bool Mute, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        switch (Mute)
        {
            case false:
                soundEffect[0].volume = 0.05f;
                soundEffect[1].volume = 0.05f;
                break;
            case true:
                soundEffect[0].volume = 0f;
                soundEffect[1].volume = 0f;
                break;
            default:
                print("Error Case");
                break;
        }
    }

    void CarEngineSound()
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

        if (gear == 0)
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

    [Header("Score Calculate")]
    public Vector3 n_lastPosition;
    public float distanceTravelled;
    private float timeCount;
    public int rayCountTrigger;
    public Text scoreMonitoring;
    public bool colliderCheck;

    void DistanceCalculate()
    {
        distanceTravelled += Vector3.Distance(transform.position, n_lastPosition);
        n_lastPosition = transform.position;
        scoreMonitoring.text = (" " + (int)distanceTravelled);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == ("Fence"))
        {
            colliderCheck = true;
            soundEffect[2].pitch += pitchRandom;
            soundEffect[2].Play();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        colliderCheck = false;
        soundEffect[2].pitch = 1;
    }
}
