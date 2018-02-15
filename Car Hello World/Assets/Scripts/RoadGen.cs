using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGen : MonoBehaviour {

    public GameObject Car;
    public GameObject[] Road;
    public bool[] Create;
    public int Distance;
    public Vector3 PosCar;
    public float PosCar_division;
    public int covPosCar_division;

    void Start()
    { // create จำนวนของ prefab ถนน
        Create = new bool[5];
        CreateMap();      
    }

    void Update()
    {
        PosCar = Car.transform.position;
        PosCar_division = PosCar.z / 30;
        covPosCar_division = (int) PosCar_division;

        CurrentRoad();
        CheckCreate();
    }
    void CreateMap()
    {
        for(int i=0; i<Road.Length; i++)
        {
            GameObject.Instantiate(Road[i], new Vector3(0, 0, Distance), Quaternion.identity).name = Road[i].name;
            Distance = Distance + 30;
        }   
    }

    void CurrentRoad()
    {
        for (int i = 0; i < Road.Length; i++)
        { // modulo = จำนวนของ prefab ถนน 
            if (covPosCar_division % 5 == i && Create[i] == false)
            {
                GameObject.Instantiate(Road[i], new Vector3(0, 0, Distance), Quaternion.identity).name = Road[i].name;
                Distance = Distance + 30;
                Create[i] = true;
            }
        }
    }

    void CheckCreate()
    {
       if(Create[0] == true && Create[1] == true && Create[2] == true && Create[3] == true && Create[4] == true && covPosCar_division % 5 == 0)
        { // modulo = จำนวนของ prefab ถนน 
            Create[0] = false;
            Create[1] = false;
            Create[2] = false;
            Create[3] = false;
            Create[4] = false;
        }
    }
}
