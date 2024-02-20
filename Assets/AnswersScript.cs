using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class AnswersScript : MonoBehaviour
{
    public int position;
    public QuestionsScript questionSource;
    public int pointer;
    public Color correct;
    public Color wrong;
    public Color greyed;
    public AnswersScript[] others;
    private string[] answer;
    private bool[] isAnswer;
    private Color original;
    private Button select;
    TMPro.TextMeshProUGUI script;
    Image image;


    // Start is called before the first frame update
    void Start()
    {
        pointer = -1;
        var ansRow = position * 2;
        var dataset = Resources.Load<TextAsset>("Questions");
        var rows = dataset.text.Split('\n');
        var data = Regex.Split(rows[ansRow], ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        answer = data[1..];
        var data2 = rows[ansRow + 1].Split(',');
        var corectString = data2[1..];

        for (int i = 0; i < answer.Length; i++)
        {
            answer[i] = answer[i].Replace("\"", string.Empty);
        }
        var temp = new List<bool>();
        for (int i = 0; i < answer.Length; i++)
        {
            if (corectString[i].Contains("1"))
            {
                temp.Add(true);
            } 
            else 
            {
                temp.Add(false);
            }
        }
        isAnswer = temp.ToArray();

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
