using UnityEngine;
using System.Collections;

public class addForceTest : MonoBehaviour {

    public Rigidbody rb;
    public float velocity;
    public Vector3 v_velocity;

     void Awake()
    {
        // print("function Awake " + Time.deltaTime);
    }

    void Start ()
    {
        // print("function start " + Time.deltaTime);
        rb = GetComponent<Rigidbody>();
	}
    void FixedUpdate()
    {
        // print("function FixedUpdate " + Time.deltaTime);
    }

    void Update () {

        // print("function Update " + Time.deltaTime);

        velocity = rb.velocity.magnitude;
        v_velocity = rb.velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 10f, 0);
            velocity = rb.velocity.magnitude;
            v_velocity = rb.velocity;
            print("test velocity");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * 100f);
            print("Add Force");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 100f);
            print("Add Relative Force ");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(Vector3.right * 100f);
            print("Key right");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(Vector3.left * 100f);
            print("Key left");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(Vector3.up * 100f);
            print("Key up");
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(Vector3.down * 100f);
            print("Key up");
        }
    }

    void LateUpdate()
    {
        // print("function LastUpdate " + Time.deltaTime);
    }
}
