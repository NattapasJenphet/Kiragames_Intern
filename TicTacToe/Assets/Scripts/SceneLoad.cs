using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

    public GameObject TapGp;
    private Animator Anim_Canvas;

    void Awake()
    {
        Anim_Canvas = gameObject.GetComponent<Animator>();
    }

    void Update ()
    {        
        Onclick();     
        TapGp.GetComponent<CanvasGroup>().alpha = Mathf.PingPong(Time.time * 3f, 1f);
    }

    void Onclick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Loadscene");
            Anim_Canvas.SetBool("Loading", true);
            Invoke("LoadScene",2);
        }
    }

    void LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
    }
}
