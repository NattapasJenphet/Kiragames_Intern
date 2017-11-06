using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccelerometerInput : MonoBehaviour
{
    public GameObject Car;
    public float speed = 10f;
    public int Count = 0;
    public string OnRoad;
	void Start ()
	{
        Car = this.gameObject;
    }
	
	void Update ()
    {
        //move
        Car.transform.Translate(Vector3.forward * speed * Time.deltaTime);  
        //reset
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            Count++;
        }
        if (Count >= 3)
        {
            SceneManager.LoadScene(0);
            Count = -1;
        }
    }

    void AccInput()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        transform.Translate(dir.x, 0, 0);
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.name == "Road")
        {
            OnRoad = (collision.collider.gameObject.transform.parent.name);
        }
    }
}
