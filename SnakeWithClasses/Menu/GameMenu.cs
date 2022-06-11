﻿using Snake.UserInput;
using System;
using System.Diagnostics;
using System.Threading;

namespace Snake
{
    internal class GameMenu
    {
        private readonly Field field;       // TODO: May be only Coordinates for ConsoleRow and ConsoleCol
        private Coordinates cursorPossition;
        private readonly string[] mainMenu;
        private readonly string[] subDificult;
        private readonly string[] subSettings;
        private int consoleRow;
        private int consoleCol;
        private char cursorSymbol;
        private int snakeLengthByUser = 6;   // By default

        public GameMenu(Field field)
        {
            this.mainMenu = new string[]
            {
               "New Game",
               "Dificult",
               "Settings",
               "Exit"
            };
            this.subDificult = new string[]
            {
               "Easy",
               "Medium",
               "Hard",
               "Back"
            };
            this.subSettings = new string[]
            {
               "Screen size",
               "Field Color",
               "Snake Color",
               "Snake Length",
               "Back"
            };
            this.field = field;
            this.consoleRow = field.ConsoleRow / 2 - 3;
            this.consoleCol = field.ConsoleCol / 2 - 4;
            this.cursorPossition = new Coordinates(this.consoleRow, this.consoleCol - 2);
            this.cursorSymbol = '*';
        }


        public string[] MainMenu { get => this.mainMenu; }

        public void WellcomeScreen()
        {
            var generator = new Random();
            //var screen = $"Wellcome";
            var loading = "loading...";
            var loadingTime = 1;//generator.Next(5, 15);
            var wellcomeText = new Coordinates(this.consoleRow, this.consoleCol); // TODO: something
            var timer = new Stopwatch();

            timer.Start();
            while (true)
            {
                TimeSpan seconds = timer.Elapsed;
               // Visualizer.WriteOnConsole(screen, wellcomeText.Row, wellcomeText.Col, ConsoleColor.Yellow);
                Visualizer.WriteOnConsole(loading, wellcomeText.Row + 1, wellcomeText.Col, ConsoleColor.Yellow);

                if (seconds.Seconds == loadingTime)
                {
                    timer.Reset();
                    Console.Clear();
                    break;
                }
            }

            Menu(this.mainMenu);
        }
        public void Menu(string[] menu)
        {
            MenuItems screen = new MenuItems(menu, this.consoleRow, this.consoleCol);
            CursorMove(menu.Length);

            int currentElementIndex = screen.GetElement(cursorPossition); // Get index of current array
            this.cursorPossition = new Coordinates(consoleRow, consoleCol - 2);

            if (mainMenu == menu)
            {
                switch (currentElementIndex)
                {
                    case 0: Engine.Start(field, snakeLengthByUser); break;       // New Game
                    case 1: Menu(this.subDificult); break;   // Dificult
                    case 2: Menu(this.subSettings); break;   // Settings
                    case 3: Environment.Exit(0); break;            // Exit   -->  TODO: fix this
                }
            }
            else if (this.subDificult == menu)
            {
                switch (currentElementIndex)
                {
                    case 0:; break;  // Easy
                    case 1:; break; // Medium
                    case 2:; break; // Hard
                    case 3: Menu(mainMenu); break;   // Back
                }
            }
            else if (this.subSettings == menu)
            {
                switch (currentElementIndex)
                {
                    case 0:; break;  // Screen size
                    case 1:; break; // Field Color
                    case 2:; break; // Snake Color
                    case 3: SnakeLength(); break;   // Snake Length
                    case 4: Menu(this.mainMenu); break;   // Back
                }
            }
        }
        private void CursorMove(int lenghtOfArr)
        {
            int move = 0;
            while (true)
            {
                Visualizer.DrowingCursor(this.cursorSymbol, this.cursorPossition.Row, this.cursorPossition.Col);

                var key = UserKeyInput.GetInput();
                switch (key)
                {
                    case KeyPressed.Up:
                        move = -1;
                        break;
                    case KeyPressed.Down:
                        move = 1;
                        break;
                    case KeyPressed.Enter:
                        this.consoleRow = this.field.ConsoleRow / 2 - 3; // Reset rows and cols
                        this.consoleCol = this.field.ConsoleCol / 2 - 4;
                        Console.Clear();
                        return;
                }

                if (key != KeyPressed.None)
                {
                    Visualizer.DrowingCursor(' ', this.cursorPossition.Row, this.cursorPossition.Col);
                    this.cursorPossition.Row += move;
                }
                if (this.cursorPossition.Row < this.consoleRow)
                    this.cursorPossition.Row = this.consoleRow;
                else if (this.cursorPossition.Row > this.consoleRow + lenghtOfArr - 1)
                    this.cursorPossition.Row = this.consoleRow + lenghtOfArr - 1;
            }
        }

        private void SnakeLength()
        {
            Visualizer.WriteOnConsole($"Enter length -> min 3 - ", 14, 50, ConsoleColor.Yellow);
            Console.CursorVisible = true;
            int length = int.Parse(Console.ReadLine());
            if (length >= 3 && length <= 50)
            {
                this.snakeLengthByUser = length;
            }
            else
            {
                Console.Clear();
                Visualizer.WriteOnConsole($"Incorect Length! Try again!", 14, 50, ConsoleColor.Red);
                Thread.Sleep(1500);
                Console.Clear();
                SnakeLength();
            }
            Console.CursorVisible = false;
            Console.Clear();
            Menu(this.mainMenu);
        }
    }
}
