using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ScoreboardScript : MonoBehaviour {

    public UnityEngine.UI.InputField inputField; //input for name
    private GameControllerScript gameController; //game controller is still needed

    // GUI components
    public GUIText scoreText;
    public GUIText errorText;
    public GameObject initialDisplay;
    // for leaderboard
    public GameObject board;
    public GUIText[] namePlacements; 
    public GUIText[] scorePlacements;

    private string filePath = "ScoreKeeper.txt"; //file path
    private string scorerName; //stores the name of the scorer
    private bool isEnterHit;

    // lists for the reading and writing of files
    private List<Scorer> listScores;
    private List<string> stringListScores;

    private bool fixErrorAfterSave; // makes old GUI unaccessable when used

    // Use this for initialization
    void Start () {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameControllerScript>();

        fixErrorAfterSave = true;

        listScores = new List<Scorer>();

        isEnterHit = false;

        scoreText.text = "" + gameController.score;

        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        ReadFile();

    }
	
	// Update is called once per frame
	void Update () {

        // gets initial name to begin checking for if they belong on leaderboard
        if (Input.GetKey(KeyCode.Return) && !isEnterHit)
        {
            isEnterHit = true;
            scorerName = inputField.text;

            if(scorerName.Length < 10)
            {
                EnterScorerInfo();
            }
            else
            {
                errorText.text = "Please enter a name between 1-10 characters, then press Enter.";
            }

        }
	}

    // allows for player to enter name
    void OnGUI()
    {
        if (fixErrorAfterSave)
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
            GUI.FocusControl("InputField");
        }
    }

    // reads file and stores it in arraylist to be manipulated with later
    void ReadFile()
    {

        stringListScores = File.ReadAllLines(filePath).ToList();

        foreach (string s in stringListScores){
            string[] strArr = s.Split(' ');

            Scorer person = new Scorer(strArr[0], System.Int32.Parse(strArr[1]));

            listScores.Add(person);
        }

    }

    // enters the information and sorts it so it can be written on leaderboard
    void EnterScorerInfo()
    { 

        Scorer newPerson = new Scorer(scorerName, gameController.score);

        listScores.Add(newPerson);

        listScores.Sort();

        stringListScores = new List<string>(5);


        for (int i = 0; i < 5; i++)
        {
            stringListScores.Add(listScores[i].name + " " + listScores[i].score);
        }

        WriteFile();
    }

    // write file for save data
    void WriteFile()
    {
        File.Delete(filePath);
        File.Create(filePath).Close();

        File.WriteAllLines(filePath, stringListScores.GetRange(0,5).ToArray());

        DisplayScores();
    }

    // show top 5 scores
    void DisplayScores()
    {
        initialDisplay.SetActive(false);
        board.SetActive(true);

        for (int i = 0; i < 5; i++)
        {
            string[] str = stringListScores[i].Split(' ');
            namePlacements[i].text = str[0];
            scorePlacements[i].text = str[1];
        }

        fixErrorAfterSave = false;

    }
}
