using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {

    public float speedValue = 3f;
    
	void Update () {
     
        transform.Translate(new Vector3(0, 0, 1f * speedValue * Time.deltaTime));    
        
        if(transform.position.y < -19f && transform.position.y > -20f)
        {
            Destroy(this.gameObject);
        }
	}

    void BehoviorsOfCar()
    {

    }
}
