using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour {

    public GameObject[] Blocker;
    public GameObject car;
    public int numberRandom;
    public int positionRandom;
    public float[] positionSpawn;

    void Start ()
    {
        car = GameObject.FindWithTag("Player");
        positionSpawn = new float[4];
        positionSpawn[0] = -3.15f;
        positionSpawn[1] = -1f;
        positionSpawn[2] = 1.2f;
        positionSpawn[3] = 3.2f;

        InvokeRepeating("SpawnCar", 3, 1);
    }

    void FixedUpdate()
    {
        numberRandom = Random.Range(0, 2);
        positionRandom = Random.Range(0, 4);       
        // InvokeRepeating("SpawnCar",3,3);       
    }
    void Update () {
        
    }

    void SpawnCar()
    {
        GameObject.Instantiate(Blocker[numberRandom], new Vector3(positionSpawn[positionRandom], car.transform.position.y + 1, car.transform.position.z + 100f), Quaternion.identity);
    } 
}
