using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    public GameObject mainCar;
    public float carX = 20;
    public float carY;
    public float carZ;
    public float lenghtCamera; // default 1.1f
    public bool accButtonStatus;
    public bool dieStatus;

    void Update()
    {
        accButtonStatus = mainCar.GetComponent<AccelerometerInput>().ACC_onTouch;
        dieStatus = mainCar.GetComponent<Animator>().GetBool("Crash");
        transform.eulerAngles = new Vector3(carX, carY, carZ);
        transform.position = new Vector3(mainCar.transform.position.x, transform.position.y, mainCar.transform.position.z - lenghtCamera);
        
        if (accButtonStatus == true && dieStatus == false)
        {
            lenghtCamera = Mathf.Lerp(lenghtCamera, 1.8f, 0.8f * Time.deltaTime);
        }
        else if (accButtonStatus == false && dieStatus == false)
        {
            lenghtCamera = Mathf.Lerp(lenghtCamera, 1.2f, 1f * Time.deltaTime);
        }
        else if (dieStatus == true)
        {
            lenghtCamera = Mathf.Lerp(lenghtCamera, -0.25f, 4f * Time.deltaTime);
        }
    }
}
