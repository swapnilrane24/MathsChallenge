using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script manages the question , ans and to which button the ans is ti be assigned
/// </summary>

public class MathsAndAnswerScript : MonoBehaviour {

    //we make this script instance
    public static MathsAndAnswerScript instance;

    //2 private floats this are the question values a and b
    private float a, b ;
    //the variable for answer value
    [HideInInspector] public float answer;
    //varible whihc will assign ans to any one of the 4 answer button
    private float locationOfAnswer;
    //ref to the button
    public GameObject[] ansButtons;
    //ref to image symbol so player can know which operation is to be done
    public Image mathSymbolObject;
    //ref to all the symbol sprites whihc will be used in above image
    public Sprite[] mathSymbols;
    //get the tag of button 
    public string tagOfButton;

    //varible to check whihc mode is this
    private GameMode currentMode;

    //ref to the time for each question
    public float timeForQuestion;

    //score vairable
    [HideInInspector]public int score;

    //ref to text in scene where we will assign a and b values of question
    public Text valueA , valueB;

    //this is to check the progress of player so we can decrease the time with increase in score
    private float scoreMileStone;
    public float scoreMileStoneCount;


    void Awake()
    {
        MakeInstance();
    }

    //method whihc make this object instance
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //at start we need to do few basic setups
	void Start ()
    {
        //we put the location value in tag of button variable
        tagOfButton = locationOfAnswer.ToString();

        //at start the mileSton value is equal to mile stone count
        scoreMileStone = scoreMileStoneCount;

        //get the time value
        GameManager.singleton.timeForQuestion = timeForQuestion;

        if (GameManager.singleton != null)
        {
            //get whihc mode is selected
            currentMode = GameManager.singleton.currentMode;
        }

        MathsProblem();

    }
	
	// Update is called once per frame
	void Update ()
    {
        tagOfButton = locationOfAnswer.ToString();

        MileStoneProcess();
    }

    //this method reduces the time with increase in score
    void MileStoneProcess()
    {
        if (scoreMileStone < GameManager.singleton.currentScore)
        {
            scoreMileStone += scoreMileStoneCount;

            timeForQuestion += 0.02f;

            if (timeForQuestion >= 0.2f)
            {
                timeForQuestion = 0.2f;
            }

        }
    }


    //Below code is for maths calculation
    //this methode calls the respective method for the respective mode
    //eg for addition mode it will only call addition method
    public void MathsProblem()
    {
        switch (currentMode)
        {
            case GameMode.Addition:
                AdditionMethod();
                break;
            case GameMode.Subtraction:
                SubtractionMethod();
                break;
            case GameMode.Multiplication:
                MultiplicationMethod();
                break;
            case GameMode.Division:
                DivisionMethod();
                break;
            case GameMode.Mix:
                int r = Random.Range(0, 4);

                if (r == 0)
                {
                    AdditionMethod();
                }
                else if (r == 1)
                {
                    SubtractionMethod();
                }
                else if (r == 2)
                {
                    MultiplicationMethod();
                }
                else if (r == 3)
                {
                    DivisionMethod();
                }
                break;
        }
    }

                                                               // Addition
    //this methode perform addition process
    void AdditionMethod()
    {
        //we assign the random number to a and b , it range from 0 - 21
        a = Random.Range(0, 21);
        b = Random.Range(0, 21);

        //we the assign the location of answer a random number from our total number of buttons
        locationOfAnswer = Random.Range(0, ansButtons.Length);

        //we get the answer value
        answer = a + b;

        //the question values are assigned to question text
        valueA.text = "" + a;
        valueB.text = "" + b;

        //and we assign the math symbol to symbol image
        mathSymbolObject.sprite = mathSymbols[0];

        //now we assign the values to the ans buttons
        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i == locationOfAnswer)
            {
                //we check for location value and the assign it to the corresponding ans button 
                ansButtons[i].GetComponentInChildren<Text>().text = "" + answer;

            }
            else
            {
                //for other ans button we assign random values
                ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1,41);

                while (ansButtons[i].GetComponentInChildren<Text>().text == "" + answer)
                {
                    //we make sure that only one button has answer values 
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 41);
                }
            }

        }
        
     }
    // Addition

    //Subtraction
    //this methode perform Subtraction process
    void SubtractionMethod()
    {
        //similar to the addition method only we do subtraction here
        a = Random.Range(0, 21);
        b = Random.Range(0, 21);

        while (a <= b)
        {
            a = Random.Range(0, 21);
            b = Random.Range(0, 21);
        }


        locationOfAnswer = Random.Range(0, ansButtons.Length);

        answer = a - b;

        valueA.text = "" + a;
        valueB.text = "" + b;

        mathSymbolObject.sprite = mathSymbols[1];

        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i == locationOfAnswer)
            {

                ansButtons[i].GetComponentInChildren<Text>().text = "" + answer;

            }
            else
            {
                ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 41);

                while (ansButtons[i].GetComponentInChildren<Text>().text == "" + answer)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 41);
                }
            }

        }

    }
    //Subtraction


    //Multiplication
    //this methode perform Multiplication process
    void MultiplicationMethod()
    {
        //similar to the addition method only we do multiplication here
        a = Random.Range(1, 21);
        b = Random.Range(1, 21);


        locationOfAnswer = Random.Range(0, ansButtons.Length);

        answer = a * b;

        valueA.text = "" + a;
        valueB.text = "" + b;

        mathSymbolObject.sprite = mathSymbols[2];

        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i == locationOfAnswer)
            {

                ansButtons[i].GetComponentInChildren<Text>().text = "" + answer;

            }
            else
            {
                // the below code make sure that all the values assigned to the ans button are within the range
                //for ex: if the answer is 45 the other button values will be between 1 to 100
                //if you want you can make it more difficult by reducing the range
                if (a * b <= 100)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 101);
                }
                else if (a * b <= 200 & a * b > 100)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(101, 201);
                }
                else if (a * b <= 300 & a * b > 200)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(201, 301);
                }
                else if (a * b <= 400 & a * b > 300)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(301, 401);
                }

                while (ansButtons[i].GetComponentInChildren<Text>().text == "" + answer)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 401);
                }
            }

        }

    }
    //Multiplication


    //Division
    //this methode perform Division process
    void DivisionMethod()
    {
        //similar to addition method
        a = Random.Range(1, 31);
        b = Random.Range(1, 31);

        //here  % is called modular , now the modular gives us the remainder
        //for ex: if we divide 3 by 2 we get remainder 1 
        //we here check that if remainder is not equal to zero , we again assign values to ques and we keep doing it
        //until we get zero as reminder
        while (a % b != 0)
        {
            a = Random.Range(1, 31);
            b = Random.Range(1, 31);
        }
        
        answer = a / b;
        

        locationOfAnswer = Random.Range(0, ansButtons.Length);

        valueA.text = "" + a;
        valueB.text = "" + b;

        mathSymbolObject.sprite = mathSymbols[3];

        for (int i = 0; i < ansButtons.Length; i++)
        {
            if (i == locationOfAnswer)
            {
                if (answer - (int)answer != 0)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = answer.ToString("F1");
                }
                else
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + answer;
                }

            }
            else
            {
                //here range is less because our number for division are less
                ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 31);

                while (ansButtons[i].GetComponentInChildren<Text>().text == "" + answer)
                {
                    ansButtons[i].GetComponentInChildren<Text>().text = "" + Random.Range(1, 31);
                }
            }

        }

    }

    //Division







}
