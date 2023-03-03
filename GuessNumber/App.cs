using System;
using System.Collections;
using System.Text;
using System.Text.Json;

public partial class App
{
    private static readonly string filePath = "lowScoreList.json";


    private const string GoodBye = "Thank you for playing the game!";
    public void Run()
    {

        List<Score> list = ReadFromFile();

        bool gameOn = true;
        Random random = new Random();
        string answer;


        while (gameOn)
        {
            int guess = 0;
            int number = random.Next(1, 101);


            Console.WriteLine("Let's play a game!\n");
            Console.WriteLine("**********************************");
            Console.WriteLine("Choose a number between 1 and 100:\n");

            int i = 0;
            while (guess != number)
            {
                try
                {
                    guess = Convert.ToInt32(Console.ReadLine());
                    if (guess > number)
                    {
                        Console.WriteLine($"Your guess is: {guess}");
                        Console.WriteLine("But your number is too high! So you have to try again!\n");
                    }
                    if (guess < number)
                    {
                        Console.WriteLine($"Your guess is: {guess}");
                        Console.WriteLine("But your number is too low! So you have to try again!\n");
                    }
                    if (guess == number)
                    {

                        Console.WriteLine("Congratulations!You won!!!\n**********************************\n");
                        Console.WriteLine($"Lucky number was {number}.");


                    }

                }
                catch
                {
                    Console.WriteLine("------ Your guess must be a number, otherwise it won't work! ------");
                    i--;
                }
                i++;


            }

            Console.WriteLine($"\n----- Good job! You won after {i} tries. ----- \n");
            Console.WriteLine("Please write your name:");
            string playername = Console.ReadLine();
            list.Add(new Score() { Name = playername, Guesses = i , Date = DateTime.Now});

            bool menu = true;
            //Console.WriteLine(GoodBye);
            while (menu)
            {
                Console.WriteLine("1. For playing again press: --Y--" +
                "2. For closing the game press: --N--" +
                "3. To see lowscore press: L");
                answer = Console.ReadLine();
                answer = answer.ToUpper();

                if (answer == "Y")
                {
                    gameOn = true;
                    Console.Clear();
                    break;
                }
                else if (answer == "N")
                {
                    gameOn = false;
                    Console.WriteLine(GoodBye);
                    SaveTheList(list);
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Result list:");
                    Console.WriteLine($"{playername} : {i}");
                }

            }


        }



    }
    private void SaveTheList(List<Score> list)
    {
        var json = JsonSerializer.Serialize(list);
        File.WriteAllText(filePath, json, Encoding.UTF8);


    }

    private List<Score> ReadFromFile()
    {
        if (!File.Exists(filePath))
        {
            return new List<Score>();
        }
        else
        {
            var json = File.ReadAllText(filePath);
            var list = JsonSerializer.Deserialize<List<Score>>(json);
            return list;
        }

    }
}