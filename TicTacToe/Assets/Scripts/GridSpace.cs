using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

    public Button button;
    public Text buttonText;
    public string playerSide;
    private GameController gameController;
    public GameObject sc_GameController;
    

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();      
        button.interactable = false;
        if (sc_GameController.GetComponent<GameController>().playerSide == "O")
        {

            this.buttonText.GetComponent<Text>().color = new Color(0.83f, 0.27f, 0.39f);
        }
        else
        {
            this.buttonText.GetComponent<Text>().color = new Color(0.35f, 0.56f, 0.76f);
        }
        gameController.EndTurn();  
    }  
}

