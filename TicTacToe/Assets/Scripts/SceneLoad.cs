using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

    public GameObject TapGp;
	
	void Start () {
		
	}
	
	void Update () {
        Onclick();
        TapGp.GetComponent<CanvasGroup>().alpha = Mathf.PingPong(Time.time * 3f, 1f);
    }

    void Onclick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
        }
    }
}
