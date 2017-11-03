using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour {

    // Use this for initialization
    public GameObject Car;
    public GameObject[] Road;
    public bool[] Create;

	void Start () {
        Car = GameObject.Find("Car");
        Create = new bool[5];
    }
	
	// Update is called once per frame
	void Update () {
        CurrentRoad();
    }
    void CurrentRoad()
    {
        for (int i=0; i<Road.Length; i++)
        {
           if(Car.GetComponent<AccelerometerInput>().OnRoad == Road[i].name)
            {
                if (i == 0 && Create[i] == false)
                // Instantiate(Road[Road.Length-1], new Vector3(0, 0, 0), Quaternion.identity);
                {
                  GameObject Clone = Instantiate(Road[4],new Vector3( 0,0, (Road[3].transform.position.z) + 30 ), Quaternion.identity);
                  Clone.name = Road[4].name;
                  Create[i] = true;
                }
                else if(i == 1 && Create[i] == false)
                {
                  GameObject Clone = Instantiate(Road[0], new Vector3(0, 0, (Road[2].transform.position.z) + 30 * (i+1+1)), Quaternion.identity);
                  Clone.name = Road[0].name;
                  Create[i] = true;
                }
                else if (i == 2 && Create[i] == false)
                {
                  GameObject Clone = Instantiate(Road[1], new Vector3(0, 0, (Road[1].transform.position.z) + 30 * (i+1+1+1)), Quaternion.identity);
                  Clone.name = Road[1].name;
                  Create[i] = true;
                }
                else if (i == 3 && Create[i] == false)
                {
                  GameObject Clone = Instantiate(Road[2], new Vector3(0, 0, (Road[0].transform.position.z) + 30 * (i+1+1+1+1)), Quaternion.identity);
                  Clone.name = Road[2].name;
                  Create[i] = true;
                }
                else if (i == 4 && Create[i] == false)
                {
                  GameObject Clone = Instantiate(Road[1], new Vector3(0, 0, (Road[4].transform.position.z) + 30 * (i+1+1+1+1+1)), Quaternion.identity);
                  Clone.name = Road[4].name;
                  Create[i] = true;
                }           
            }
        }
    }
}
