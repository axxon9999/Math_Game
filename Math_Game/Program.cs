using System.Diagnostics;

Console.WriteLine("Math Game");
Console.WriteLine();

string mainMenuInput;
int levelInput;
int questionsInput;

string now = DateTime.Now.ToString("dd-MM-yyyy");
string type = "test";
string level;
int score = 0;
int timeSec = 0;

List<List<string>> mainDataBase = new List<List<string>>();

/*-----------------MAIN------------------*/

do
{
    do
    {
        
        mainMenuInput = displayMainMenu();
        

    } while (mainMenuInput != "V" && mainMenuInput != "A" && mainMenuInput != "S" && mainMenuInput != "M" && mainMenuInput != "D" && mainMenuInput != "R" && mainMenuInput != "Q");

    if (mainMenuInput == "Q")
    {
        break;
    }

    if (mainMenuInput == "V")
    {
        Console.Clear();

        displayDatabase();
        
        continue;
    }

    do
    {
        Console.Clear();
        levelInput = displayLevelMenu();
        
    } while(levelInput < 10 || levelInput > 100);

    /*Add test amount*/

    do
    {
        Console.Clear();
        questionsInput = displayAmountQuestions();
    } while (questionsInput < 5 || questionsInput > 20);

    /*Play the game*/

    List<string> recordData = new List<string>();
    recordData = playTheGame(mainMenuInput, questionsInput, recordData, levelInput);

    if (levelInput == 10)
    {
        recordData[2] = "1";
    }
    else if (levelInput == 50)
    {
        recordData[2] = "2";
    }
    else if (levelInput == 100)
    {
        recordData[2] = "3";
    }

    if(mainMenuInput == "A")
    {
        recordData[1] = "Addition";
    }
    else if (mainMenuInput == "S")
    {
        recordData[1] = "Subtraction";
    }
    else if (mainMenuInput == "M")
    {
        recordData[1] = "Multiplication";
    }
    else if (mainMenuInput == "D")
    {
        recordData[1] = "Division";
    }
    else if (mainMenuInput == "R")
    {
        recordData[1] = "Random";
    }

    /*Show final score*/
    Console.WriteLine();
    Console.WriteLine($"Final:\tDate\t\tType\t\tLevel\tScore\tTime(Sec)");
    Console.WriteLine($"\t---------------------------------------------------------");
    Console.WriteLine($"\t{recordData[0]}\t{recordData[1]}\t{recordData[2]}\t{recordData[3]}\t{recordData[4]}");
    
    /*Add to the results to the 2D List*/

    mainDataBase.Add( recordData );

} while (true);

/*---------------MAIN MENU-------------------*/

string displayMainMenu() 
{
    //Menu 1
    Console.WriteLine();
    Console.WriteLine("Menu");
    Console.WriteLine();
    Console.WriteLine("V - View Preview");
    Console.WriteLine("A - Addition");
    Console.WriteLine("S - Substraction");
    Console.WriteLine("M - Multiplication");
    Console.WriteLine("D - Division");
    Console.WriteLine("R - Random");
    Console.WriteLine("Q - Quit");
    Console.WriteLine();
    Console.Write("Choose an option in the menu. ");
    string mainMenuInput = Console.ReadLine();
    return mainMenuInput;
}

/*---------------LEVEL MENU-------------------*/

int displayLevelMenu()
{
    int highLevelInput;
    Console.Write("Choose level [1-3] ");
    var levelInput = Console.ReadLine();
    if (levelInput == "1")
    {
        highLevelInput = 10;
        
    } else if (levelInput == "2")
    {
        highLevelInput = 50;
        
    } else if (levelInput == "3")
    {
        highLevelInput = 100;
        
    } else
    {
        highLevelInput = 0;
    }

    return highLevelInput;
    
}

/*----------------AMOUNT OF QUESTIONS MENU 3 -----------------*/

int displayAmountQuestions()
{
    int howManyQuestions;
    Console.Write("Choose the amount of questions [5-20] ");
    var numberString = Console.ReadLine();
    howManyQuestions = Convert.ToInt32(numberString);
    return howManyQuestions;
}

/*----------------------PLAY THE GAME-------------------------*/

