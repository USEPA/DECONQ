using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswersScript : MonoBehaviour
{

    
    public QuestionsScript questionSource;
    public int pointer;
    public Color correct;
    public Color wrong;
    public Color greyed;
    public AnswersScript[] others;
    public string[] answer;
    public bool[] isAnswer;
    private Color original;
    private Button select;
    TMPro.TextMeshProUGUI script;
    Image image;
    

    // Start is called before the first frame update
    void Start()
    {
        script = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        image = GetComponent<Image>();
        select = GetComponent<Button>();
        original = image.color;
        select.enabled = false;
        //pointer = 0;
        //script.text = answer[pointer];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAnswer()
    {
        questionSource.answered = true;
        for(int i = 0; i < 3; i++)
        {
            others[i].GreyOut();
        }
        if (isAnswer[pointer])
        {
            image.color = correct;
        }
        else
        {
            image.color = wrong;
            questionSource.correct = false;
        }
        
    }

    public void GreyOut()
    {
        if (isAnswer[pointer])
        {
            image.color = correct;
        }
        else
        {
            image.color = greyed;
        }
    }

    public void FinalCall()
    {
        image.color = greyed;
        select.enabled = false;
    }

    public void NextQuestion(int value)
    {
        pointer = value;
        script.text = answer[pointer];
        image.color = original;
    }
}
