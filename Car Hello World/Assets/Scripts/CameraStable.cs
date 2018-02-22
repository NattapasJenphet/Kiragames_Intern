using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStable : MonoBehaviour
{
    public GameObject CarPlayer;
    public float carX = 20;
    public float carY;
    public float carZ;
    public float lenghtCamera; // default 1.1f
    public bool accButtonStatus;
    public bool dieStatus;
    public Animator myCamera;

    private void Start()
    {
        CarPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        accButtonStatus = CarPlayer.GetComponent<CoreGameController>().ACC_onTouch;
        dieStatus = CarPlayer.GetComponent<Animator>().GetBool("Crash");
        myCamera = GetComponent<Animator>();
        transform.eulerAngles = new Vector3(carX, carY, carZ);
        transform.position = new Vector3(CarPlayer.transform.position.x, transform.position.y, CarPlayer.transform.position.z - lenghtCamera);

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
            lenghtCamera = Mathf.Lerp(lenghtCamera, -1.3f, 4f * Time.deltaTime);
        }

        if (CarPlayer.GetComponent<CoreGameController>().colliderCheck == true)
        {
            myCamera.SetBool("Shake", true);
        }
        else if (CarPlayer.GetComponent<CoreGameController>().colliderCheck == false)
        {
            myCamera.SetBool("Shake", false);
        }
    }
}
