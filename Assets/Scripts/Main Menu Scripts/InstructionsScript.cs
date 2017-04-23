using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Just sets up the instructions screen
public class InstructionsScript : MonoBehaviour
{

    public Texture background;

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
