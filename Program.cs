using System;

namespace lifeAdventure
{
    internal class SoloPlayVersion
    {
        static void Main(string[] args)
        {
            int die = 0, playersTile = 0, myTile = 0, round = 0, count = 0;
            bool gameContinued = true;
            Random r = new Random();
            Console.Write("What is your name? : ");
            string name = Console.ReadLine();
            Console.Write($"Hello, {name}! The map has 30 tiles that you can land on.\nEnter 1 if you want to play first. Otherwise, Enter 2: ");
            int input = int.Parse(Console.ReadLine());

            while (gameContinued)
            {
                switch (input)
                {
                    case 1:
                        Console.Write("It's your turn! Roll your die by Entering 1: ");
                        input = int.Parse(Console.ReadLine());
                        while (input != 1)
                        {
                            Console.WriteLine("Invalid input. Please try again.\nRoll your die by Entering 1: ");
                            input = int.Parse(Console.ReadLine());
                        }
                        RollDie(name, ref die, r);
                        CalculateTile(name, ref die, ref playersTile);
                        if (playersTile >= 30)
                            break;
                        Actions(name, ref playersTile);
                        if (playersTile >= 30)
                            break;
                        PrintStatus(name, ref playersTile, ref round, ref count);
                        input = 2;
                        break;
                    case 2:
                        Console.WriteLine("It's CPU's turn. Please wait...");
                        RollDie("CPU", ref die, r);
                        CalculateTile("CPU", ref die, ref myTile);
                        if (myTile >= 30)
                            break;
                        Actions("CPU", ref myTile);
                        if (myTile >= 30)
                            break;
                        PrintStatus("CPU", ref myTile, ref round, ref count);
                        input = 1;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.\nEnter 1 if you want to play first. Otherwise, Enter 2: ");
                        input = int.Parse(Console.ReadLine());
                        break;
                }

                if (playersTile >= 30)
                {
                    Console.WriteLine($"Congratulations, {name}! You win!");
                    gameContinued = false;
                }
                else if (myTile >= 30)
                {
                    Console.WriteLine("You lose...");
                    gameContinued = false;
                }
            }
        }

        static void RollDie(string name, ref int die, Random r)
        {
            die = r.Next(6) + 1;
            Console.WriteLine($"{name} rolled {die}!");
            return;
        }

        static void CalculateTile(string name, ref int tile, ref int location)
        {
            location += tile;
            if (location >= 30)
                Console.WriteLine($"Finally, {name} has reached the last tile!");
            else
            {
                Console.WriteLine($"{name} is on tile number {location}.");
                Console.WriteLine("Press the enter key to see what they landed on...");
                Console.ReadLine();
            }
            return;
        }

        static void Actions(string name, ref int location)
        {
            if (location % 2 == 0 && location % 5 == 0)
            {
                Console.WriteLine($"{name} worked hard and got a raise. Move +5");
                location += 5;
            }
            else if (location % 2 == 0 && location % 3 == 0)
            {
                Console.WriteLine($"{name} helped their neighbors and the neighbors gifted some money. Move +3");
                location += 3;
            }
            else if (location % 5 == 0)
            {
                Console.WriteLine($"{name} overslept and made their boss unhappy. Move -3");
                location -= 3;
            }
            else if (location % 3 == 0)
            {
                Console.WriteLine($"{name} had to see their family and took PTO. Move -1");
                location -= 1;
            }
            else if (location % 2 == 0)
            {
                Console.WriteLine($"{name} studied for their work and passed the certificate exam. Move +1");
                location += 1;
            }
            else
            {
                Console.WriteLine($"{name} fell on the ground while biking and had to be in the hospital.\nSolve this equation to decide how long they will be in the hospital.");
                Random r = new Random();
                int r1 = r.Next(20) + 1;
                int r2 = r.Next(10) + 1;
                Console.Write($"{r1} x {r2} = ");
                int answer = int.Parse(Console.ReadLine());
                if (name == "CPU")
                {
                    if (r1 * r2 == answer)
                    {
                        Console.WriteLine($"Your answer is correct! CPU needs to be in the hospital for a week. Move -5");
                        location -= 5;
                    }
                    else
                    {
                        Console.WriteLine($"Your answer is incorrect! CPU only needs to be in the hospital for a day. Stay there.");
                    }
                }
                else
                {
                    if (r1 * r2 == answer)
                    {
                        Console.WriteLine($"Your answer is correct! You only need to be in the hospital for a day. Stay there.");
                    }
                    else
                    {
                        Console.WriteLine($"Your answer is incorrect! You need to be in the hospital for a week. Move -5");
                        location -= 5;
                    }
                }
            }

            if (location >= 30)
                Console.WriteLine($"Finally, {name} has reached the last tile!");
            else
            {
                Console.WriteLine("Press the enter key to see the status...");
                Console.ReadLine();
            }
            return;
        }

        static void PrintStatus(string name, ref int tile, ref int round, ref int count)
        {
            count++;
            if (count % 2 == 1)
                round++;
            Console.WriteLine($"************************* Round {round} *************************");
            Console.WriteLine($"Player: {name}");
            if (tile < 0)
                tile = 0;
            Console.WriteLine($"Tile number: {tile}");
            Console.WriteLine($"There are {30 - tile} tiles remaining.");
            Console.WriteLine("***********************************************************");
            Console.WriteLine("Press the enter key to end the turn...");
            Console.ReadLine();
        }
    }
}
