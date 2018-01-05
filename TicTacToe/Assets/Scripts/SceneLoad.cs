using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

    public GameObject tapText;
    private Animator animatorController;

    void Awake()
    {
        animatorController = gameObject.GetComponent<Animator>();
    }

    void Update ()
    {        
        Onclick();
        tapText.GetComponent<CanvasGroup>().alpha = Mathf.PingPong(Time.time * 3f, 1f);
    }

    void Onclick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Loadscene");
            animatorController.SetBool("loading", true);
            Invoke("LoadScene",2);
        }
    }

    void LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
    }
}
