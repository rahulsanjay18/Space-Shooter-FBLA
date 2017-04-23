using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Just sets up the main menu screen
public class MainMenuScript : MonoBehaviour {

    public Texture background;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Shooter", LoadSceneMode.Single);
        }
        if (Input.GetKey(KeyCode.I))
        {
            SceneManager.LoadScene("Instructions", LoadSceneMode.Single);
        }
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

}
