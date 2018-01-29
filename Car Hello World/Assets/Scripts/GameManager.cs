using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public enum GameState {
    menu,
    gamePlay,
    gameOver
}

    public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public GameState currentGameState = GameState.menu;

        void Awake()
    {
        instance = this;
    }
        void Start ()
    {
        currentGameState = GameState.menu;
	}
	
        public void StartGame()
    {
        SetGameState(GameState.gamePlay);
    }
        public void gameOver()
    {
        SetGameState(GameState.gameOver);
    }
	
        public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
            print("Menu");
        }
        else if(newGameState == GameState.gamePlay)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GamePlay");
            print("GamePlay");
        }
        else if(newGameState == GameState.gameOver)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
            print("GameOver");
        }
        currentGameState = newGameState;
    }       
}
