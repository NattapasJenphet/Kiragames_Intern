using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    public GameObject Text_tap;
    public Vector3 CenterScreen;
    GameObject ittle;
    Animator anim;
    Animator c_anim;

	void Start () {
        ittle = GameObject.Find("Ittle");
        anim = ittle.GetComponent<Animator>();
        c_anim = GetComponent<Animator>();

    } 
	
	void Update ()
    {
        TapEvent();
        fadeAnim(Text_tap);   

    }

    void TapEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {        
            Invoke("SceneLoad", 1.5f);
            anim.SetBool("Onclick", true);
            c_anim.SetBool("Onclick", true);
            Text_tap.SetActive(false);
        }
    }
    void fadeAnim(GameObject mytext)
    {     
        mytext.GetComponent<CanvasGroup>().alpha = Mathf.PingPong(Time.time*3f, 1f);
    }
        void SceneLoad()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
    }
}
