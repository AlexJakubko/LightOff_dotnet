using System;
using LightsOff.LightsOffCore.Entity;
using LightsOff.LightsOffCore.Service;
using LightsOff.LightsOffCore.Service.RatingServices;
using LightsOffCore.Core;
using LightsOffCore.Entity;
using LightsOffCore.Service;

namespace LightsOff.LightsOffConsole
{
    public class ConsoleUi
    {
        private readonly Field _field;

        private readonly IScoreService _scoreService = new ScoreServiceFile();
        private readonly ICommentService _commentService = new CommentServiceFile();
        private readonly IRatingService _ratingService = new RatingServiceFile();


        public ConsoleUi(Field field)
        {
            this._field = field;
        }

        public void Play()
        {
            Console.WriteLine();
            PrintManual();
            Console.WriteLine();
            PrintScores();
            do
            {
                Console.WriteLine();
                Console.WriteLine("Playing field ");
                Console.WriteLine("Your score: " + _field.Score);
                Console.WriteLine();

                PrintField();
                ProcessInput();
            } while (_field.GetState() == GameState.Playing);

            _scoreService.AddScore(new Score { Name = _field.PlayersName, Points = _field.Score, Level = _field.Level });
            PrintField();
            Console.WriteLine("You won!");

            PrintComments();
            AddComment();
        }

        private void PrintField()
        {
            PrintFieldHeader();
            Console.WriteLine();
            PrintFieldBody();
        }

        private void PrintFieldHeader()
        {
            Console.Write(" ");
            for (var column = 0; column < _field.ColumnCount; column++)
            {
                Console.Write("  ");
                Console.Write(column + 1);
            }

            Console.WriteLine();
        }

        private void PrintFieldBody()
        {
            for (var row = 0; row < _field.RowCount; row++)
            {
                Console.Write((char)('A' + row));
                Console.Write(' ');
                for (var column = 0; column < _field.ColumnCount; column++)
                {
                    var tile = _field[row, column];
                    switch (tile.GetState())
                    {
                        case TileState.Shine:
                            Console.Write(" X ");
                            break;
                        case TileState.Dontshine:
                            Console.Write(" O ");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                Console.WriteLine();
            }

        }

        private void ProcessInput()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter input (e.g. SA0, X): ");
                var input = Console.ReadLine()?.Trim().ToUpper();
                if ("X" == input)
                    Environment.Exit(0);

                if (input != null && input.Length >= 3)
                {
                    try
                    {
                        int row = input[1] - 'A';
                        int column = Int32.Parse(input.Substring(2)) - 1;
                        if (row >= 0 && row < _field.RowCount && column >= 0 && column < _field.ColumnCount)
                        {
                            if (input[0] == 'S')
                            {
                                _field.ShineDots(row, column);
                            }

                            break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("You put wrong Number Format");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input!");
                }
            }
        }

        private void PrintScores()
        {
            Console.WriteLine("Top scores at " + _field.Level + "level");
            Console.WriteLine("Player         Score");
            Console.WriteLine("--------------------------------");
            foreach (var score in _scoreService.GetTopScores())
            {
                if (score.Level == _field.Level)
                {
                    Console.WriteLine("  {0}         {1}", score.Name, score.Points);
                }
            }
        }

        private void PrintComments()
        {
            Console.WriteLine("Last 3 comments:");
            Console.WriteLine("--------------------------------");
            foreach (var comment in _commentService.GetLastComments())
            {
                Console.WriteLine("{0}     {1} ", comment.Name, comment.TimeOfComment);
                Console.WriteLine();
                Console.WriteLine("{0}", comment.Message);
                Console.WriteLine();
            }
        }

        private void AddComment()
        {
            Console.WriteLine("You want leave comment? Answer: Yes or No");

            while (true)
            {
                Console.WriteLine();
                Console.Write("Answer:");
                string input = Console.ReadLine()?.Trim().ToUpper();
                if ("YES" == input)
                {
                    Console.Write("Write your comment: ");
                    input = Console.ReadLine()?.Trim();
                    try
                    {
                        _commentService.AddComment(new Comment { Name = _field.PlayersName, Message = input, TimeOfComment = DateTime.Now });
                        try
                        {
                            AddRating();
                            return;
                        }
                        catch (FormatException)
                        {
                        }
                    }
                    catch (FormatException)
                    {
                        return;
                    }
                }
                else if ("NO" == input)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again.");
                }
            }
        }

        private void AddRating()
        {
            Console.WriteLine();
            Console.WriteLine("Please rate our game (1 to 5)");
            while (true)
            {
                Console.Write("Rate:");
                var input = Console.ReadLine()?.Trim();

                int rate = Int32.Parse(input);
                if (rate > 0 && rate <= 5)
                {
                    _ratingService.AddRating(new Rating { Name = _field.PlayersName, Stars = rate });
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong input! Try again.");
                }
            }
        }

        public void PrintManual()
        {
            Console.WriteLine("Control of the game:");
            Console.WriteLine("The player's task is to turn off all lights on the board. Clicking a light box changes ");
            Console.WriteLine("its status from on to off, but the same happens with a field adjacent ");
            Console.WriteLine("to the north, south, east, and west of the clicked field.");
            Console.WriteLine("You are using coordinates.For the row coodinates use letters and for columns use numbers");
            Console.WriteLine("Form of command :SA0   S-Select, A-row , 0-column");
            Console.WriteLine("Form of command :X - for exit the game");
            Console.WriteLine();

        }
    }
}