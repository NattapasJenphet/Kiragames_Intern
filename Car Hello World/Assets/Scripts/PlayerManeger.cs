using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManeger : MonoBehaviour {
    
    public static PlayerManeger instance;
    public GameObject gameOverCanvas;   

    void Awake()
    {
        instance = this;      
    } 

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Blocker")
        {            
            GetComponent<Animator>().SetBool("Crash", true);
            GetComponent<AccelerometerInput>().soundEffect[2].Play();           
            GetComponent<AccelerometerInput>().colliderCheck = true;
            Invoke("GameOver", 1f);
            print("You lose");
        }
    }
    void GameOver()
    {
        Time.timeScale = 0;       
        gameOverCanvas.SetActive(true);
    }  
}
