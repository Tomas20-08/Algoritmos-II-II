namespace AdivinaElNumero
{
    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    internal class Program
    {
        static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n----------------------------");
            Console.WriteLine("Welcome to Guess The Number!");
            Console.WriteLine("----------------------------\n");
            Console.ResetColor();

            string? playerName;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("Enter your Name: ");
                playerName = Console.ReadLine();
                Console.ResetColor();

                if (string.IsNullOrWhiteSpace(playerName))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("");
            Console.WriteLine("Nice to meet you " + playerName + ".");
            Console.WriteLine("");
            Console.ResetColor();

            Console.WriteLine("The game is simple, these are the rules: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n--------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine("- The machine chooses a random number; the player try to guess that number.");
            Console.WriteLine("- Each difficulty level has a range of numbers.");
            Console.WriteLine("- Each level has a limit of attempts.");
            Console.WriteLine("- After each guess, the machine will tell you if the number is higher or lower.");
            Console.WriteLine("- If you guess the number, you win points based on the difficulty and attempts used.");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("--------------------------------------------------------------------------\n");
            Console.ResetColor();

            Console.WriteLine("Are you ready?");
            Console.WriteLine("");
            Console.Write("Press Any key to Start: ");
            Console.ReadKey(true);
            Console.Clear();

            int round = 1;
            int totalScore = 0;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n------------------------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[E]asy   (1-10)  = 3  attempts");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("[M]edium (1-50)  = 5  attempts");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("[H]ard   (1-100) = 7 attempts");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n------------------------------");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Difficulty Easy\r\n    1 Attempt  => 10 Points\r\n    2 Attempts => 5  Points\r\n    3 Attempts => 1  Points");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Difficulty Medium\r\n    1 Attempt  => 50 Points\r\n    2 Attempts => 30 Points\r\n    3 Attempts => 10 Points\r\n    4 Attempts => 5  Points\r\n    5 Attempts => 1  Points");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Difficulty Hard\r\n    1 Attempt  => 100 Points\r\n    2 Attempts => 80  Points\r\n    3 Attempts => 60  Points\r\n    4 Attempts => 40  Points\r\n    5 Attempts => 20  Points\r\n    6 Attempts => 5   Points\r\n    7 Attempts => 1   Points");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("-------------------------------\n");
                Console.ResetColor();

                Difficulty gameDifficulty = SelectedDifficulty();
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n-------------------------------");
                Console.WriteLine($"¡Total score: {totalScore}! - ¡Round: {round}!");
                Console.WriteLine("-------------------------------\n");
                Console.ResetColor();
                round++;

                switch (gameDifficulty)
                {
                    case Difficulty.Easy:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case Difficulty.Medium:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case Difficulty.Hard:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                }
                Console.WriteLine($"You Selected Difficulty {gameDifficulty}, Good luck {playerName}.");
                Console.ResetColor();

                var (min, max) = GetRange(gameDifficulty);
                int randomNumber = random.Next(min, max + 1);

                Console.WriteLine("");
                Console.WriteLine("I've chosen a random number. Try to guess it!");
                Console.WriteLine("");

                int maxAttempts = gameDifficulty switch
                {
                    Difficulty.Easy => 3,
                    Difficulty.Medium => 5,
                    Difficulty.Hard => 7,
                    _ => 5
                };
                Console.WriteLine($"You have {maxAttempts} attempts to guess the number between {min} and {max}.");
                Console.WriteLine("");

                int attempts = 0;
                bool guessed = false;

                do
                {
                    int enteredNumber = ReadGuess(min, max);
                    int result = CompareGuess(enteredNumber, randomNumber);
                    attempts++;

                    if (result == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n----------------------------------------------");
                        Console.WriteLine($"Correct! You guessed the number in {attempts} attempts.");
                        Console.WriteLine("----------------------------------------------\n");
                        Console.ResetColor();
                        guessed = true;
                        break;
                    }

                    if (result < 0)
                    {
                        Console.WriteLine("\n-----------------------------");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Too low. ");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Try a higher number.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("\n-----------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Too high. ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Try a lower number.");
                        Console.ResetColor();
                    }

                    Console.WriteLine($"Attempts left: {maxAttempts - attempts}.");
                    Console.WriteLine("-----------------------------\n");
                }
                while (attempts < maxAttempts && !guessed);

                if (!guessed)
                {
                    Console.Beep(400, 200);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\n--------------------------------------------------------------");
                    Console.WriteLine($"Sorry {playerName}, you ran out of attempts. The correct number was {randomNumber}.");
                    Console.WriteLine("--------------------------------------------------------------\n");
                    Console.ResetColor();
                }

                int score = Score(gameDifficulty, attempts, guessed);
                totalScore += score;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"You earned {score} points this round.");
                Console.WriteLine("");
                Console.WriteLine($"Total score: {totalScore}.");
                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("Do you want to play again?");

                while (true)
                {
                    Console.Write("Enter [Y] or [N]: ");
                    string? playAgain = Console.ReadLine();

                    if (playAgain?.ToLower() == "y")
                    {
                        Console.Clear();
                        break;
                    }
                    else if (playAgain?.ToLower() == "n")
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"You have earned {totalScore} points in {round - 1} rounds.");
                        Console.WriteLine("");
                        Console.WriteLine("Thanks for playing " + playerName + " ... Goodbye!");
                        Console.ResetColor();
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input.");
                        Console.ResetColor();
                    }
                }
            }
        }

        static Difficulty SelectedDifficulty()
        {
            while (true)
            {
                Console.Write("Enter your choice: ([E]asy, [M]edium, [H]ard): ");
                string? choice = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    continue;
                }

                switch (choice[0])
                {
                    case 'e':
                        return Difficulty.Easy;
                    case 'm':
                        return Difficulty.Medium;
                    case 'h':
                        return Difficulty.Hard;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Invalid input.");
                        Console.ResetColor();
                        Console.WriteLine("");
                        break;
                }
            }
        }

        static (int min, int max) GetRange(Difficulty d)
        {
            return d switch
            {
                Difficulty.Easy => (1, 10),
                Difficulty.Medium => (1, 50),
                Difficulty.Hard => (1, 100),
                _ => (1, 10)
            };
        }

        static int ReadGuess(int min, int max)
        {
            while (true)
            {
                Console.Write("Enter a number: ");
                string? enteredNumber = Console.ReadLine()?.Trim();

                if (!int.TryParse(enteredNumber, out int validatedNumber))
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input.");
                    Console.ResetColor();
                    continue;
                }

                if (validatedNumber < min || validatedNumber > max)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Number out of range. Must be between {min} and {max}.");
                    Console.ResetColor();
                    continue;
                }

                return validatedNumber;
            }
        }

        static int CompareGuess(int validatedNumber, int randomNumber)
        {
            if (validatedNumber < randomNumber)
            {
                return -1;
            }
            else if (validatedNumber > randomNumber)
            {
                return 1;
            }
            return 0;
        }

        static int Score(Difficulty difficulty, int attempts, bool guessed)
        {
            if (!guessed)
            {
                return 0;
            }

            return difficulty switch
            {
                Difficulty.Easy => attempts switch { 1 => 10, 2 => 5, 3 => 1, _ => 0 },
                Difficulty.Medium => attempts switch { 1 => 50, 2 => 30, 3 => 10, 4 => 5, 5 => 1, _ => 0 },
                Difficulty.Hard => attempts switch { 1 => 100, 2 => 80, 3 => 60, 4 => 40, 5 => 20, 6 => 5, 7 => 1, _ => 0 },
                _ => 0
            };
        }
    }
}
