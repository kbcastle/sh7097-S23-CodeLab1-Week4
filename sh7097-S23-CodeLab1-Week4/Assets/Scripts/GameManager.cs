using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private int score = 0;
    
    const string DIR_DATA = "/Data/";
    const string FILE_FAIL_SCORE = "failureScore.txt";

    private string PATH_FAIL_SCORE;
    
    public const string PREF_FAIL_SCORE = "fScore";

    private bool inGame = true;

    public int FailScore
    {
        get { return score; }
        set
        {
            score = value; 
            Debug.Log("new fail score");

            if (score > FailScore)
            {
                FailScore = score;
            }
        }
    }
    
    public List<int> failScores = new List<int>();
    
    
    public TextMeshPro textMeshPro;
    
    
    void Awake()
    {

        if (Instance == null) 
        {
            DontDestroyOnLoad(gameObject);
            Instance = this; 
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PATH_FAIL_SCORE = Application.dataPath + DIR_DATA + FILE_FAIL_SCORE;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelNumber == 1) //why can it not access levelNumber even though it's a public variable?
        {
            UpdateFailScore();
        }
    }

    void UpdateFailScore()
    {
        if (failScores.Count == 0)
        {
            if (File.Exists(PATH_FAIL_SCORE))
            {
                string fileContents = File.ReadAllText(PATH_FAIL_SCORE); //text from file as string

                string[] fileSplit = fileContents.Split("\n"); //split into array with new lines separating 

                for (int i = 1; i < fileSplit.Length - 1; i++)
                {
                    FailScore = Int32.Parse(fileSplit[i]);
                }
            }
            else
            {
                failScores.Add(0);
            }
        }

        for (int i = 0; i < failScores.Count; i++)
        {
            if (failScores[i] < FailScore)
            {
                failScores.Insert(i, FailScore);
                break;
            }
        }

        if (failScores.Count > 5)
        {
            failScores.RemoveRange(5, failScores.Count - 5);
        }

        string failScoreStr = "Failed Attempts:\n";

        for (int i = 0; i < failScores.Count; i++)
        {
            failScoreStr += failScores[i] + "\n";
        }
        
        displayText.text = failScoreStr; //no clue why it doesn't like displayText
        
        File.WriteAllText(PATH_FAIL_SCORE, failScoreStr);
    }
}
