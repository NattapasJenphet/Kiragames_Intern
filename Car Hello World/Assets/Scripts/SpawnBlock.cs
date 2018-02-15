using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{

    public GameObject[] Blocker;
    public GameObject carPlayer;
    public int numberRandomCar;
    public int positionRandom;
    public float[] positionSpawn;
    public float timeCount;
    public int rayCount;
    public bool[] CreateCar;
    void Start()
    {
        carPlayer = GameObject.FindWithTag("Player");
        positionSpawn = new float[4];
        positionSpawn[0] = -3.15f;
        positionSpawn[1] = -1f;
        positionSpawn[2] = 1.2f;
        positionSpawn[3] = 3.2f;
    }

    void FixedUpdate()
    {
        numberRandomCar = Random.Range(0, 2);
        positionRandom = Random.Range(0, 4);
    }

    void Update()
    {
        rayCount = carPlayer.GetComponent<AccelerometerInput>().rayCountTrigger;
        timeCount += 1 * Time.deltaTime;
        LevelControl();
    }

    void LevelControl()
    {
        float distance = carPlayer.GetComponent<AccelerometerInput>().distanceTravelled;

        if (distance > 300 && carPlayer.GetComponent<AccelerometerInput>().ACC_onTouch == true)
        {
            print("HARD CAR");
            SpawnCar(1, "SpawnOneCar");
        }
        else
        {
            print("NORMAL CAR");
            SpawnCar(2, "SpawnOneCar");
        }

        if (distance > 300)
        {
            if (rayCount % 3 == 1 && CreateCar[0] == false)
            {
                SpawnTwoCar();
                StartCoroutine(ResetCreate(0, 3));
            }

            if (rayCount % 5 == 0 && CreateCar[1] == false)
            {
                SpawnThreeCar();
                StartCoroutine(ResetCreate(1, 5));
            }
        }
        else if (distance > 300 && distance < 1000)
        {
            if (rayCount % 2 == 1 && CreateCar[0] == false)
            {
                SpawnTwoCar();
                StartCoroutine(ResetCreate(0, 1));
            }

            if (rayCount % 4 == 0 && CreateCar[1] == false)
            {
                SpawnThreeCar();
                StartCoroutine(ResetCreate(1, 2));
            }
        }
    }
    void SpawnOneCar()
    {
        GameObject.Instantiate(Blocker[numberRandomCar], new Vector3(positionSpawn[positionRandom], carPlayer.transform.position.y + 1, carPlayer.transform.position.z + 100f), Quaternion.identity);
    }

    void SpawnTwoCar()
    {
        int laneOne = positionRandom;
        for (int i = 0; i < positionSpawn.Length; i++)
        {
            if (i == laneOne)
            {
                int laneTwo = Random.Range(0, 4);
                if (laneOne != laneTwo)
                {
                    for (int j = 0; j < positionSpawn.Length; j++)
                    {
                        if (j != laneOne && j != laneTwo)
                        {
                            GameObject.Instantiate(Blocker[numberRandomCar], new Vector3(positionSpawn[j], carPlayer.transform.position.y + 1, carPlayer.transform.position.z + 120f), Quaternion.identity);
                        }
                    }
                    // carPlayer.GetComponent<AccelerometerInput>().rayCountTrigger = 0;
                    CreateCar[0] = true;
                }
            }
        }
    }

    void SpawnThreeCar()
    {
        for (int i = 0; i < positionSpawn.Length; i++)
        {
            if (positionRandom != i)
            {
                GameObject.Instantiate(Blocker[numberRandomCar], new Vector3(positionSpawn[i], carPlayer.transform.position.y + 1, carPlayer.transform.position.z + 130f), Quaternion.identity);
                numberRandomCar = Random.Range(0, 2);
            }
        }
        // carPlayer.GetComponent<AccelerometerInput>().rayCountTrigger = 0;
        CreateCar[1] = true;
    }

    void SpawnCar(int coolDown, string nameMethod)
    {
        if (timeCount > coolDown)
        {
            Invoke(nameMethod, 0f);
            timeCount = 0;
        }
    }

    IEnumerator ResetCreate(int numBoolCreate, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        CreateCar[numBoolCreate] = false;
    }
}
