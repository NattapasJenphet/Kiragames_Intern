using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccelerometerInput : MonoBehaviour {

    public int Count = 0;

	void Start () {
        
    }
	
	void Update () {
        AccInput();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left click.");
            Count++;
        }
        if(Count >= 3)
        {
            SceneManager.LoadScene(0);
            Count = -1;
        }
    }
    void AccInput()
    {
        transform.Translate(Input.acceleration.x, 0, 0);
    }
}
