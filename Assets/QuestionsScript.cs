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
    private child subtitle;
    

    // Start is called before the first frame update
    void Start()
    {
        incorrect = 0;
        pointer = -1;
        var dataset = Resources.Load<TextAsset>("Questions");
        var rows = Regex.Split(dataset.text, "\n(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        var data = Regex.Split(rows[1], ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        question = data[1..];
        numQuestions = question.Length;
        for (int i = 0; i<numQuestions; i++)
        {
            question[i] = question[i].Replace("\"", string.Empty);
        }

        //question = new string[5] { "Serious games are primarily designed for which purpose?", "Which mainstream game has been referenced for its unintended role in modeling disease spread, offering insights into epidemiology?",
        //    "With the reduction in costs for gaming hardware, which device has specifically gained significant attention for its application in serious gaming due to immersive experiences?", 
        //    "What was the name of the modified version of 'Doom' that was used by the United States military for training?", "What is Tim’s favorite food?" };
        script = GetComponent<TMPro.TextMeshProUGUI>();
        subtitle = GetComponentInChildren<child>();
        answered = false;
        //next.gameObject.SetActive(false);
        //script.text = rows[10].Split(',')[1];
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

    public void NextQuestion()
    {
        if (pointer == -1)
        {
            subtitle.gameObject.SetActive(false);
            script.alignment = TMPro.TextAlignmentOptions.Center;
        }
        if (pointer < numQuestions-1)
        {
            pointer += 1;
            answered = false;
            for (int i=0; i<4; i++)
            {
                answers[i].NextQuestion(pointer);
            }
        }
        else
        {
            next.gameObject.SetActive(false);
            for (int i = 0; i < 4; i++)
            {
                answers[i].FinalCall();
            }
        }
    }

    public void WrongAnswer()
    {
        incorrect += 1;
        if (incorrect >= allowed_fails)
        {
            plank.PullOut();
            pointer = numQuestions;
            next.gameObject.SetActive(false);
        }
        else
        {
            plank.anim.Play("Base Layer.Wobble");
        }
        
        //Do something
    }
}
