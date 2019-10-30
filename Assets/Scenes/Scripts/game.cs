using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

class Question
{
    public string question;
    public string[] answers;
    public Question(string s)
    {
        question = s;
    }

    public void addAnswers(string[] a)
    {
        answers = a;
    }

}

public class game : MonoBehaviour
{

    public int GameTimeMinutes;
    public int AgentTimeMinutes;

    static Question[] Questions = new Question[14];
    public static int QuestionNumber = 0;
    public Text Question;
    public Text timeNow;
    public GameObject Agent;
    public GameObject ans0;
    public GameObject ans1;
    public GameObject ans2;
    public GameObject ans3;
    public GameObject ans4;
    public GameObject Menue;
    public GameObject IQGame;
    public GameObject Success;
    public GameObject Version;
    string[] answers=new string[14];
    public bool Finish = false;
    public bool Agentwalk = false;

    // Start is called before the first frame update


    void Start()
    {
     
        Questions[0] = new Question("Which one of the five is least like the other four ?");
        string[] answers0 = new string[5];
        answers0[0] = "Dog";
        answers0[1] = "Mouse";
        answers0[2] = "Lion";
        answers0[3] = "Snake";
        answers0[4] = "Elephant";
        Questions[0].addAnswers(answers0);

        Questions[13] = new Question("Which one of the five is least like the other four ?");
        string[] answers13 = new string[5];
        answers13[0] = "Shark";
        answers13[1] = "Deer";
        answers13[2] = "Cow";
        answers13[3] = "Dog";
        answers13[4] = "Lion";
        Questions[13].addAnswers(answers13);

        Questions[1] = new Question("The word 'racecar' is spelled the same forwards and backwards.");
        string[] answers1 = new string[2];
        answers1[0] = "True";
        answers1[1] = "False";
        Questions[1].addAnswers(answers1);

        Questions[12] = new Question("The word 'reviver' is spelled the same forwards and backwards.");
        string[] answers12 = new string[2];
        answers12[0] = "True";
        answers12[1] = "False";
        Questions[12].addAnswers(answers12);

        Questions[2] = new Question("The entire following sentence makes sense if the word toog is understood to mean the same as the word start: \n" +
          "" + " I tooged the car and turned on the radio just in time to hear the announcer say,\n" +
          " 'The marathon is over as the first runner crosses the toog line.'");
        string[] answers2 = new string[2];
        answers2[0] = "True";
        answers2[1] = "False";
        Questions[2].addAnswers(answers2);

        Questions[11] = new Question("The entire following sentence makes sense if the word gwah is understood to mean the same as the word Finish: \n" +
            "" + " If I gwahed the dishes,\n" +
            "I would not gwahed the laundry");
        string[] answers11 = new string[2];
        answers11[0] = "True";
        answers11[1] = "False";
        Questions[11].addAnswers(answers11);

        Questions[3] = new Question("Which one of the five choices makes the best comparison? God is to Dog as Fun is to:");
        string[] answers3 = new string[5];
        answers3[0] = "Nuf";
        answers3[1] = "Fantasy";
        answers3[2] = "Feline";
        answers3[3] = "Canine";
        answers3[4] = "Infinity";
        Questions[3].addAnswers(answers3);

        Questions[10] = new Question("Which one of the five choices makes the best comparison? Frog is to Gorf as Sweet is to:");
        string[] answers10 = new string[5];
        answers10[0] = "Teews";
        answers10[1] = "weets";
        answers10[2] = "teeth";
        answers10[3] = "seet";
        answers10[4] = "swets";
        Questions[10].addAnswers(answers10);

        Questions[4] = new Question("Which one of the five choices makes the best comparison? \n PEACH is to HCAEP as 46251 is to:");
        string[] answers4 = new string[5];
        answers4[0] = "25641";
        answers4[1] = "26451";
        answers4[2] = "12654";
        answers4[3] = "51462";
        answers4[4] = "15264";
        Questions[4].addAnswers(answers4);

        Questions[9] = new Question("Which one of the five makes the best comparison? FWAWWFA is to 9323392 as AFFWWAF is to:");
        string[] answers9 = new string[5];
        answers9[0] = "2993329";
        answers9[1] = "2847495";
        answers9[2] = "2997534";
        answers9[3] = "9238585";
        answers9[4] = "2984323";
        Questions[9].addAnswers(answers9);

        Questions[5] = new Question("Happiness is to Senipah as 517768399 is to");
        string[] answers5 = new string[5];
        answers5[0] = "9386715";
        answers5[1] = "9378622";
        answers5[2] = "5128736";
        answers5[3] = "5182963";
        answers5[4] = "9372636";
        Questions[5].addAnswers(answers5);

        Questions[8] = new Question("Listen is to Silent as 284195 is to");
        string[] answers8 = new string[5];
        answers8[0] = "482951";
        answers8[1] = "487345";
        answers8[2] = "152343";
        answers8[3] = "482915";
        answers8[4] = "485129";
        Questions[8].addAnswers(answers8);

        Questions[7] = new Question("If you rearrange the letters CIFAIPC you would have the name of a(n):");
        string[] answers7 = new string[5];
        answers7[0] = "City";
        answers7[1] = "Animal";
        answers7[2] = "Ocean";
        answers7[3] = "River";
        answers7[4] = "Country";
        Questions[7].addAnswers(answers7);

        Questions[6] = new Question(" If you rearrange the letters 'ANICH', you would have the name of a / an:");
        string[] answers6 = new string[5];
        answers6[0] = "Country";
        answers6[1] = "Ocean";
        answers6[2] = "State";
        answers6[3] = "City";
        answers6[4] = "Animal";
        Questions[6].addAnswers(answers6);

        if (QuestionNumber < 14 && QuestionNumber >= 0)
        {
            setQuestion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeNow.text =  DateTime.Now.ToString("HH:mm:ss ");
    }

    public void Answer0()
    {
        CheckAnswer(ans0.transform.GetChild(0).gameObject.GetComponent<Text>().text);
    }
    public void Answer1()
    {
        CheckAnswer(ans1.transform.GetChild(0).gameObject.GetComponent<Text>().text);
    }
    public void Answer2()
    {
        CheckAnswer(ans2.transform.GetChild(0).gameObject.GetComponent<Text>().text);
    }
    public void Answer3()
    {
        CheckAnswer(ans3.transform.GetChild(0).gameObject.GetComponent<Text>().text);
    }
    public void Answer4()
    {
        CheckAnswer(ans4.transform.GetChild(0).gameObject.GetComponent<Text>().text);
    }

    public void CheckAnswer(String answer)
    {
        if (QuestionNumber < 14 && QuestionNumber >= 0) {
            answers[QuestionNumber] = answer;
            Debug.Log("Question Number :" + QuestionNumber + " The Answer : " + answers[QuestionNumber] + "  " + DateTime.Now);
            Logger.Log("TABLET", "Question " + QuestionNumber + ", answered " + answers[QuestionNumber]);
        }

        if (QuestionNumber < 14 && QuestionNumber >= 0 && !Finish)
        {
           

            if (Version.transform.GetChild(0).gameObject.GetComponent<Text>().text.Equals("Version A"))
            {
                QuestionNumber++;

            }
            else
            {
                QuestionNumber--;
            }

            if (QuestionNumber < 14 && QuestionNumber >= 0)
            {
                setQuestion();
            }
            else
            {
                gameFinished();
            }
        }
        else
        {
            gameFinished();
        }
        
    }

    void setQuestion()
    {
        Question.text = Questions[QuestionNumber].question;

        if (Questions[QuestionNumber].answers.Length == 2)
        {
            ans3.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[0];
            ans4.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[1];
            ans0.SetActive(false);
            ans1.SetActive(false);
            ans2.SetActive(false);
        }
        else
        {
            ans0.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[0];
            ans1.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[1];
            ans2.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[2];
            ans3.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[3];
            ans4.transform.GetChild(0).gameObject.GetComponent<Text>().text = Questions[QuestionNumber].answers[4];
            ans0.SetActive(true);
            ans1.SetActive(true);
            ans2.SetActive(true);
        }
    }

    private void gameFinished()
    {
        Logger.Log("TABLET", "Finished");
        Success.SetActive(true);
        IQGame.SetActive(false);
    }

    public void startt()
    {
        Logger.Log("TABLET", Version.transform.GetChild(0).gameObject.GetComponent<Text>().text + " started");
        Menue.SetActive(false);
        IQGame.SetActive(true);

        Invoke("setFinished", GameTimeMinutes * 60);
        Invoke("setAgent", AgentTimeMinutes * 60);
    }

    private void setFinished()
    {
        Finish = true;
    }

    private void setAgent()
    {
        if (Agent)
        {
            Agent.SetActive(true);
        }
    }

    public void VersionToggle()
    {
        if(Version.transform.GetChild(0).gameObject.GetComponent<Text>().text.Equals("Version A"))
        {
            Version.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Version B";
            QuestionNumber = 13;
        }
        else
        {
            Version.transform.GetChild(0).gameObject.GetComponent<Text>().text = "Version A";
            QuestionNumber = 0;

        }

    }


}
