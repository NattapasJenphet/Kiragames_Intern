using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

	
	void Start () {
		
	}
	
	void Update () {
        Onclick();
    }

    void Onclick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
        }
    }
}
