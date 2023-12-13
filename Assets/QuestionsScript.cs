using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsScript : MonoBehaviour
{

    public Button next;
    public AnswersScript[] answers;
    public string[] question;
    public int pointer;
    public bool answered;
    public bool correct;
    public PullThePlank plank;
    TMPro.TextMeshProUGUI script;
    private child subtitle;

    // Start is called before the first frame update
    void Start()
    {
        pointer = -1;
        question = new string[5] { "Serious games are primarily designed for which purpose?", "Which mainstream game has been referenced for its unintended role in modeling disease spread, offering insights into epidemiology?",
            "With the reduction in costs for gaming hardware, which device has specifically gained significant attention for its application in serious gaming due to immersive experiences?", 
            "What was the name of the modified version of 'Doom' that was used by the United States military for training?", "What is Tim’s favorite food?" };
        script = GetComponent<TMPro.TextMeshProUGUI>();
        subtitle = GetComponentInChildren<child>();
        answered = false;
        //next.gameObject.SetActive(false);
        correct = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointer>=0 && pointer < 5)
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
        if (!correct)
        {
            plank.PullOut();
            pointer = 5;
        }
        if (pointer < 4)
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
}
