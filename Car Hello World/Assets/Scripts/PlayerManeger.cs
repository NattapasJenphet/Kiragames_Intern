using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManeger : MonoBehaviour {

    public static PlayerManeger instance;

    void Awake()
    {
        instance = this;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Blocker")
        {          
            print("You lose");
            GameManager.instance.gameOver();
        }
    }
}