List<string> playTheGame(string mainMenuInput, int questionsInput, List<string> recordData, int levelInput)
{
    // Date, Type, Level, Score, TimeSec
                                        
    recordData.Add(now); // Record Date
    recordData.Add(mainMenuInput);  // Record Type
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();  // START TIMER

    for (int i = 0; i < questionsInput; i++)
    {
        Console.Clear();
        Console.WriteLine("Question " + (i + 1));
        int[] nums = selectTwoRandomNumbers(levelInput);
        
        if (mainMenuInput == "A")
        {
            addition(nums);
        }
        else if (mainMenuInput == "S")
        {
            subtraction(nums);
        }
        else if (mainMenuInput == "D")
        {
            division(nums);
        }
        else if (mainMenuInput == "M")
        {
            multiplication(nums);
        }
        else if (mainMenuInput == "R")
        {
            string[] option = { "A", "S", "M", "D" };
            Random index = new Random();
            int indexOps = index.Next(0, 4);

            String operationSelection = option[indexOps];

            if (operationSelection == "A")
            {
                addition(nums);
            }
            else if (operationSelection == "S")
            {
                subtraction(nums);
            }
            else if (operationSelection == "M")
            {
                multiplication(nums);
            }
            else if (operationSelection == "D")
            {
                division(nums);
            }
        }
    }

    recordData.Add(Convert.ToString(levelInput)); // Record Level
    recordData.Add(Convert.ToString(score)); // Record Score

    stopWatch.Stop(); // STOP TIMER
    string totalTime = Convert.ToInt32(stopWatch.Elapsed.TotalSeconds).ToString();  // TOTAL TIME

    recordData.Add(totalTime); // Record Time
    score = 0; // Reset Score to 0 for a new Game
    return recordData;
}

/*----------------CREATE TWO RANDOM NUMBERS-------------------*/

int[] selectTwoRandomNumbers(int difficultyLevel)
{
    int[] twoNums = new int[2];

    Random rnd = new Random();

    int lowNumLevel = 0;
    int highNumLevel = difficultyLevel;

    int num1 = rnd.Next(lowNumLevel, highNumLevel);
    int num2 = rnd.Next(lowNumLevel, highNumLevel);

    twoNums[0] = num1;
    twoNums[1] = num2;

    return twoNums;
}

/*-------------------------- ADDITION ---------------------------*/

void addition(int[] nums)
{
    int calcAnswer = nums[0] + nums[1];

    Console.Write(nums[0] + " + " + nums[1] + " =  ? ");

    int inputAnswer = Convert.ToInt32(Console.ReadLine());

    testAnswer(inputAnswer, calcAnswer);
}

/*------------------------- SUBTRACTION -------------------------*/

void subtraction(int[] nums)
{
    while (nums[0] < nums[1])
    {
        nums = selectTwoRandomNumbers(levelInput);
    }

    int calcAnswer = nums[0] - nums[1];

    Console.Write(nums[0] + " - " + nums[1] + " =  ? ");

    int inputAnswer = Convert.ToInt32(Console.ReadLine());

    testAnswer(inputAnswer, calcAnswer);

}

/*------------------------ MULTIPLICATION -----------------------*/

void multiplication(int[] nums)

{
    int calcAnswer = nums[0] * nums[1];

    Console.Write(nums[0] + " X " + nums[1] + " =  ? ");

    int inputAnswer = Convert.ToInt32(Console.ReadLine());

    testAnswer(inputAnswer, calcAnswer);
}

/*-------------------------- DIVISION ---------------------------*/

void division(int[] nums)
{
    while (nums[0] % nums[1] != 0)
    {
        nums = selectTwoRandomNumbers(levelInput);
        if (nums[1] == 0)
        {
            nums[1] = 1;
        }
    }
    int calcAnswer = nums[0] / nums[1];

    Console.Write(nums[0] + " / " + nums[1] + " =  ? ");

    int inputAnswer = Convert.ToInt32(Console.ReadLine());

    testAnswer(inputAnswer, calcAnswer);
}

/*---------------- ADD SCORE DATA TO THE 2D LIST ----------------*/
/*---------------- DISPLAY THE 2D LIST AS A TABLE ---------------*/

/*Record final score in 2D list*/
//==>    // LOOP THE 2D LIST

void displayDatabase()
{
    Console.WriteLine();
    Console.WriteLine($"Final:\tDate\t\tType\t\tLevel\tScore\tTime(Sec)");
    Console.WriteLine($"        ---------------------------------------------------------");


    for (int i = 0; i < mainDataBase.Count; i++)
    {
        Console.WriteLine($"\t{mainDataBase[i][0]}\t{mainDataBase[i][1]}\t{mainDataBase[i][2]}\t{mainDataBase[i][3]}\t{mainDataBase[i][4]}");
    }
    Console.ReadKey();
    Console.Clear();
}


/*--------------------- TEST IF RIGHT ANSWER -------------------*/

void testAnswer(int inputAnswer, int calcAnswer)
{
    if (calcAnswer == inputAnswer)
    {
        Console.WriteLine("Very Good! you calculated the right answer!");
        score++; // GLOBAL
    }
    else
    {
        Console.WriteLine("Wrong answer!");
    }
    Console.ReadKey();
}

/*--------------------- MOD LEVEL TO [1-3] ---------------------*/

  

/*-------------- CHANGE MENU CODE TO LONG TEXT -----------------*/