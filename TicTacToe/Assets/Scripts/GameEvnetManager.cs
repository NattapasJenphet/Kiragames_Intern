using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvnetManager : MonoBehaviour {
	
	void Start () {
		
	}

	void Update () {
		
	}
    public void backtoMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
    }
}
