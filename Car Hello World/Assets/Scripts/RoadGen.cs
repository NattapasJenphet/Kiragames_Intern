using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour {

    // Use this for initialization
    public GameObject Car;
    public GameObject[] Road;
    public bool[] Create;
    public int Distance;

	void Start () {
        Car = GameObject.Find("Car");
        Create = new bool[5];
        Distance = 120;
    }
	
	// Update is called once per frame
	void Update () {
        CurrentRoad();
        CheckCreate();
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
                    // GameObject Clone = Instantiate(Road[4],new Vector3( 0,0, Distance), Quaternion.identity);
                    //GameObject Clone;
                    GameObject.Instantiate(Road[4],new Vector3(0, 0, Distance),Quaternion.identity);
                    // Clone.name = Road[4].name;
                    Distance = Distance + 30;
                    Create[i] = true;
                }
                else if(i == 1 && Create[i] == false)
                {
                    // GameObject Clone = Instantiate(Road[0], new Vector3(0, 0, Distance), Quaternion.identity);
                    // Clone.name = Road[0].name;
                    Distance = Distance + 30;
                    Create[i] = true;
                }
                else if (i == 2 && Create[i] == false)
                {
                    // GameObject Clone = Instantiate(Road[1], new Vector3(0, 0, Distance), Quaternion.identity);
                    // Clone.name = Road[1].name;
                    Distance = Distance + 30;
                    Create[i] = true;
                }
                else if (i == 3 && Create[i] == false)
                {
                    // GameObject Clone = Instantiate(Road[2], new Vector3(0, 0, Distance), Quaternion.identity);
                    // Clone.name = Road[2].name;
                    Distance = Distance + 30;
                    Create[i] = true;
                }
                else if (i == 4 && Create[i] == false)
                {
                    // GameObject Clone = Instantiate(Road[3], new Vector3(0, 0, Distance), Quaternion.identity);
                    // Clone.name = Road[3].name;
                    Distance = Distance + 30;
                    Create[i] = true;
                  
                }           
            }
        }
    }
    void CheckCreate()
    {
        if(Create[0] == true && 
           Create[1] == true &&
           Create[2] == true &&
           Create[3] == true &&
           Create[4] == true )
        {
            Create[0] = false;
            Create[1] = false;
            Create[2] = false;
            Create[3] = false;
            Create[4] = false;
        }
        
    }
}
