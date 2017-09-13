using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;

public class sceneManger : MonoBehaviour {

    Scene currentScene;    
    string previousScene;
    public string titleSceneName;
    public string connectSceneName;
    public string singlePlayerSceneName;
    public string multiPlayerSceneName;
    public string gameOverSceneName;

    // Use this for initialization
    void Start () {
        Object.DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        currentScene = SceneManager.GetActiveScene();

        //quits
        if (GamePad.GetButtonDown(GamePad.Button.Back, GamePad.Index.One))
        {
            Application.Quit(); 
        }

        if (string.Equals(currentScene.name, titleSceneName))
        {
            titleScene();
        }

        else if (string.Equals(currentScene.name, connectSceneName))
        {
            connectScene();
        }

        else if (string.Equals(currentScene.name, singlePlayerSceneName))
        {
            singleScene();
        }

        else if (string.Equals(currentScene.name, multiPlayerSceneName))
        {
            mulltiScene();
        }

        else if (string.Equals(currentScene.name, gameOverSceneName))
        {
            gameOverScene();
        }
    }

    void titleScene() 
    {
        string selection = ContentsManager.getSelection().name;

        if (string.Equals(selection,"play") && GamePad.GetButtonDown(GamePad.Button.A,GamePad.Index.One))
        {
            SceneManager.LoadScene(connectSceneName);
        }
        else if (string.Equals(selection, "quit") && GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
        {
            Application.Quit();
        }
    }

    void connectScene() {

        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One)) {

            SceneManager.LoadScene(singlePlayerSceneName);
        }
        else if(GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One)){

            SceneManager.LoadScene(multiPlayerSceneName);
        }

    }

    void singleScene()
    {
        previousScene = singlePlayerSceneName;

        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))//player is dead
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    void mulltiScene()
    {
        previousScene = multiPlayerSceneName;

        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))//player wins
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    void gameOverScene() {

        if (GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One))
        {
            SceneManager.LoadScene(previousScene);
        }
        else if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One))
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}
