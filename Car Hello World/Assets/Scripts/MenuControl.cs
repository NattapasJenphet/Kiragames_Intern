using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    public GameObject Text_tap;
    public GameObject[] ittle;
    public Vector3 CenterScreen;
    public int num = 1;
	void Start () {
	
	} 
	
	void Update ()
    {
        TapEvent();
        fadeAnim(Text_tap);
        animationIttle(num);

    }

    void TapEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Enter Game");
            num = 2;
            Invoke("SceneLoad", 1f);
        }
    }
    void fadeAnim(GameObject mytext)
    {     
        mytext.GetComponent<CanvasGroup>().alpha = Mathf.PingPong(Time.time*3f, 1f);
    }
    void animationIttle(int value)
    {
        value = num;
        switch (value) {
            case 1:
                ittle[0].transform.position = Vector3.Lerp(ittle[0].transform.position, new Vector3(CenterScreen.x, ittle[0].transform.position.y, 0f), 1f * Time.deltaTime);
                ittle[1].transform.position = Vector3.Lerp(ittle[1].transform.position, new Vector3(CenterScreen.x, ittle[1].transform.position.y, 0f), 1f * Time.deltaTime);
                break;
            case 2:
                ittle[0].transform.position = Vector3.Lerp(ittle[0].transform.position, new Vector3(1338, ittle[0].transform.position.y, 0f), 1f * Time.deltaTime);
                ittle[1].transform.position = Vector3.Lerp(ittle[1].transform.position, new Vector3(-338, ittle[1].transform.position.y, 0f), 1f * Time.deltaTime);
                break;
        }
    }
    void SceneLoad()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
    }
}
