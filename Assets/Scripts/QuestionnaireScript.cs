using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class QuestionnaireScript : MonoBehaviour
{
    static string[] Questions = new string[6];
    static int QuestionNumber = 0;
    public GameObject done;
    public GameObject Questionnaire;
    public GameObject ToggleGroup;
    public Text Question;

    // Start is called before the first frame update
    void Start()
    {
        Questions[0] = "The game was easy";
        Questions[1] = "The controls were easy to use";
        Questions[2] = "Playing the game was fun";
        Questions[3] = "Playing the game was natural";
        Questions[4] = "I was focused on the game";
        Questions[5] = "The surroundings distracted me from the game";
    }

    // Update is called once per frame
    void Update()
    {
        if (QuestionNumber < 6)
        {
            Question.text = Questions[QuestionNumber];
        }
    }
    public Toggle current{
        get { return ToggleGroup.GetComponent<ToggleGroup>().ActiveToggles().FirstOrDefault(); }
    }
    public void Next()
    {
        if(current != null)
        {
            Debug.Log("Question Number :" + QuestionNumber + " The Answer : " + current.tag + "  ");
            Logger.Log("QUESTIONNAIRE", "Question " + QuestionNumber + ", answered " + current.tag);
            ToggleGroup.GetComponent<ToggleGroup>().SetAllTogglesOff();
            if (QuestionNumber < 6)
            {
                QuestionNumber++;
            }
            else
            {
                Logger.Log("QUESTIONNAIRE", "Finished");
                done.SetActive(true);
                Questionnaire.SetActive(false);
            }
        }
       

    }
}
