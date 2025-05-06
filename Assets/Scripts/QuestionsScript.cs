using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class QuestionsScript : MonoBehaviour
{

    public Button next;
    public AnswersScript[] answers;
    private string[] question;
    public int pointer;
    public bool answered;
    public bool correct;
    public int numQuestions;
    public PullThePlank plank;
    private int incorrect;
    public int allowed_fails;
    TMPro.TextMeshProUGUI script;
    private GameObject subtitle;
    

    // Start is called before the first frame update
    void Start()
    {
        incorrect = 0;
        pointer = -1;
        script = GetComponent<TMPro.TextMeshProUGUI>();
        subtitle = transform.GetChild(0).gameObject;
        answered = false;
        //load the data from the Questions csv, split it by newline to isolate the question row, then seperate by comma to get the individual questions
        var dataset = Resources.Load<TextAsset>("Questions");
        var rows = Regex.Split(dataset.text, "\n(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        var data = Regex.Split(rows[1], ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        question = data[1..];
        //identify how many questions there are and clean all of the questions of quotation marks
        numQuestions = question.Length;
        for (int i = 0; i<numQuestions; i++)
        {
            question[i] = question[i].Replace("\"", string.Empty);
        }
        //pull the title from the Questions csv
        string[] title_parts = rows[10].Split(',')[1..];
        string title = "";
        for (int i = 0; i < title_parts.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(title_parts[i]))
            {
                title = string.Join('\n',title, title_parts[i]);
            }
        }
        script.text = title;
        //pull the number of allowed incorrect answers from the Questions csv
        allowed_fails = int.Parse(rows[11].Split(',')[1]);
    }

    // Update is called once per frame
    void Update()
    {
        if (pointer>=0 && pointer < numQuestions)
        {
            script.text = question[pointer];
        }
    }

    public void NextQuestion()          //moving to the next question when the arrow is selected
    {
        if (pointer == -1)              //if this is the first call to this
        {
            subtitle.SetActive(false);
            script.alignment = TMPro.TextAlignmentOptions.Center;
        }
        if (pointer < numQuestions-1)   //if there is still more questions to go through
        {
            pointer += 1;               //point to the next question
            answered = false;
            for (int i=0; i<4; i++)
            {
                answers[i].NextQuestion(pointer);
            }
        }
        else                            //otherwise finalize the question screen
        {
            next.gameObject.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                answers[i].FinalCall();
            }
        }
    }

    public void WrongAnswer()           //if the player selects the wrong answer
    {
        incorrect += 1;                 //see if the number of incorrect answers is equal to the limit
        if (incorrect >= allowed_fails) //if yes, pull the plank
        {
            plank.PullOut();
            pointer = numQuestions;
            next.gameObject.SetActive(false);
        }
        else                            //otherwise woble the plank
        {
            plank.anim.Play("Base Layer.Wobble");
        }
        
        //Potentially add sound que here
    }
}
