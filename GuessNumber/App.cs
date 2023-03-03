using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

public partial class App
{
    private static readonly string filePath = "lowScoreList.json";
    private const string GoodBye = "Thank you for playing the game!";
    public string playername = "";
    public bool gameOn = true;

    public int i = 0;

    public void Run()
    {
        Random random = new Random();
        List<Score> list = ReadFromFile();
        while (gameOn)
        {
            int guess = 0;
            int number = random.Next(1, 101);

            Console.WriteLine("Let's play a game!");
            Console.WriteLine("**********************************");
            Console.WriteLine("Choose a number between 1 and 100:");


            while (guess != number)
            {
                try
                {
                    guess = Convert.ToInt32(Console.ReadLine());
                    if (guess < 1 || guess > 100)
                    {
                        Console.WriteLine("You need to pass in a number between 1 and 100!");
                    }
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

            Console.WriteLine($"----- Good job! You won after {i} tries. ----- ");

            Console.WriteLine("Please write your name:");
            playername = Console.ReadLine();
            list.Add(new Score() { Name = playername, Guesses = i, Date = DateTime.Now });
            Menu(list);


        }


    }

    private List<Score> Menu(List<Score> list)
    {
        bool menu = true;
        string answer;
        while (menu)
        {
            Console.WriteLine("1. For playing again press: --Y--\n" +
            "2. For closing the game press: --N--\n" +
            "3. To see lowscore press: L");
            answer = Console.ReadLine();
            answer = answer.ToUpper();

            if (answer == "Y")
            {
                gameOn = true;
                Console.Clear();
                return list;
            }
            else if (answer == "N")
            {
                gameOn = false;
                Console.WriteLine(GoodBye);
                SortTheList(list);
                Environment.Exit(0);
            }
            else
            {
                foreach (var item in list)
                {
                    Console.WriteLine(item.Name + item.Guesses + item.Date);
                }
                return list;
            }
            
        }
        return list;
    }
    private void SortTheList(List<Score> list)
    {
        var sortedList = list.OrderBy(x => x.Guesses).ToList();
        if (sortedList.Count > 5)
        {
            sortedList.RemoveRange(5, sortedList.Count() - 5);
            SaveTheList(sortedList);
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
